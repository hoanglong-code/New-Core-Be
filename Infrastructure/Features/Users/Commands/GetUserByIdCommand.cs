using Application.EntityDtos.Users;
using Domain.Entities.Extend;
using MediatR;

namespace Infrastructure.Features.Users.Commands
{
    public class GetUserByIdCommand : IRequest<UserDetailDto>
    {
        public int Id { get; set; }
    }
}

