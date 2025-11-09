using Application.EntityDtos;
using MediatR;

namespace Infrastructure.Features.Roles.Commands
{
    public class GetRoleByIdCommand : IRequest<RoleDto>
    {
        public int Id { get; set; }
    }
}

