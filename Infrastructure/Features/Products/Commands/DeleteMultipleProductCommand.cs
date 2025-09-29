using Application.EntityDtos;
using Domain.Entities.Extend;
using MediatR;

namespace Infrastructure.Features.Products.Commands
{
    public class DeleteMultipleProductCommand : IRequest<List<Product>>
    {
        public string Ids { get; set; } = string.Empty;
    }
}
