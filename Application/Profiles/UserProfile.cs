using Application.EntityDtos.Users;
using AutoMapper;
using Domain.Entities.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class UserDtoToEntity : Profile
    {
        public UserDtoToEntity()
        {
            CreateMap<UserSaveDto, User>();
        }
    }
    public class UserEntityToDto : Profile
    {
        public UserEntityToDto()
        {
            CreateMap<User, UserGridDto>();
            CreateMap<User, UserDetailDto>();
        }
    }
}

