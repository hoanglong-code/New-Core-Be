using Domain.Entities.Extend;
using System;
using System.Linq.Expressions;

namespace Application.EntityDtos
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int Type { get; set; }
        public string? Note { get; set; }
        public DateTime? CreatedAt { get; set; }

        public static Expression<Func<Role, RoleDto>> Expression => p => new RoleDto
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

