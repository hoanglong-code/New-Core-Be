using Domain.Entities.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.ConstantEnums;

namespace Application.EntityDtos.Roles
{
    public class RoleDetailDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public TypeRole Type { get; set; }
        public string? Note { get; set; }

        public static Expression<Func<Role, RoleDetailDto>> Expression => p => new RoleDetailDto
        {
            Id = p.Id,
            Name = p.Name,
            Code = p.Code,
            Type = p.Type,
            Note = p.Note,
        };
    }
}

