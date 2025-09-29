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
        Task<TEntity?> FindAsync(TId id);
        void Remove(TEntity e);
        void RemoveSoft(TEntity e);
        void Update(TEntity e);
        void UpdateRange(IEnumerable<TEntity> entities);
        void RemoveRange(IEnumerable<TEntity> entities);
        void RemoveSoftRange(IEnumerable<TEntity> entities);
        void LockEntity(TEntity e);
        void UnLockEntity(TEntity e);
        Task<TEntity> GetByKeyAsync(TId keyValue, bool skipDeleted = true);
    }
}
