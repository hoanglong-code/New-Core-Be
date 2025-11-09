using Application.EntityDtos;
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
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _entityRepo;
        private readonly IValidator<Brand> _validator;
        public readonly IUnitOfWork _unitOfWork;
        private static readonly ILog log = LogMaster.GetLogger("BrandService", "BrandService");
        public BrandService(IBrandRepository entityRepo, IValidator<Brand> validator, IUnitOfWork unitOfWork)
        {
            _entityRepo = entityRepo;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseSearchResponse<BrandDto>> GetByPage(BaseCriteria request)
        {
            try
            {
                IQueryable<BrandDto> query = _entityRepo.All().Select(BrandDto.Expression);
                return await BaseSearchResponse<BrandDto>.GetResponse(query, request);
            }
            catch (Exception ex)
            {
                log.Error("Error in GetByPage: ", ex);
                throw;
            }
        }
        public async Task<Brand> GetById(int id)
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
        public async Task<Brand> SaveData(Brand entity)
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
                }
                else
                {
                    var curEntity = await _entityRepo.GetByKeyAsync(entity.Id);
                    if (curEntity == null)
                    {
                        throw new NotFoundException(MessageErrorConstant.NOT_FOUND);
                    }
                    await _entityRepo.UpdateAsync(entity);
                }
                await _unitOfWork.CommitChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                log.Error("Error in SaveData: ", ex);
                throw;
            }
        }
        public async Task<Brand> DeleteData(int id)
        {
            try
            {
                Brand curEntity = await _entityRepo.GetByKeyAsync(id);
                if (curEntity == null)
                {
                    throw new NotFoundException(MessageErrorConstant.NOT_FOUND);
                }
                await _entityRepo.RemoveSoftAsync(curEntity);
                await _unitOfWork.CommitChangesAsync();
                return curEntity;
            }
            catch (Exception ex)
            {
                log.Error("Error in DeleteData: ", ex);
                throw;
            }
        }
        public async Task<List<Brand>> DeleteMultipleData(string ids)
        {
            try
            {
                var idList = ids.Split(",").Select(int.Parse).ToList();
                var entities = await _entityRepo.All().Where(s => idList.Contains(s.Id)).ToListAsync();
                await _entityRepo.RemoveSoftRangeAsync(entities);
                await _unitOfWork.CommitChangesAsync();
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
