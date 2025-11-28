using Domain.Entities.Extend;
using System;
using System.Linq.Expressions;

namespace Application.EntityDtos.Brands
{
    public class BrandGridDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime? CreatedAt { get; set; }

        public static Expression<Func<Brand, BrandGridDto>> Expression => p => new BrandGridDto
        {
            Id = p.Id,
            Name = p.Name,
            Code = p.Code,
            CreatedAt = p.CreatedAt,
        };
    }
}
