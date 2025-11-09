using Application.EntityDtos;
using AutoMapper;
using Domain.Entities.Extend;
using Infrastructure.Features.Roles.Commands;
using Infrastructure.Services.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Roles.Queries
{
    public class SaveRoleQuery : IRequestHandler<SaveRoleCommand, Role>
    {
        private readonly IRoleService _service;
        private readonly IMapper _mapper;
        public SaveRoleQuery(IRoleService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<Role> Handle(SaveRoleCommand request, CancellationToken cancellationToken)
        {
            Role role = _mapper.Map<Role>(request);
            return await _service.SaveData(role);
        }
    }
}

