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
    public class DeleteMultipleRoleQuery : IRequestHandler<DeleteMultipleRoleCommand, List<Role>>
    {
        private readonly IRoleService _service;
        public DeleteMultipleRoleQuery(IRoleService service)
        {
            _service = service;
        }
        public async Task<List<Role>> Handle(DeleteMultipleRoleCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteMultipleData(request.Ids);
        }
    }
}

