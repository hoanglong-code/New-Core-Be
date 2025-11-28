using Domain.Entities.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.EntityDtos.Functions
{
    public class FunctionDetailDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public int? FunctionParentId { get; set; }
        public string? Url { get; set; }
        public string? Note { get; set; }
        public int? Location { get; set; }
        public string? Icon { get; set; }

        public static Expression<Func<Function, FunctionDetailDto>> Expression => p => new FunctionDetailDto
        {
            Id = p.Id,
            Name = p.Name,
            Code = p.Code,
            FunctionParentId = p.FunctionParentId,
            Url = p.Url,
            Note = p.Note,
            Location = p.Location,
            Icon = p.Icon,
        };
    }
}
