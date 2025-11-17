using Application.Data;
using AutoMapper;
using Domain.Entities.Extend;
using Infrastructure.Features.Users.Commands;
using Infrastructure.Services.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Users.Queries
{
    public class LoginQuery : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly ILoginService _service;
        private readonly IMapper _mapper;
        public LoginQuery(ILoginService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            LoginRequest loginRequest = _mapper.Map<LoginRequest>(request);
            return await _service.Login(loginRequest);
        }
    }
}
