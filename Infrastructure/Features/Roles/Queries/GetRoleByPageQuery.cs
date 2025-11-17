using Application.EntityDtos.Roles;
using AutoMapper;
using Domain.Commons;
using Infrastructure.Commons;
using Infrastructure.Features.Roles.Commands;
using Infrastructure.Services.Abstractions;
using MediatR;

namespace Infrastructure.Features.Roles.Queries
{
    public class GetRoleByPageQuery : IRequestHandler<GetRoleByPageCommand, BaseSearchResponse<RoleDto>>
    {
        private readonly IRoleService _service;
        private readonly IMapper _mapper;
        public GetRoleByPageQuery(IRoleService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<BaseSearchResponse<RoleDto>> Handle(GetRoleByPageCommand request, CancellationToken cancellationToken)
        {
            BaseCriteria baseCriteria = _mapper.Map<BaseCriteria>(request);
            return await _service.GetByPage(baseCriteria);
        }
    }
}

