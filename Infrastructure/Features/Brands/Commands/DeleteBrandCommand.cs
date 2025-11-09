using Application.EntityDtos;
using MediatR;

namespace Infrastructure.Features.Brands.Commands
{
    public class DeleteBrandCommand : IRequest<BrandDto>
    {
        public int Id { get; set; }
    }
}

