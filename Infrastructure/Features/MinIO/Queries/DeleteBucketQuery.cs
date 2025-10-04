using Domain.Entities.Extend;
using Infrastructure.Features.MinIO.Commands;
using Infrastructure.Features.Products.Commands;
using MediatR;
using Minio.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.MinIO.Queries
{
    public class DeleteBucketQuery : IRequestHandler<DeleteBucketCommand, bool>
    {
        private readonly IMinioService _service;
        public DeleteBucketQuery(IMinioService service)
        {
            _service = service;
        }
        public async Task<bool> Handle(DeleteBucketCommand request, CancellationToken cancellationToken)
        {
            return await _service.RemoveBucketAsync(request.BucketName);
        }
    }
}
