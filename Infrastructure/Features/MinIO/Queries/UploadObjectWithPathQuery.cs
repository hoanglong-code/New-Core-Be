using Infrastructure.Features.MinIO.Commands;
using MediatR;
using Minio.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.MinIO.Queries
{
    public class UploadObjectWithPathQuery : IRequestHandler<UploadObjectWithPathCommand, bool>
    {
        private readonly IMinioService _minioService;
        public UploadObjectWithPathQuery(IMinioService minioService)
        {
            _minioService = minioService;
        }
        public async Task<bool> Handle(UploadObjectWithPathCommand request, CancellationToken cancellationToken)
        {
            return await _minioService.UploadObjectWithPathAsync(request.FormFiles, request.RelativePaths, request.BucketName, request.Prefix);
        }
    }
}
