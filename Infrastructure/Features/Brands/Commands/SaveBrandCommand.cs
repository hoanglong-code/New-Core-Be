using Application.EntityDtos;
using Domain.Entities.Extend;
using MediatR;

namespace Infrastructure.Features.Brands.Commands
{
    public class SaveBrandCommand : IRequest<Brand>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Note { get; set; }
    }
}

