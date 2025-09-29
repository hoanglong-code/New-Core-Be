using Application.Contexts.Abstractions;
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
using Infrastructure.Reponsitories.Base;
using Infrastructure.Services.Abstractions;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _entityRepo;
        private readonly IValidator<Product> _validator;
        public readonly IUnitOfWork _unitOfWork;
        private static readonly ILog log = LogMaster.GetLogger("ProductService", "ProductService");
        public ProductService(IProductRepository entityRepo, IValidator<Product> validator, IUnitOfWork unitOfWork)
        {
            _entityRepo = entityRepo;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseSearchResponse<ProductDto>> GetByPage(BaseCriteria request)
        {
            try
            {
                IQueryable<ProductDto> query = _entityRepo.All().Select(ProductDto.Expression);
                return await BaseSearchResponse<ProductDto>.GetResponse(query, request);
            }
            catch (Exception ex)
            {
                log.Error("Error in GetByPage: ", ex);
                throw;
            }
        }
        public async Task<Product> GetById(int id)
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
        public async Task<Product> SaveData(Product entity)
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
        public async Task<Product> DeleteData(int id)
        {
            try
            {
                Product curEntity = await _entityRepo.GetByKeyAsync(id);
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
        public async Task<List<Product>> DeleteMultipleData(string ids)
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
