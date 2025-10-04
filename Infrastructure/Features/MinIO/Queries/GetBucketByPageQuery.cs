using AutoMapper;
using Domain.Commons;
using Domain.Entities.Extend;
using Infrastructure.Commons;
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
    public class GetBucketByPageQuery : IRequestHandler<GetBucketByPageCommand, BaseSearchResponse<Minio.DataModel.Bucket>>
    {
        private readonly IMinioService _service;
        private readonly IMapper _mapper;
        public GetBucketByPageQuery(IMinioService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<BaseSearchResponse<Minio.DataModel.Bucket>> Handle(GetBucketByPageCommand request, CancellationToken cancellationToken)
        {
            BaseCriteria baseCriteria = _mapper.Map<BaseCriteria>(request);
            return await _service.ListBucketByPageAsync(baseCriteria);
        }
    }
}
