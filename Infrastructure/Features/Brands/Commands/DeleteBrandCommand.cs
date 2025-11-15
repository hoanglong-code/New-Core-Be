using Domain.Entities.Extend;
using MediatR;

namespace Infrastructure.Features.Brands.Commands
{
    public class DeleteBrandCommand : IRequest<Brand>
    {
        public int Id { get; set; }
    }
}

