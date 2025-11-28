using Application.EntityDtos.Products;
using AutoMapper;
using Domain.Commons;
using Infrastructure.Commons;
using Infrastructure.Features.Products.Commands;
using Infrastructure.Services.Abstractions;
using MediatR;

namespace Infrastructure.Features.Products.Queries
{
    public class GetProductByPageQuery : IRequestHandler<GetProductByPageCommand, BaseSearchResponse<ProductGridDto>>
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;
        public GetProductByPageQuery(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<BaseSearchResponse<ProductGridDto>> Handle(GetProductByPageCommand request, CancellationToken cancellationToken)
        {
            BaseCriteria baseCriteria = _mapper.Map<BaseCriteria>(request);
            return await _service.GetByPage(baseCriteria);
        }
    }
}
