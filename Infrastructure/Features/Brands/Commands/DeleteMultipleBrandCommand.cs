using Application.EntityDtos;
using Domain.Entities.Extend;
using MediatR;

namespace Infrastructure.Features.Brands.Commands
{
    public class DeleteMultipleBrandCommand : IRequest<List<Brand>>
    {
        public string Ids { get; set; } = string.Empty;
    }
}

