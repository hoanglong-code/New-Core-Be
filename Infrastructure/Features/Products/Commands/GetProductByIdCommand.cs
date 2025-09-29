using Application.EntityDtos;
using MediatR;

namespace Infrastructure.Features.Products.Commands
{
    public class GetProductByIdCommand : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }
}
