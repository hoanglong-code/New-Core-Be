using Application.EntityDtos.Brands;
using AutoMapper;
using Domain.Entities.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class BrandDtoToEntity : Profile
    {
        public BrandDtoToEntity()
        {
            CreateMap<BrandGridDto, Brand>();
            CreateMap<BrandSaveDto, Brand>();
        }
    }
    public class BrandEntityToDto : Profile
    {
        public BrandEntityToDto()
        {
            CreateMap<Brand, BrandGridDto>();
            CreateMap<Brand, BrandDetailDto>();
        }
    }
}
