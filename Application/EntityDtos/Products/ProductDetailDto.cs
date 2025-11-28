using Domain.Entities.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.EntityDtos.Products
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public string? Note { get; set; }
        public int? BrandId { get; set; }
        public decimal Price { get; set; }

        public static Expression<Func<Product, ProductDetailDto>> Expression => p => new ProductDetailDto
        {
            Id = p.Id,
            Name = p.Name,
            Code = p.Code,
            Note = p.Note,
            BrandId = p.BrandId,
            Price = p.Price,
        };
    }
}

