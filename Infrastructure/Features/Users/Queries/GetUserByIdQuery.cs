using Application.EntityDtos.Users;
using Domain.Entities.Extend;
using Infrastructure.Features.Users.Commands;
using Infrastructure.Services.Abstractions;
using MediatR;

namespace Infrastructure.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequestHandler<GetUserByIdCommand, UserDetailDto>
    {
        private readonly IUserService _service;
        public GetUserByIdQuery(IUserService service)
        {
            _service = service;
        }
        public async Task<UserDetailDto> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetById(request.Id);
        }
    }
}

