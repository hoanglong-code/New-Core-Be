using Application.EntityDtos.Functions;
using Domain.Commons;
using Domain.Entities.Extend;
using Infrastructure.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Abstractions
{
    public interface IFunctionService
    {
        public Task<BaseSearchResponse<FunctionGridDto>> GetByPage(BaseCriteria request);
        public Task<FunctionDetailDto> GetById(int id);
        public Task<Function> SaveData(Function entity);
        public Task<Function> DeleteData(int id);
        public Task<List<Function>> DeleteMultipleData(string ids);
    }
}

