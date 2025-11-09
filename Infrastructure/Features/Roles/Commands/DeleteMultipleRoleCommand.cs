using Application.EntityDtos;
using Domain.Entities.Extend;
using MediatR;

namespace Infrastructure.Features.Roles.Commands
{
    public class DeleteMultipleRoleCommand : IRequest<List<Role>>
    {
        public string Ids { get; set; } = string.Empty;
    }
}

