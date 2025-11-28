using Application.EntityDtos;
using Application.EntityDtos.Products;
using Domain.Entities.Extend;
using MediatR;

namespace Infrastructure.Features.Products.Commands
{
    public class GetProductByIdCommand : IRequest<ProductDetailDto>
    {
        public int Id { get; set; }
    }
}
