using Application.EntityDtos.Users;
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
    public class UserService : IUserService
    {
        private readonly IUserRepository _entityRepo;
        private readonly IUserRoleRepository _userRoleRepo;
        private readonly IValidator<User> _validator;
        private static readonly ILog log = LogMaster.GetLogger("UserService", "UserService");
        public UserService(IUserRepository entityRepo, IUserRoleRepository userRoleRepo, IValidator<User> validator)
        {
            _entityRepo = entityRepo;
            _userRoleRepo = userRoleRepo;
            _validator = validator;
        }
        public async Task<BaseSearchResponse<UserDto>> GetByPage(BaseCriteria request)
        {
            try
            {
                IQueryable<UserDto> query = _entityRepo.All().Select(UserDto.Expression);
                return await BaseSearchResponse<UserDto>.GetResponse(query, request);
            }
            catch (Exception ex)
            {
                log.Error("Error in GetByPage: ", ex);
                throw;
            }
        }
        public async Task<User> GetById(int id)
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
        public async Task<User> SaveData(User entity)
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
                    if (entity.UserRole != null && entity.UserRole.Any())
                    {
                        entity.UserRole.ToList().ForEach(x => x.UserId = entity.Id);
                        await _userRoleRepo.AddRangeAsync(entity.UserRole);
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
                    if (entity.UserRole != null && entity.UserRole.Any())
                    {
                        entity.UserRole.ToList().ForEach(x => x.UserId = entity.Id);
                        await _userRoleRepo.UpdateRangeAsync(entity.UserRole);
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
        public async Task<User> DeleteData(int id)
        {
            try
            {
                User curEntity = await _entityRepo.GetByKeyAsync(id);
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
        public async Task<List<User>> DeleteMultipleData(string ids)
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

