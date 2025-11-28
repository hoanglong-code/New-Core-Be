using Application.EntityDtos.Products;
using AutoMapper;
using Domain.Entities.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class ProductDtoToEntity : Profile
    {
        public ProductDtoToEntity()
        {
            CreateMap<ProductGridDto, Product>();
            CreateMap<ProductSaveDto, Product>();
        }
    }
    public class ProductEntityToDto : Profile
    {
        public ProductEntityToDto()
        {
            CreateMap<Product, ProductGridDto>();
            CreateMap<Product, ProductDetailDto>();
        }
    }
}
