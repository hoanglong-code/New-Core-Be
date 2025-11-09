using Application.EntityDtos;
using Infrastructure.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Roles.Commands
{
    public class GetRoleByPageCommand : IRequest<BaseSearchResponse<RoleDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 20;
        public string? Sorts { get; set; } = null;
        public string? Orders { get; set; } = string.Empty;
        public string? QueryString { get; set; } = string.Empty;
    }
}

