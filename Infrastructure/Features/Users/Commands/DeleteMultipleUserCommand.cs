using Domain.Entities.Extend;
using MediatR;
using System.Collections.Generic;

namespace Infrastructure.Features.Users.Commands
{
    public class DeleteMultipleUserCommand : IRequest<List<User>>
    {
        public string Ids { get; set; } = string.Empty;
    }
}

