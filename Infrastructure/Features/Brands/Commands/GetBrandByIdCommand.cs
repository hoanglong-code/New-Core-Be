using Application.EntityDtos.Brands;
using Domain.Entities.Extend;
using MediatR;

namespace Infrastructure.Features.Brands.Commands
{
    public class GetBrandByIdCommand : IRequest<BrandDetailDto>
    {
        public int Id { get; set; }
    }
}

