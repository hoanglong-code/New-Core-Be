using Application.EntityDtos.Functions;
using Application.IReponsitories.Abstractions;
using Application.IReponsitories.Base;
using Domain.Commons;
using Domain.Constants;
using Domain.Entities.Extend;
using Domain.Exceptions.Extend;
using FluentValidation;
using Infrastructure.Commons;
using Infrastructure.Helpers;
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
    public class FunctionService : IFunctionService
    {
        private readonly IFunctionRepository _entityRepo;
        private readonly IValidator<Function> _validator;
        private static readonly ILog log = LogMaster.GetLogger("FunctionService", "FunctionService");
        public FunctionService(IFunctionRepository entityRepo, IValidator<Function> validator)
        {
            _entityRepo = entityRepo;
            _validator = validator;
        }
        public async Task<BaseSearchResponse<FunctionGridDto>> GetByPage(BaseCriteria request)
        {
            try
            {
                IQueryable<FunctionGridDto> query = _entityRepo.All().Select(FunctionGridDto.Expression);
                return await BaseSearchResponse<FunctionGridDto>.GetResponse(query, request);
            }
            catch (Exception ex)
            {
                log.Error("Error in GetByPage: ", ex);
                throw;
            }
        }
        public async Task<FunctionDetailDto> GetById(int id)
        {
            try
            {
                var dto = await _entityRepo.All()
                    .Where(x => x.Id == id)
                    .Select(FunctionDetailDto.Expression)
                    .FirstOrDefaultAsync();
                if (dto == null)
                {
                    throw new NotFoundException(MessageErrorConstant.NOT_FOUND);
                }
                return dto;
            }
            catch (Exception ex)
            {
                log.Error("Error in GetById: ", ex);
                throw;
            }
        }
        public async Task<Function> SaveData(Function entity)
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
                    return curEntity;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error in SaveData: ", ex);
                throw;
            }
        }
        public async Task<Function> DeleteData(int id)
        {
            try
            {
                Function curEntity = await _entityRepo.GetByKeyAsync(id);
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
        public async Task<List<Function>> DeleteMultipleData(string ids)
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

