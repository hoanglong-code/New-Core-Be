using Domain.Entities.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.EntityDtos.Functions
{
	public class FunctionDto
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Code { get; set; }
		public string? Url { get; set; }
		public int? Location { get; set; }
		public string? FunctionParentName { get; set; }

        public static Expression<Func<Function, FunctionDto>> Expression => p => new FunctionDto
        {
            Id = p.Id,
            Name = p.Name,
            Code = p.Code,
            Url = p.Url,
            Location = p.Location,
            FunctionParentName = p.Parent != null ? p.Parent.Name : "",
        };
	}
}
