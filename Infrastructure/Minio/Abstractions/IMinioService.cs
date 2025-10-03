using Application.Data;
using Domain.Commons;
using Infrastructure.Commons;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minio.Abstractions
{
    public interface IMinioService
    {
        Task<BaseSearchResponse<Minio.DataModel.Bucket>> ListBucketByPageAsync(BaseCriteria request);
        Task<List<Minio.DataModel.Bucket>> ListAllBucketsAsync();
        Task<BaseSearchResponse<Minio.DataModel.Item>> ListObjectsByPageAsync(MinioCriteria request);
        Task<List<Minio.DataModel.Item>> ListAllObjectsAsync(string bucketName, string? prefix);
        Task<bool> BucketExistsAsync(string bucketName);
        Task CreateBucketAsync(string bucketName);
        Task RemoveBucketAsync(string bucketName);
        Task UploadObjectAsync(IFormFileCollection formFiles, string bucketName, string? prefix);
        Task UploadObjectWithPathAsync(IFormFileCollection formFiles, List<string> relativePaths, string bucketName, string? prefix);
        Task RemoveObjectAsync(string bucketName, string objectName);
        Task<string> GetPresignedObjectUrlAsync(string bucketName, string objectName, int expiresInSeconds);
        Task CopyObjectAsync(string bucketName, string sourcePrefix, string destinationPrefix, string fileName);
        Task<(MemoryStream Stream, string FileName, string ContentType)> GetObjectAsync(string bucketName, string objectName);
    }
}
