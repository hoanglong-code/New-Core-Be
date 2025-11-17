using Domain.Entities.Extend;
using Infrastructure.Features.Users.Commands;
using Infrastructure.Services.Abstractions;
using MediatR;
using System.Collections.Generic;

namespace Infrastructure.Features.Users.Queries
{
    public class DeleteMultipleUserQuery : IRequestHandler<DeleteMultipleUserCommand, List<User>>
    {
        private readonly IUserService _service;
        public DeleteMultipleUserQuery(IUserService service)
        {
            _service = service;
        }
        public async Task<List<User>> Handle(DeleteMultipleUserCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteMultipleData(request.Ids);
        }
    }
}

