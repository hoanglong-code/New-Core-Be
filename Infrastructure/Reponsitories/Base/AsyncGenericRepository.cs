using Application.Contexts.Abstractions;
using Application.IReponsitories.Base;
using Domain.Entities.Base;
using Domain.Entities.Extend;
using Domain.Enums;
using Elastic.Clients.Elasticsearch;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Infrastructure.Reponsitories.Base
{
    public class AsyncGenericRepository<TEntity, TId> : IAsyncGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        protected AppDbContext _dbContext { get; private set; }
        protected DbSet<TEntity> _dbSet;
        private readonly IUserContext _userContext;
        private readonly ElasticsearchClient _elasticClient;
        protected AsyncGenericRepository(AppDbContext dbContext, IUserContext userContext, ElasticsearchClient elasticsearchClient)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
            _userContext = userContext;
            _elasticClient = elasticsearchClient;
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
        public virtual async Task AddWithElasticSearchAsync(TEntity e)
        {
            e.CreatedBy = _userContext.userClaims.fullName;
            e.CreatedById = _userContext.userClaims.userId;
            e.CreatedAt = DateTime.Now;
            e.UpdatedAt = DateTime.Now;
            await _dbSet.AddAsync(e);
            // Index vào Elasticsearch
            await _elasticClient.IndexAsync(e, idx => idx.Index(typeof(TEntity).Name.ToLower()).Id(e.Id.ToString()));
        }
        public virtual async Task AddRangeWithElasticSearchAsync(IEnumerable<TEntity> entities)
        {
            foreach (TEntity e in entities)
            {
                e.CreatedBy = _userContext.userClaims.fullName;
                e.CreatedById = _userContext.userClaims.userId;
                e.CreatedAt = DateTime.Now;
                e.UpdatedAt = DateTime.Now;
            }
            await _dbSet.AddRangeAsync(entities);
            // Bulk index vào Elasticsearch
            await _elasticClient.BulkAsync(b => b.Index(typeof(TEntity).Name.ToLower()).IndexMany(entities, (descriptor, e) => descriptor.Id(e.Id.ToString())));
        }
        public virtual async Task<TEntity?> FindAsync(TId id)
        {
            return await _dbSet.FindAsync(id);
        }
        public virtual void Update(TEntity e)
        {
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Update(e);
        }
        public virtual Task UpdateAsync(TEntity e)
        {
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Update(e);
            //_dbSet.Entry(e).State = EntityState.Modified;
            return Task.CompletedTask;
        }
        public virtual Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (TEntity e in entities)
            {
                e.UpdatedAt = DateTime.Now;
                e.UpdatedBy = _userContext.userClaims.fullName;
                e.UpdatedById = _userContext.userClaims.userId;
            }
            _dbSet.UpdateRange(entities);
            return Task.CompletedTask;
        }
        public virtual async Task UpdateWithElasticSearchAsync(TEntity e)
        {
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Update(e);
            //_dbSet.Entry(e).State = EntityState.Modified;
            await _elasticClient.IndexAsync(e, i => i.Index(typeof(TEntity).Name.ToLower()).Id(e.Id.ToString()));
        }
        public virtual async Task UpdateRangeWithElasticSearchAsync(IEnumerable<TEntity> entities)
        {
            foreach (TEntity e in entities)
            {
                e.UpdatedAt = DateTime.Now;
                e.UpdatedBy = _userContext.userClaims.fullName;
                e.UpdatedById = _userContext.userClaims.userId;
            }
            _dbSet.UpdateRange(entities);
            await _elasticClient.BulkAsync(b => b.Index(typeof(TEntity).Name.ToLower()).IndexMany(entities, (descriptor, e) => descriptor.Id(e.Id.ToString())));
        }
        public virtual void Remove(TEntity e)
        {
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Remove(e);
        }
        public virtual Task RemoveAsync(TEntity e)
        {
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Remove(e);
            return Task.CompletedTask;
        }
        public virtual void RemoveSoft(TEntity e)
        {
            e.Status = ConstantEnums.EntityStatus.DELETED;
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Update(e);
            //_dbSet.Entry(e).State = EntityState.Modified;
        }
        public virtual Task RemoveSoftAsync(TEntity e)
        {
            e.Status = ConstantEnums.EntityStatus.DELETED;
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Update(e);
            //_dbSet.Entry(e).State = EntityState.Modified;
            return Task.CompletedTask;
        }
        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (TEntity e in entities)
            {
                e.UpdatedAt = DateTime.Now;
                e.UpdatedBy = _userContext.userClaims.fullName;
                e.UpdatedById = _userContext.userClaims.userId;
            }
            _dbSet.RemoveRange(entities);
        }
        public virtual Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (TEntity e in entities)
            {
                e.UpdatedAt = DateTime.Now;
                e.UpdatedBy = _userContext.userClaims.fullName;
                e.UpdatedById = _userContext.userClaims.userId;
            }
            _dbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }
        public virtual void RemoveSoftRange(IEnumerable<TEntity> entities)
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
        public virtual Task RemoveSoftRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (TEntity e in entities)
            {
                e.Status = ConstantEnums.EntityStatus.DELETED;
                e.UpdatedAt = DateTime.Now;
                e.UpdatedBy = _userContext.userClaims.fullName;
                e.UpdatedById = _userContext.userClaims.userId;
            }
            _dbSet.UpdateRange(entities);
            return Task.CompletedTask;
        }
        public virtual async Task RemoveWithElasticSearchAsync(TEntity e)
        {
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Remove(e);
            await _elasticClient.DeleteAsync<TEntity>(e.Id.ToString(), d => d.Index(typeof(TEntity).Name.ToLower()));
        }
        public virtual async Task RemoveSoftWithElasticSearchAsync(TEntity e)
        {
            e.Status = ConstantEnums.EntityStatus.DELETED;
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Update(e);
            //_dbSet.Entry(e).State = EntityState.Modified;
            await _elasticClient.DeleteAsync<TEntity>(e.Id.ToString(), d => d.Index(typeof(TEntity).Name.ToLower()));
        }
        public virtual async Task RemoveRangeWithElasticSearchAsync(IEnumerable<TEntity> entities)
        {
            foreach (TEntity e in entities)
            {
                e.UpdatedAt = DateTime.Now;
                e.UpdatedBy = _userContext.userClaims.fullName;
                e.UpdatedById = _userContext.userClaims.userId;
            }
            _dbSet.RemoveRange(entities);
            await _elasticClient.BulkAsync(b => b.Index(typeof(TEntity).Name.ToLower()).DeleteMany(entities, (descriptor, e) => descriptor.Id(e.Id.ToString())));
        }
        public virtual async Task RemoveSoftRangeWithElasticSearchAsync(IEnumerable<TEntity> entities)
        {
            foreach (TEntity e in entities)
            {
                e.Status = ConstantEnums.EntityStatus.DELETED;
                e.UpdatedAt = DateTime.Now;
                e.UpdatedBy = _userContext.userClaims.fullName;
                e.UpdatedById = _userContext.userClaims.userId;
            }
            _dbSet.UpdateRange(entities);
            await _elasticClient.BulkAsync(b => b.Index(typeof(TEntity).Name.ToLower()).DeleteMany(entities, (descriptor, e) => descriptor.Id(e.Id.ToString())));
        }
        public virtual void LockEntity(TEntity e)
        {
            e.Status = ConstantEnums.EntityStatus.LOCK;
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Update(e);
            //_dbSet.Entry(e).State = EntityState.Modified;
        }
        public virtual void UnLockEntity(TEntity e)
        {
            e.Status = ConstantEnums.EntityStatus.NORMAL;
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Update(e);
            //_dbSet.Entry(e).State = EntityState.Modified;
        }
        public virtual async Task<TEntity> GetByKeyAsync(TId keyValue, bool skipDeleted = true)
        {
            if (skipDeleted)
                return await _dbSet.AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(keyValue));
            else
                return await _dbSet.AsNoTracking().IgnoreQueryFilters().SingleOrDefaultAsync(x => x.Id.Equals(keyValue));
        }
    }
}
