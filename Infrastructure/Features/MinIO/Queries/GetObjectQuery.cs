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
    public class GetObjectQuery : IRequestHandler<GetObjectCommand, (MemoryStream Stream, string FileName, string ContentType)>
    {
        private readonly IMinioService _service;
        public GetObjectQuery(IMinioService service)
        {
            _service = service;
        }
        public async Task<(MemoryStream Stream, string FileName, string ContentType)> Handle(GetObjectCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetObjectAsync(request.BucketName, request.ObjectName);
        }
    }
}
