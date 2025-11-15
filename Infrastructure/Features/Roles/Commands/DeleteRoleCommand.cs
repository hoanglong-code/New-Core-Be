using Domain.Entities.Extend;
using MediatR;

namespace Infrastructure.Features.Roles.Commands
{
    public class DeleteRoleCommand : IRequest<Role>
    {
        public int Id { get; set; }
    }
}

