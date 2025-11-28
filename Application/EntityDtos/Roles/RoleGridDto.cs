using Domain.Entities.Extend;
using System;
using System.Linq.Expressions;

namespace Application.EntityDtos.Roles
{
    public class RoleGridDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int Type { get; set; }
        public string? Note { get; set; }
        public DateTime? CreatedAt { get; set; }

        public static Expression<Func<Role, RoleGridDto>> Expression => p => new RoleGridDto
        {
            Id = p.Id,
            Name = p.Name,
            Code = p.Code,
            Type = (int)p.Type,
            Note = p.Note,
            CreatedAt = p.CreatedAt,
        };
    }
}

