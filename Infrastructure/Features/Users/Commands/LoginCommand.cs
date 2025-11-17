using Application.Data;
using Domain.Entities.Extend;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Users.Commands
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
