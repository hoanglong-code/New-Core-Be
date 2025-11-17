using Domain.Entities.Extend;
using Infrastructure.Features.Users.Commands;
using Infrastructure.Services.Abstractions;
using MediatR;

namespace Infrastructure.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequestHandler<GetUserByIdCommand, User>
    {
        private readonly IUserService _service;
        public GetUserByIdQuery(IUserService service)
        {
            _service = service;
        }
        public async Task<User> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetById(request.Id);
        }
    }
}

