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
    public class UploadObjectQuery: IRequestHandler<UploadObjectCommand, bool>
    {
        private readonly IMinioService _minioService;
        public UploadObjectQuery(IMinioService minioService)
        {
            _minioService = minioService;
        }
        public async Task<bool> Handle(UploadObjectCommand request, CancellationToken cancellationToken)
        {
            return await _minioService.UploadObjectAsync(request.FormFiles, request.BucketName, request.Prefix);
        }
    }
}
