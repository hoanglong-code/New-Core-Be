using Application.EntityDtos.Brands;
using AutoMapper;
using Domain.Commons;
using Infrastructure.Commons;
using Infrastructure.Features.Brands.Commands;
using Infrastructure.Services.Abstractions;
using MediatR;

namespace Infrastructure.Features.Brands.Queries
{
    public class GetBrandByPageQuery : IRequestHandler<GetBrandByPageCommand, BaseSearchResponse<BrandGridDto>>
    {
        private readonly IBrandService _service;
        private readonly IMapper _mapper;
        public GetBrandByPageQuery(IBrandService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<BaseSearchResponse<BrandGridDto>> Handle(GetBrandByPageCommand request, CancellationToken cancellationToken)
        {
            BaseCriteria baseCriteria = _mapper.Map<BaseCriteria>(request);
            return await _service.GetByPage(baseCriteria);
        }
    }
}

