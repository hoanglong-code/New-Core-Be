using Application.EntityDtos.Roles;
using Application.IReponsitories.Abstractions;
using Application.IReponsitories.Base;
using Domain.Commons;
using Domain.Constants;
using Domain.Entities.Extend;
using Domain.Exceptions.Extend;
using FluentValidation;
using Infrastructure.Commons;
using Infrastructure.Helpers;
using Infrastructure.Reponsitories.Implementations;
using Infrastructure.Services.Abstractions;
using log4net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _entityRepo;
        private readonly IFunctionRoleRepository _functionRoleRepo;
        private readonly IValidator<Role> _validator;
        private static readonly ILog log = LogMaster.GetLogger("RoleService", "RoleService");
        public RoleService(IRoleRepository entityRepo, IFunctionRoleRepository functionRoleRepo, IValidator<Role> validator)
        {
            _entityRepo = entityRepo;
            _functionRoleRepo = functionRoleRepo;
            _validator = validator;
        }
        public async Task<BaseSearchResponse<RoleDto>> GetByPage(BaseCriteria request)
        {
            try
            {
                IQueryable<RoleDto> query = _entityRepo.All().Select(RoleDto.Expression);
                return await BaseSearchResponse<RoleDto>.GetResponse(query, request);
            }
            catch (Exception ex)
            {
                log.Error("Error in GetByPage: ", ex);
                throw;
            }
        }
        public async Task<Role> GetById(int id)
        {
            try
            {
                var entity = await _entityRepo.GetByKeyAsync(id);
                if (entity == null)
                {
                    throw new NotFoundException(MessageErrorConstant.NOT_FOUND);
                }
                return entity;
            }
            catch (Exception ex)
            {
                log.Error("Error in GetById: ", ex);
                throw;
            }
        }
        public async Task<Role> SaveData(Role entity)
        {
            try
            {
                var validationResult = _validator.Validate(entity);
                if (!validationResult.IsValid)
                {
                    var errorMessages = string.Join(" , ", validationResult.Errors.Select(e => e.ErrorMessage));
                    throw new ValidateException(errorMessages);
                }
                if (entity.Id <= 0)
                {
                    await _entityRepo.AddAsync(entity);
                    if (entity.FunctionRole != null && entity.FunctionRole.Any())
                    {
                        entity.FunctionRole.ToList().ForEach(x => x.RoleId = entity.Id);
                        await _functionRoleRepo.AddRangeAsync(entity.FunctionRole);
                    }
                    return entity;
                }
                else
                {
                    var curEntity = await _entityRepo.GetByKeyAsync(entity.Id);
                    if (curEntity == null)
                    {
                        throw new NotFoundException(MessageErrorConstant.NOT_FOUND);
                    }
                    await _entityRepo.UpdateAsync(entity);
                    if (entity.FunctionRole != null && entity.FunctionRole.Any())
                    {
                        entity.FunctionRole.ToList().ForEach(x => x.RoleId = entity.Id);
                        await _functionRoleRepo.UpdateRangeAsync(entity.FunctionRole);
                    }
                    return curEntity;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error in SaveData: ", ex);
                throw;
            }
        }
        public async Task<Role> DeleteData(int id)
        {
            try
            {
                Role curEntity = await _entityRepo.GetByKeyAsync(id);
                if (curEntity == null)
                {
                    throw new NotFoundException(MessageErrorConstant.NOT_FOUND);
                }
                await _entityRepo.RemoveSoftAsync(curEntity);
                return curEntity;
            }
            catch (Exception ex)
            {
                log.Error("Error in DeleteData: ", ex);
                throw;
            }
        }
        public async Task<List<Role>> DeleteMultipleData(string ids)
        {
            try
            {
                var idList = ids.Split(",").Select(int.Parse).ToList();
                var entities = await _entityRepo.All().Where(s => idList.Contains(s.Id)).ToListAsync();
                await _entityRepo.RemoveSoftRangeAsync(entities);
                return entities;
            }
            catch (Exception ex)
            {
                log.Error("Error in DeleteMultipleData: ", ex);
                throw;
            }
        }
    }
}

