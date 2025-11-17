using Domain.Entities.Extend;
using MediatR;

namespace Infrastructure.Features.Users.Commands
{
    public class DeleteUserCommand : IRequest<User>
    {
        public int Id { get; set; }
    }
}

