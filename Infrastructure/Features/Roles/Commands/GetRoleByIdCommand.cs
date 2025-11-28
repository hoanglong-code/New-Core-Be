using Application.EntityDtos.Roles;
using Domain.Entities.Extend;
using MediatR;

namespace Infrastructure.Features.Roles.Commands
{
    public class GetRoleByIdCommand : IRequest<RoleDetailDto>
    {
        public int Id { get; set; }
    }
}

