using Infrastructure.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.MinIO.Commands
{
    public class GetObjectsByPageCommand : IRequest<BaseSearchResponse<Minio.DataModel.Item>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 20;
        public string? Sorts { get; set; } = null;
        public string? Orders { get; set; } = string.Empty;
        public string? QueryString { get; set; } = string.Empty;
        public string BucketName { get; set; } = string.Empty;
        public string Prefix { get; set; } = string.Empty;
    }
}
