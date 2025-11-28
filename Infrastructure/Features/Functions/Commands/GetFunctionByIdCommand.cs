using Application.EntityDtos.Functions;
using Domain.Entities.Extend;
using MediatR;

namespace Infrastructure.Features.Functions.Commands
{
    public class GetFunctionByIdCommand : IRequest<FunctionDetailDto>
    {
        public int Id { get; set; }
    }
}

