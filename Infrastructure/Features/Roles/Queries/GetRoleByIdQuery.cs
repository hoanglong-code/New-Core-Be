using Application.EntityDtos;
using Application.EntityDtos.Roles;
using Domain.Entities.Extend;
using Infrastructure.Features.Roles.Commands;
using Infrastructure.Services.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Roles.Queries
{
    public class GetRoleByIdQuery : IRequestHandler<GetRoleByIdCommand, RoleDetailDto>
    {
        private readonly IRoleService _service;
        public GetRoleByIdQuery(IRoleService service)
        {
            _service = service;
        }
        public async Task<RoleDetailDto> Handle(GetRoleByIdCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetById(request.Id);
        }
    }
}

