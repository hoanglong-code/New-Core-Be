using Domain.Entities.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.EntityDtos.Brands
{
    public class BrandDetailDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public string? Note { get; set; }

        public static Expression<Func<Brand, BrandDetailDto>> Expression => p => new BrandDetailDto
        {
            Id = p.Id,
            Name = p.Name,
            Code = p.Code,
            Note = p.Note,
        };
    }
}

