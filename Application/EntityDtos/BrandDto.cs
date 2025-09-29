using Domain.Entities.Extend;
using System.Linq.Expressions;

namespace Application.EntityDtos
{
    public class BrandDto : Brand
    {
        public List<Product>? Products { get; set; }
        public static Expression<Func<Brand, BrandDto>> Expression => p => new BrandDto
        {
            Id = p.Id,
            Name = p.Name,
            Code = p.Code,
            Note = p.Note,
            Products = p.Products.ToList(),
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt,
            CreatedById = p.CreatedById,
            UpdatedById = p.UpdatedById,
            CreatedBy = p.CreatedBy,
            UpdatedBy = p.UpdatedBy,
        };
    }
}
