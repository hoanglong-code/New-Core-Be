using Application.EntityDtos;
using MediatR;

namespace Infrastructure.Features.Products.Commands
{
    public class SaveProductCommand : IRequest<ProductDto>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Note { get; set; }
        public int BrandId { get; set; }
        public decimal Price { get; set; }
    }
}
