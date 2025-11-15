using Domain.Entities.Extend;
using MediatR;

namespace Infrastructure.Features.Products.Commands
{
    public class DeleteProductCommand : IRequest<Product>
    {
        public int Id { get; set; }
    }
}
