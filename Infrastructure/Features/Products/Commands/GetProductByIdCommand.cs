using Application.EntityDtos;
using Domain.Entities.Extend;
using MediatR;

namespace Infrastructure.Features.Products.Commands
{
    public class GetProductByIdCommand : IRequest<Product>
    {
        public int Id { get; set; }
    }
}
