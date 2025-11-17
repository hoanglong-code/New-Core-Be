using Domain.Entities.Extend;
using MediatR;

namespace Infrastructure.Features.Users.Commands
{
    public class GetUserByIdCommand : IRequest<User>
    {
        public int Id { get; set; }
    }
}

