using Application.EntityDtos;
using AutoMapper;
using Domain.Entities.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class FunctionProfile : Profile
    {
        public FunctionProfile()
        {
            CreateMap<Function, FunctionDto>();
            CreateMap<FunctionDto, Function>();
        }
    }
}

