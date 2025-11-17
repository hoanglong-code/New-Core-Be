using Application.EntityDtos.Brands;
using Domain.Commons;
using Domain.Entities.Extend;
using Infrastructure.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Abstractions
{
    public interface IBrandService
    {
        public Task<BaseSearchResponse<BrandDto>> GetByPage(BaseCriteria request);
        public Task<Brand> GetById(int id);
        public Task<Brand> SaveData(Brand entity);
        public Task<Brand> DeleteData(int id);
        public Task<List<Brand>> DeleteMultipleData(string ids);
    }
}
