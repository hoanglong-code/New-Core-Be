using Application.EntityDtos.Roles;
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
    public interface IRoleService
    {
        public Task<BaseSearchResponse<RoleGridDto>> GetByPage(BaseCriteria request);
        public Task<RoleDetailDto> GetById(int id);
        public Task<Role> SaveData(Role entity);
        public Task<Role> DeleteData(int id);
        public Task<List<Role>> DeleteMultipleData(string ids);
    }
}

