using Application.Data;
using AutoMapper;
using Domain.Commons;
using Domain.Entities.Extend;
using Infrastructure.Features.Users.Commands;

namespace Infrastructure.CommandProfiles
{
    internal class UserCommandProfile : Profile
    {
        public UserCommandProfile()
        {
            CreateMap<User, SaveUserCommand>();
            CreateMap<SaveUserCommand, User>()
                .ForMember(dest => dest.CreatedById, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedById, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
            CreateMap<BaseCriteria, GetUserByPageCommand>();
            CreateMap<GetUserByPageCommand, BaseCriteria>();
            CreateMap<LoginRequest, LoginCommand>();
            CreateMap<LoginCommand, LoginRequest>();
        }
    }
}

