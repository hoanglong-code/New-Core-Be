using Application.EntityDtos;
using MediatR;

namespace Infrastructure.Features.Brands.Commands
{
    public class GetBrandByIdCommand : IRequest<BrandDto>
    {
        public int Id { get; set; }
    }
}

