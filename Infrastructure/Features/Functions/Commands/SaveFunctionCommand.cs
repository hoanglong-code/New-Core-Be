using Application.EntityDtos;
using Domain.Entities.Extend;
using MediatR;

namespace Infrastructure.Features.Functions.Commands
{
    public class SaveFunctionCommand : IRequest<Function>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int? FunctionParentId { get; set; }
        public string? Url { get; set; }
        public string? Note { get; set; }
        public int? Location { get; set; }
        public string? Icon { get; set; }
    }
}

