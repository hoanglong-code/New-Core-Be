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
        Task<bool> CreateBucketAsync(string bucketName);
        Task<bool> RemoveBucketAsync(string bucketName);
        Task<bool> UploadObjectAsync(IFormFileCollection formFiles, string bucketName, string? prefix);
        Task<bool> UploadObjectWithPathAsync(IFormFileCollection formFiles, List<string> relativePaths, string bucketName, string? prefix);
        Task<bool> RemoveObjectAsync(string bucketName, string objectName);
        Task<string> GetPresignedObjectUrlAsync(string bucketName, string objectName, int expiresInSeconds);
        Task<bool> CopyObjectAsync(string bucketName, string sourcePrefix, string destinationPrefix, string fileName);
        Task<(MemoryStream Stream, string FileName, string ContentType)> GetObjectAsync(string bucketName, string objectName);
    }
}
