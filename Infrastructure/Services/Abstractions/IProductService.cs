using Application.EntityDtos;
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
    public interface IProductService
    {
        public Task<BaseSearchResponse<ProductDto>> GetByPage(BaseCriteria request);
        public Task<Product> GetById(int id);
        public Task<Product> SaveData(Product entity);
        public Task<Product> DeleteData(int id);
        public Task<List<Product>> DeleteMultipleData(string ids);

        //public Task IndexAsync(Product entity, string? indexName = null);
        //public Task<Product?> GetByIdAsync(int id, string? indexName = null);
        //public Task<IEnumerable<Product>> SearchAsync(string fieldName, string searchTerm, string? indexName = null);
        //public Task<bool> DeleteAsync(int id, string? indexName = null);
    }
}
