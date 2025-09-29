using Application.EntityDtos;
using MediatR;

namespace Infrastructure.Features.Products.Commands
{
    public class DeleteProductCommand : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }
}
