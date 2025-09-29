using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IReponsitories.Base
{
    public interface IAsyncGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        IQueryable<TEntity> All(bool skipDeleted = true);
        IQueryable<TEntity> AsQueryable();
        Task AddAsync(TEntity e);
        void Add(TEntity e);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task AddWithElasticSearchAsync(TEntity e);
        Task AddRangeWithElasticSearchAsync(IEnumerable<TEntity> entities);
        Task<TEntity?> FindAsync(TId id);
        void Update(TEntity e);
        Task UpdateAsync(TEntity e);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateWithElasticSearchAsync(TEntity e);
        Task UpdateRangeWithElasticSearchAsync(IEnumerable<TEntity> entities);
        void Remove(TEntity e);
        Task RemoveAsync(TEntity e);
        void RemoveSoft(TEntity e);
        Task RemoveSoftAsync(TEntity e);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task RemoveRangeAsync(IEnumerable<TEntity> entities);
        void RemoveSoftRange(IEnumerable<TEntity> entities);
        Task RemoveSoftRangeAsync(IEnumerable<TEntity> entities);
        Task RemoveWithElasticSearchAsync(TEntity e);
        Task RemoveSoftWithElasticSearchAsync(TEntity e);
        Task RemoveRangeWithElasticSearchAsync(IEnumerable<TEntity> entities);
        Task RemoveSoftRangeWithElasticSearchAsync(IEnumerable<TEntity> entities);
        void LockEntity(TEntity e);
        void UnLockEntity(TEntity e);
        Task<TEntity> GetByKeyAsync(TId keyValue, bool skipDeleted = true);
    }
}
