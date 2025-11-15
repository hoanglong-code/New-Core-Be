using Application.EntityDtos;
using Domain.Entities.Extend;
using MediatR;
using static Domain.Enums.ConstantEnums;

namespace Infrastructure.Features.Roles.Commands
{
    public class SaveRoleCommand : IRequest<Role>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public TypeRole Type { get; set; }
        public string? Note { get; set; }
    }
}

