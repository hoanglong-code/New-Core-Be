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
    public class ProductProfile : Profile
    {
       public ProductProfile()
       {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}
