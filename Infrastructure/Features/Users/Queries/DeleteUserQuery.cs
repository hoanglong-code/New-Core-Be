using Domain.Entities.Extend;
using Infrastructure.Features.Users.Commands;
using Infrastructure.Services.Abstractions;
using MediatR;

namespace Infrastructure.Features.Users.Queries
{
    public class DeleteUserQuery : IRequestHandler<DeleteUserCommand, User>
    {
        private readonly IUserService _service;
        public DeleteUserQuery(IUserService service)
        {
            _service = service;
        }
        public async Task<User> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteData(request.Id);
        }
    }
}

