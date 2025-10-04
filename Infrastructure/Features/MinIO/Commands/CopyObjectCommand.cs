using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.MinIO.Commands
{
    public class CopyObjectCommand : IRequest<bool>
    {
        public string BucketName { get; set; } = string.Empty; 
        public string SourcePrefix { get; set; } = string.Empty;
        public string DestinationPrefix { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
    }
}
