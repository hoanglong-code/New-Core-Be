using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.MinIO.Commands
{
    public class UploadObjectCommand : IRequest<bool>
    {
        public IFormFileCollection FormFiles { get; set; } = null!;
        public string BucketName { get; set; } = string.Empty;
        public string? Prefix { get; set; }
    }
}
