using Application.EntityDtos.Functions;
using AutoMapper;
using Domain.Entities.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class FunctionDtoToEntity : Profile
    {
        public FunctionDtoToEntity()
        {
            CreateMap<FunctionGridDto, Function>();
            CreateMap<FunctionSaveDto, Function>();
        }
    }
    public class FunctionEntityToDto : Profile
    {
        public FunctionEntityToDto()
        {
            CreateMap<Function, FunctionGridDto>();
            CreateMap<Function, FunctionDetailDto>();
        }
    }
}

