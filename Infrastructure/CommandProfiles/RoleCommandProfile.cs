using AutoMapper;
using Domain.Commons;
using Domain.Entities.Extend;
using Domain.Enums;
using Infrastructure.Features.Roles.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.ConstantEnums;

namespace Infrastructure.CommandProfiles
{
    internal class RoleCommandProfile : Profile
    {
        public RoleCommandProfile()
        {
            CreateMap<Role, SaveRoleCommand>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type));
            CreateMap<SaveRoleCommand, Role>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.CreatedById, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedById, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.FunctionRole, opt => opt.Ignore())
                .ForMember(dest => dest.UserRole, opt => opt.Ignore());
            CreateMap<BaseCriteria, GetRoleByPageCommand>();
            CreateMap<GetRoleByPageCommand, BaseCriteria>();

        }
    }
}

