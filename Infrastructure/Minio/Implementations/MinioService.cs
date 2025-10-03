using Application.Data;
using Application.EntityDtos;
using Domain.Commons;
using Elastic.Clients.Elasticsearch.Aggregations;
using Infrastructure.Commons;
using Infrastructure.Helpers;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Minio;
using Minio.Abstractions;
using Minio.ApiEndpoints;
using Minio.DataModel;
using Minio.DataModel.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minio.Implementations
{
    public class MinioService : IMinioService
    {
        private readonly IMinioClient _minio;
        private static readonly ILog log = LogMaster.GetLogger("MinioService", "MinioService");
        public MinioService(IMinioClient minio)
        {
            _minio = minio ?? throw new ArgumentNullException(nameof(minio)); ;
        }
        public async Task<BaseSearchResponse<Minio.DataModel.Bucket>> ListBucketByPageAsync(BaseCriteria request)
        {
            var buckets = await _minio.ListBucketsAsync();
            return await BaseSearchResponse<Minio.DataModel.Bucket>.GetResponse(buckets.Buckets.AsQueryable(), request);
        }
        public async Task<List<Minio.DataModel.Bucket>> ListAllBucketsAsync()
        {
            var buckets = await _minio.ListBucketsAsync();
            return buckets.Buckets.ToList();
        }
        public async Task<BaseSearchResponse<Minio.DataModel.Item>> ListObjectsByPageAsync(MinioCriteria request)
        {
            var result = new List<Minio.DataModel.Item>();

            var args = new ListObjectsArgs()
                .WithBucket(request.BucketName)
                .WithPrefix(request.Prefix)
                .WithRecursive(false);

            var obs = _minio.ListObjectsAsync(args);

            var tcs = new TaskCompletionSource();

            obs.Subscribe(
                item => result.Add(item),
                ex => tcs.SetException(ex),
                () => tcs.SetResult()
            );

            await tcs.Task; // chờ hoàn tất

            return await BaseSearchResponse<Minio.DataModel.Item>.GetResponse(result.AsQueryable(), request);
        }
        public async Task<List<Minio.DataModel.Item>> ListAllObjectsAsync(string bucketName, string? prefix)
        {
            var result = new List<Minio.DataModel.Item>();
            var args = new ListObjectsArgs()
                .WithBucket(bucketName)
                .WithPrefix(prefix)
                .WithRecursive(false);
            var obs = _minio.ListObjectsAsync(args);
            var tcs = new TaskCompletionSource();
            obs.Subscribe(
                item => result.Add(item),
                ex => tcs.SetException(ex),
                () => tcs.SetResult()
            );
            await tcs.Task; // chờ hoàn tất
            return result;
        }
        public async Task<bool> BucketExistsAsync(string bucketName)
        {
            return await _minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
        }
        public async Task CreateBucketAsync(string bucketName)
        {
            try
            {
                bool found = await BucketExistsAsync(bucketName);
                if (!found)
                {
                    await _minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
                }
            }
            catch (Exception ex)
            {
                log.Error($"Error creating bucket: {ex.Message}");
            }
        }
        public async Task RemoveBucketAsync(string bucketName)
        {
            try
            {
                bool found = await BucketExistsAsync(bucketName);
                if (found)
                {
                    await _minio.RemoveBucketAsync(new RemoveBucketArgs().WithBucket(bucketName));
                }
            }
            catch (Exception ex)
            {
                log.Error($"Error removing bucket: {ex.Message}");
            }
        }
        public async Task UploadObjectAsync(IFormFileCollection formFiles, string bucketName, string? prefix)
        {
            try
            {
                bool found = await _minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
                if (!found)
                {
                    await _minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
                }

                foreach (var postedFile in formFiles)
                {
                    if (postedFile.Length > 0)
                    {
                        var fileName = $"{Path.GetFileNameWithoutExtension(postedFile.FileName)}-{DateTime.Now:yyyyMMddHHmmssfff}{Path.GetExtension(postedFile.FileName)}";
                        var objectKey = string.IsNullOrEmpty(prefix) ? fileName : prefix + fileName;

                        using var stream = postedFile.OpenReadStream();

                        await _minio.PutObjectAsync(new PutObjectArgs()
                            .WithBucket(bucketName)
                            .WithObject(objectKey)
                            .WithStreamData(stream)
                            .WithObjectSize(stream.Length)
                            .WithContentType(postedFile.ContentType)
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"Error uploading object: {ex.Message}");
            }
        }
        public async Task UploadObjectWithPathAsync(IFormFileCollection formFiles, List<string> relativePaths, string bucketName, string? prefix)
        {
            try
            {
                bool found = await _minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));

                if (!found)
                {
                    await _minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
                }

                for (int i = 0; i < formFiles.Count; i++)
                {
                    var postedFile = formFiles[i];
                    var relativePath = relativePaths[i];

                    if (postedFile.Length > 0)
                    {
                        var directory = Path.GetDirectoryName(relativePath)?.Replace("\\", "/");
                        var fileNameWithoutExt = Path.GetFileNameWithoutExtension(relativePath);
                        var ext = Path.GetExtension(relativePath);

                        var newFileName = $"{fileNameWithoutExt}-{DateTime.Now:yyyyMMddHHmmssfff}{ext}";

                        var objectKey = string.IsNullOrEmpty(directory)
                            ? newFileName
                            : $"{directory}/{newFileName}";

                        if (!string.IsNullOrEmpty(prefix))
                        {
                            objectKey = prefix.TrimEnd('/') + "/" + objectKey;
                        }

                        using var stream = postedFile.OpenReadStream();

                        await _minio.PutObjectAsync(new PutObjectArgs()
                            .WithBucket(bucketName)
                            .WithObject(objectKey)
                            .WithStreamData(stream)
                            .WithObjectSize(stream.Length)
                            .WithContentType(postedFile.ContentType)
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"Error uploading object with folder: {ex.Message}");
            }
        }
        public async Task RemoveObjectAsync(string bucketName, string objectName)
        {
            try
            {
                await _minio.RemoveObjectAsync(new RemoveObjectArgs().WithBucket(bucketName).WithObject(objectName));
            }
            catch (Exception ex)
            {
                log.Error($"Error removing object: {ex.Message}");
            }
        }
        public async Task<string> GetPresignedObjectUrlAsync(string bucketName, string objectName, int expiresInSeconds = 7 * 24 * 3600)
        {
            try
            {
                var args = new PresignedGetObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithExpiry(expiresInSeconds);
                string url = await _minio.PresignedGetObjectAsync(args);
                return url;
            }
            catch (Exception ex)
            {
                log.Error($"Error generating presigned URL: {ex.Message}");
                return string.Empty;
            }
        }
        public async Task CopyObjectAsync(string bucketName, string sourcePrefix, string destinationPrefix, string fileName)
        {
            try
            {
                var sourceObject = string.IsNullOrEmpty(sourcePrefix) ? fileName : $"{sourcePrefix.TrimEnd('/')}/{fileName}";

                var destObject = string.IsNullOrEmpty(destinationPrefix) ? fileName : $"{destinationPrefix.TrimEnd('/')}/{fileName}";

                var copySource = new CopySourceObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(sourceObject);

                await _minio.CopyObjectAsync(new CopyObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(destObject)
                    .WithCopyObjectSource(copySource)
                );
            }
            catch (Exception ex)
            {
                log.Error($"Error copying object: {ex.Message}");
                throw;
            }
        }
        public async Task<(MemoryStream Stream, string FileName, string ContentType)> GetObjectAsync(string bucketName, string objectName)
        {
            try
            {
                var fileName = Path.GetFileName(objectName);

                var provider = new FileExtensionContentTypeProvider();
                string contentType = "application/octet-stream"; // Initialize to default value
                if (provider.TryGetContentType(fileName, out var ContentType))
                {
                    contentType = ContentType;
                }

                var memoryStream = new MemoryStream();
                await _minio.GetObjectAsync(new GetObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithCallbackStream(stream =>
                    {
                        stream.CopyTo(memoryStream);
                    })
                );
                memoryStream.Position = 0;
                return (memoryStream, fileName, contentType);
            }
            catch (Exception ex)
            {
                log.Error($"Error getting object: {ex.Message}");
                throw;
            }
        }
    }
}
