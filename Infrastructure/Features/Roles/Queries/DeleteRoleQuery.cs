using Application.EntityDtos;
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
    public class DeleteRoleQuery : IRequestHandler<DeleteRoleCommand, Role>
    {
        private readonly IRoleService _service;
        public DeleteRoleQuery(IRoleService service)
        {
            _service = service;
        }
        public async Task<Role> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteData(request.Id);
        }
    }
}

