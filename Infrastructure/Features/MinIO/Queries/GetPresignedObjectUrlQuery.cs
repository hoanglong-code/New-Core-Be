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
    public class GetPresignedObjectUrlQuery : IRequestHandler<GetPresignedObjectUrlCommand, string>
    {
        private readonly IMinioService _service;
        public GetPresignedObjectUrlQuery(IMinioService service)
        {
            _service = service;
        }
        public async Task<string> Handle(GetPresignedObjectUrlCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetPresignedObjectUrlAsync(request.BucketName, request.ObjectName, request.ExpiryInSeconds);
        }
    }
}
