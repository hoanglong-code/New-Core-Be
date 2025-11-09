using Application.EntityDtos;
using MediatR;

namespace Infrastructure.Features.Functions.Commands
{
    public class GetFunctionByIdCommand : IRequest<FunctionDto>
    {
        public int Id { get; set; }
    }
}

