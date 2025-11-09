using Domain.Entities.Extend;
using System;
using System.Linq.Expressions;

namespace Application.EntityDtos
{
    public class BrandDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

        public static Expression<Func<Brand, BrandDto>> Expression => p => new BrandDto
        {
            Id = p.Id,
            Name = p.Name,
            Code = p.Code,
        };
    }
}
