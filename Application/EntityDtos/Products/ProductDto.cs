using Domain.Entities.Extend;
using System;
using System.Linq.Expressions;

namespace Application.EntityDtos.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public decimal Price { get; set; }
        public string? BrandName { get; set; }

        public static Expression<Func<Product, ProductDto>> Expression => p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Code = p.Code,
            Price = p.Price,
            BrandName = p.Brand != null ? p.Brand.Name : "",
        };
    }
}
