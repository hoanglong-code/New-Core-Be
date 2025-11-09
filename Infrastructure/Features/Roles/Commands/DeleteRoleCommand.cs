using Application.EntityDtos;
using MediatR;

namespace Infrastructure.Features.Roles.Commands
{
    public class DeleteRoleCommand : IRequest<RoleDto>
    {
        public int Id { get; set; }
    }
}

