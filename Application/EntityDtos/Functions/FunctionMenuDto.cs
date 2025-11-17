using Application.EntityDtos.Users;
using Domain.Commons;
using Domain.Entities.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.EntityDtos.Functions
{
    public class FunctionMenuDto
    {
        public required string Name { get; set; }
        public int? FunctionParentId { get; set; }
        public string? Url { get; set; }
        public int? Location { get; set; }
        public string? Icon { get; set; }

        public static Expression<Func<Function, FunctionMenuDto>> Expression => p => new FunctionMenuDto
        {
            Name = p.Name,
            FunctionParentId = p.FunctionParentId,
            Url = p.Url,
            Location = p.Location,
            Icon = p.Icon
        };
    }
}
