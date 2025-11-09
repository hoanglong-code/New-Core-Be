using Application.EntityDtos;
using Domain.Entities.Extend;
using MediatR;

namespace Infrastructure.Features.Roles.Commands
{
    public class SaveRoleCommand : IRequest<Role>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Type { get; set; }
        public string? Note { get; set; }
    }
}

