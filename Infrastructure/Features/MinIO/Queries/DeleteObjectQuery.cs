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
    public class DeleteObjectQuery : IRequestHandler<DeleteObjectCommand, bool>
    {
        private readonly IMinioService _service;
        public DeleteObjectQuery(IMinioService service)
        {
            _service = service;
        }
        public async Task<bool> Handle(DeleteObjectCommand request, CancellationToken cancellationToken)
        {
            return await _service.RemoveObjectAsync(request.BucketName, request.ObjectName);
        }
    }
}
