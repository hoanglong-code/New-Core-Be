using AutoMapper;
using Domain.Commons;
using Domain.Entities.Extend;
using Infrastructure.Features.Functions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CommandProfiles
{
    internal class FunctionCommandProfile : Profile
    {
        public FunctionCommandProfile()
        {
            CreateMap<Function, SaveFunctionCommand>();
            CreateMap<SaveFunctionCommand, Function>()
                .ForMember(dest => dest.CreatedById, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedById, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Parent, opt => opt.Ignore())
                .ForMember(dest => dest.Children, opt => opt.Ignore())
                .ForMember(dest => dest.FunctionRole, opt => opt.Ignore());
            CreateMap<BaseCriteria, GetFunctionByPageCommand>();
            CreateMap<GetFunctionByPageCommand, BaseCriteria>();

        }
    }
}

