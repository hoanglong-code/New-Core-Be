using Application.EntityDtos;
using AutoMapper;
using Domain.Commons;
using Infrastructure.Commons;
using Infrastructure.Features.Functions.Commands;
using Infrastructure.Services.Abstractions;
using MediatR;

namespace Infrastructure.Features.Functions.Queries
{
    public class GetFunctionByPageQuery : IRequestHandler<GetFunctionByPageCommand, BaseSearchResponse<FunctionDto>>
    {
        private readonly IFunctionService _service;
        private readonly IMapper _mapper;
        public GetFunctionByPageQuery(IFunctionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<BaseSearchResponse<FunctionDto>> Handle(GetFunctionByPageCommand request, CancellationToken cancellationToken)
        {
            BaseCriteria baseCriteria = _mapper.Map<BaseCriteria>(request);
            return await _service.GetByPage(baseCriteria);
        }
    }
}

