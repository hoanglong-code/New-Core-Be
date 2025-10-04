using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.MinIO.Commands
{
    public class GetPresignedObjectUrlCommand : IRequest<string>
    {
        public string BucketName { get; set; } = string.Empty;
        public string ObjectName { get; set; } = string.Empty;
        public int ExpiryInSeconds { get; set; }
    }
}
