using Application.Contexts.Abstractions;
using Application.IReponsitories.Base;
using Domain.Entities.Base;
using Domain.Enums;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.Base
{
    public class AsyncGenericRepository<TEntity, TId> : IAsyncGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        protected AppDbContext _dbContext { get; private set; }
        protected DbSet<TEntity> _dbSet;
        private readonly IUserContext _userContext;
        protected AsyncGenericRepository(AppDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
            _userContext = userContext;
        }
        public IQueryable<TEntity> All(bool skipDeleted = true)
        {
            var query = _dbSet.AsNoTracking();
            if (!skipDeleted)
                query = query.IgnoreQueryFilters();
            return query;
        }
        public IQueryable<TEntity> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }
        public virtual async Task AddAsync(TEntity e)
        {
            e.CreatedBy = _userContext.userClaims.fullName;
            e.CreatedById = _userContext.userClaims.userId;
            e.CreatedAt = DateTime.Now;
            e.UpdatedAt = DateTime.Now;
            await _dbSet.AddAsync(e);
        }
        public virtual void Add(TEntity e)
        {
            e.CreatedBy = _userContext.userClaims.fullName;
            e.CreatedById = _userContext.userClaims.userId;
            e.CreatedAt = DateTime.Now;
            e.UpdatedAt = DateTime.Now;
            _dbSet.Add(e);
        }
        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (TEntity e in entities)
            {
                e.CreatedBy = _userContext.userClaims.fullName;
                e.CreatedById = _userContext.userClaims.userId;
                e.CreatedAt = DateTime.Now;
                e.UpdatedAt = DateTime.Now;
            }
            await _dbSet.AddRangeAsync(entities);
        }
        public virtual async Task<TEntity?> FindAsync(TId id)
        {
            return await _dbSet.FindAsync(id);
        }
        public virtual void Remove(TEntity e)
        {
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Remove(e);
        }
        public virtual void RemoveSoft(TEntity e)
        {
            e.Status = ConstantEnums.EntityStatus.DELETED;
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Update(e);
            _dbSet.Entry(e).State = EntityState.Modified;
        }
        public void Update(TEntity e)
        {
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Update(e);
            _dbSet.Entry(e).State = EntityState.Modified;
        }
        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (TEntity e in entities)
            {
                e.UpdatedAt = DateTime.Now;
                e.UpdatedBy = _userContext.userClaims.fullName;
                e.UpdatedById = _userContext.userClaims.userId;
            }
            _dbSet.UpdateRange(entities);
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (TEntity e in entities)
            {
                e.UpdatedAt = DateTime.Now;
                e.UpdatedBy = _userContext.userClaims.fullName;
                e.UpdatedById = _userContext.userClaims.userId;
            }
            _dbSet.RemoveRange(entities);
        }
        public void RemoveSoftRange(IEnumerable<TEntity> entities)
        {
            foreach (TEntity e in entities)
            {
                e.Status = ConstantEnums.EntityStatus.DELETED;
                e.UpdatedAt = DateTime.Now;
                e.UpdatedBy = _userContext.userClaims.fullName;
                e.UpdatedById = _userContext.userClaims.userId;
            }
            _dbSet.UpdateRange(entities);
        }
        public void LockEntity(TEntity e)
        {
            e.Status = ConstantEnums.EntityStatus.LOCK;
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Update(e);
            _dbSet.Entry(e).State = EntityState.Modified;
        }
        public void UnLockEntity(TEntity e)
        {
            e.Status = ConstantEnums.EntityStatus.NORMAL;
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Update(e);
            _dbSet.Entry(e).State = EntityState.Modified;
        }
        public async Task<TEntity> GetByKeyAsync(TId keyValue, bool skipDeleted = true)
        {
            if (skipDeleted)
                return await _dbSet.AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(keyValue));
            else
                return await _dbSet.AsNoTracking().IgnoreQueryFilters().SingleOrDefaultAsync(x => x.Id.Equals(keyValue));
        }
    }
}
