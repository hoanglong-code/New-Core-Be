using Application.EntityDtos.Roles;
using AutoMapper;
using Domain.Entities.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class RoleDtoToEntity : Profile
    {
        public RoleDtoToEntity()
        {
            CreateMap<RoleGridDto, Role>();
            CreateMap<RoleSaveDto, Role>();
        }
    }
    public class RoleEntityToDto : Profile
    {
        public RoleEntityToDto()
        {
            CreateMap<Role, RoleGridDto>();
            CreateMap<Role, RoleDetailDto>();
        }
    }
}

