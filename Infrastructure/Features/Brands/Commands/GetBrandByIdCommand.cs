using Domain.Entities.Extend;
using MediatR;

namespace Infrastructure.Features.Brands.Commands
{
    public class GetBrandByIdCommand : IRequest<Brand>
    {
        public int Id { get; set; }
    }
}

