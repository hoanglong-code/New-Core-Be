using Application.IReponsitories.Base;
using Domain.Entities.Extend;

namespace Application.IReponsitories.Abstractions
{
    public interface IProductRepository : IAsyncGenericRepository<Product, int>
    {
    }
}
