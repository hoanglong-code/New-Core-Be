using Application.Data;
using AutoMapper;
using Infrastructure.Commons;
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
    public class GetObjectsByPageQuery : IRequestHandler<GetObjectsByPageCommand, BaseSearchResponse<Minio.DataModel.Item>>
    {
        private readonly IMinioService _service;
        private readonly IMapper _mapper;
        public GetObjectsByPageQuery(IMinioService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<BaseSearchResponse<Minio.DataModel.Item>> Handle(GetObjectsByPageCommand request, CancellationToken cancellationToken)
        {
            MinioCriteria minioCriteria = _mapper.Map<MinioCriteria>(request);
            return await _service.ListObjectsByPageAsync(minioCriteria);
        }
    }
}
