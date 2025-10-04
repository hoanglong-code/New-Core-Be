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
    public class CreateBucketQuery : IRequestHandler<CreateBucketCommand, bool>
    {
        private readonly IMinioService _service;
        public CreateBucketQuery(IMinioService service)
        {
            _service = service;
        }
        public async Task<bool> Handle(CreateBucketCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateBucketAsync(request.BucketName);
        }
    }
}
