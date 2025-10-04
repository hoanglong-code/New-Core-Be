using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.MinIO.Commands
{
    public class CreateBucketCommand : IRequest<bool>
    {
        public string BucketName { get; set; } = string.Empty;
    }
}
