using Application.EntityDtos;
using Domain.Entities.Extend;
using MediatR;

namespace Infrastructure.Features.Functions.Commands
{
    public class DeleteMultipleFunctionCommand : IRequest<List<Function>>
    {
        public string Ids { get; set; } = string.Empty;
    }
}

