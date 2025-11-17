using AutoMapper;
using Domain.Entities.Extend;
using Infrastructure.Features.Users.Commands;
using Infrastructure.Services.Abstractions;
using MediatR;

namespace Infrastructure.Features.Users.Queries
{
    public class SaveUserQuery : IRequestHandler<SaveUserCommand, User>
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        public SaveUserQuery(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<User> Handle(SaveUserCommand request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request);
            return await _service.SaveData(user);
        }
    }
}

