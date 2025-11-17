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

        private void Commit()
        {
            _dbContext.SaveChanges();
        }

        private Task CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
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
            await CommitAsync();
        }
        public virtual void Add(TEntity e)
        {
            e.CreatedBy = _userContext.userClaims.fullName;
            e.CreatedById = _userContext.userClaims.userId;
            e.CreatedAt = DateTime.Now;
            e.UpdatedAt = DateTime.Now;
            _dbSet.Add(e);
            Commit();
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
            await CommitAsync();
        }
        public virtual async Task AddWithElasticSearchAsync(TEntity e)
        {
            e.CreatedBy = _userContext.userClaims.fullName;
            e.CreatedById = _userContext.userClaims.userId;
            e.CreatedAt = DateTime.Now;
            e.UpdatedAt = DateTime.Now;
            await _dbSet.AddAsync(e);
            await CommitAsync();
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
            await CommitAsync();
            // Bulk index vào Elasticsearch
            await _elasticClient.BulkAsync(b => b.Index(typeof(TEntity).Name.ToLower()).IndexMany(entities, (descriptor, e) => descriptor.Id(e.Id.ToString())));
        }
        public virtual async Task<TEntity?> FindAsync(TId id)
        {
            return await _dbSet.FindAsync(id);
        }
        public virtual void Update(TEntity e)
        {
            var entry = _dbContext.Entry(e);
            if (entry.State == EntityState.Detached)
            {
                var trackedEntity = _dbContext.ChangeTracker.Entries<TEntity>()
                    .FirstOrDefault(x => x.Entity.Id.Equals(e.Id) && x.State != EntityState.Detached)?.Entity;
                
                if (trackedEntity != null)
                {
                    _dbContext.Entry(trackedEntity).CurrentValues.SetValues(e);
                    trackedEntity.UpdatedAt = DateTime.Now;
                    trackedEntity.UpdatedBy = _userContext.userClaims.fullName;
                    trackedEntity.UpdatedById = _userContext.userClaims.userId;
                }
                else
                {
                    e.UpdatedAt = DateTime.Now;
                    e.UpdatedBy = _userContext.userClaims.fullName;
                    e.UpdatedById = _userContext.userClaims.userId;
                    _dbSet.Update(e);
                }
            }
            else
            {
                e.UpdatedAt = DateTime.Now;
                e.UpdatedBy = _userContext.userClaims.fullName;
                e.UpdatedById = _userContext.userClaims.userId;
            }
            Commit();
        }
        public virtual async Task UpdateAsync(TEntity e)
        {
            var entry = _dbContext.Entry(e);
            if (entry.State == EntityState.Detached)
            {
                var trackedEntity = _dbContext.ChangeTracker.Entries<TEntity>()
                    .FirstOrDefault(x => x.Entity.Id.Equals(e.Id) && x.State != EntityState.Detached)?.Entity;
                
                if (trackedEntity != null)
                {
                    _dbContext.Entry(trackedEntity).CurrentValues.SetValues(e);
                    trackedEntity.UpdatedAt = DateTime.Now;
                    trackedEntity.UpdatedBy = _userContext.userClaims.fullName;
                    trackedEntity.UpdatedById = _userContext.userClaims.userId;
                }
                else
                {
                    e.UpdatedAt = DateTime.Now;
                    e.UpdatedBy = _userContext.userClaims.fullName;
                    e.UpdatedById = _userContext.userClaims.userId;
                    _dbSet.Update(e);
                }
            }
            else
            {
                e.UpdatedAt = DateTime.Now;
                e.UpdatedBy = _userContext.userClaims.fullName;
                e.UpdatedById = _userContext.userClaims.userId;
            }
            await CommitAsync();
        }
        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            var entitiesToUpdate = new List<TEntity>();
            
            foreach (TEntity e in entities)
            {
                var entry = _dbContext.Entry(e);
                if (entry.State == EntityState.Detached)
                {
                    var trackedEntity = _dbContext.ChangeTracker.Entries<TEntity>()
                        .FirstOrDefault(x => x.Entity.Id.Equals(e.Id) && x.State != EntityState.Detached)?.Entity;
                    
                    if (trackedEntity != null)
                    {
                        _dbContext.Entry(trackedEntity).CurrentValues.SetValues(e);
                        trackedEntity.UpdatedAt = DateTime.Now;
                        trackedEntity.UpdatedBy = _userContext.userClaims.fullName;
                        trackedEntity.UpdatedById = _userContext.userClaims.userId;
                    }
                    else
                    {
                        e.UpdatedAt = DateTime.Now;
                        e.UpdatedBy = _userContext.userClaims.fullName;
                        e.UpdatedById = _userContext.userClaims.userId;
                        entitiesToUpdate.Add(e);
                    }
                }
                else
                {
                    e.UpdatedAt = DateTime.Now;
                    e.UpdatedBy = _userContext.userClaims.fullName;
                    e.UpdatedById = _userContext.userClaims.userId;
                }
            }
            
            if (entitiesToUpdate.Any())
            {
                _dbSet.UpdateRange(entitiesToUpdate);
            }
            
            await CommitAsync();
        }
        public virtual async Task UpdateWithElasticSearchAsync(TEntity e)
        {
            var entry = _dbContext.Entry(e);
            TEntity entityToIndex = e;
            
            if (entry.State == EntityState.Detached)
            {
                var trackedEntity = _dbContext.ChangeTracker.Entries<TEntity>()
                    .FirstOrDefault(x => x.Entity.Id.Equals(e.Id) && x.State != EntityState.Detached)?.Entity;
                
                if (trackedEntity != null)
                {
                    _dbContext.Entry(trackedEntity).CurrentValues.SetValues(e);
                    trackedEntity.UpdatedAt = DateTime.Now;
                    trackedEntity.UpdatedBy = _userContext.userClaims.fullName;
                    trackedEntity.UpdatedById = _userContext.userClaims.userId;
                    entityToIndex = trackedEntity;
                }
                else
                {
                    e.UpdatedAt = DateTime.Now;
                    e.UpdatedBy = _userContext.userClaims.fullName;
                    e.UpdatedById = _userContext.userClaims.userId;
                    _dbSet.Update(e);
                }
            }
            else
            {
                e.UpdatedAt = DateTime.Now;
                e.UpdatedBy = _userContext.userClaims.fullName;
                e.UpdatedById = _userContext.userClaims.userId;
            }
            await CommitAsync();
            await _elasticClient.IndexAsync(entityToIndex, i => i.Index(typeof(TEntity).Name.ToLower()).Id(entityToIndex.Id.ToString()));
        }
        public virtual async Task UpdateRangeWithElasticSearchAsync(IEnumerable<TEntity> entities)
        {
            var entitiesToUpdate = new List<TEntity>();
            var entitiesToIndex = new List<TEntity>();
            
            foreach (TEntity e in entities)
            {
                var entry = _dbContext.Entry(e);
                TEntity entityToIndex = e;
                
                if (entry.State == EntityState.Detached)
                {
                    var trackedEntity = _dbContext.ChangeTracker.Entries<TEntity>()
                        .FirstOrDefault(x => x.Entity.Id.Equals(e.Id) && x.State != EntityState.Detached)?.Entity;
                    
                    if (trackedEntity != null)
                    {
                        _dbContext.Entry(trackedEntity).CurrentValues.SetValues(e);
                        trackedEntity.UpdatedAt = DateTime.Now;
                        trackedEntity.UpdatedBy = _userContext.userClaims.fullName;
                        trackedEntity.UpdatedById = _userContext.userClaims.userId;
                        entityToIndex = trackedEntity;
                    }
                    else
                    {
                        e.UpdatedAt = DateTime.Now;
                        e.UpdatedBy = _userContext.userClaims.fullName;
                        e.UpdatedById = _userContext.userClaims.userId;
                        entitiesToUpdate.Add(e);
                    }
                }
                else
                {
                    e.UpdatedAt = DateTime.Now;
                    e.UpdatedBy = _userContext.userClaims.fullName;
                    e.UpdatedById = _userContext.userClaims.userId;
                }
                
                entitiesToIndex.Add(entityToIndex);
            }
            
            if (entitiesToUpdate.Any())
            {
                _dbSet.UpdateRange(entitiesToUpdate);
            }
            
            await CommitAsync();
            await _elasticClient.BulkAsync(b => b.Index(typeof(TEntity).Name.ToLower()).IndexMany(entitiesToIndex, (descriptor, e) => descriptor.Id(e.Id.ToString())));
        }
        public virtual void Remove(TEntity e)
        {
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Remove(e);
            Commit();
        }
        public virtual async Task RemoveAsync(TEntity e)
        {
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Remove(e);
            await CommitAsync();
        }
        public virtual void RemoveSoft(TEntity e)
        {
            e.Status = ConstantEnums.EntityStatus.DELETED;
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Update(e);
            //_dbSet.Entry(e).State = EntityState.Modified;
            Commit();
        }
        public virtual async Task RemoveSoftAsync(TEntity e)
        {
            e.Status = ConstantEnums.EntityStatus.DELETED;
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Update(e);
            //_dbSet.Entry(e).State = EntityState.Modified;
            await CommitAsync();
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
            Commit();
        }
        public virtual async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (TEntity e in entities)
            {
                e.UpdatedAt = DateTime.Now;
                e.UpdatedBy = _userContext.userClaims.fullName;
                e.UpdatedById = _userContext.userClaims.userId;
            }
            _dbSet.RemoveRange(entities);
            await CommitAsync();
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
            Commit();
        }
        public virtual async Task RemoveSoftRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (TEntity e in entities)
            {
                e.Status = ConstantEnums.EntityStatus.DELETED;
                e.UpdatedAt = DateTime.Now;
                e.UpdatedBy = _userContext.userClaims.fullName;
                e.UpdatedById = _userContext.userClaims.userId;
            }
            _dbSet.UpdateRange(entities);
            await CommitAsync();
        }
        public virtual async Task RemoveWithElasticSearchAsync(TEntity e)
        {
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Remove(e);
            await CommitAsync();
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
            await CommitAsync();
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
            await CommitAsync();
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
            await CommitAsync();
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
            Commit();
        }
        public virtual void UnLockEntity(TEntity e)
        {
            e.Status = ConstantEnums.EntityStatus.NORMAL;
            e.UpdatedAt = DateTime.Now;
            e.UpdatedBy = _userContext.userClaims.fullName;
            e.UpdatedById = _userContext.userClaims.userId;
            _dbSet.Update(e);
            //_dbSet.Entry(e).State = EntityState.Modified;
            Commit();
        }
        public virtual async Task<TEntity> GetByKeyAsync(TId keyValue, bool skipDeleted = true)
        {
            if (skipDeleted)
                return await _dbSet.SingleOrDefaultAsync(x => x.Id.Equals(keyValue));
            else
                return await _dbSet.IgnoreQueryFilters().SingleOrDefaultAsync(x => x.Id.Equals(keyValue));
        }
    }
}
