using Application.EntityDtos.Users;
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
    public interface IUserService
    {
        public Task<BaseSearchResponse<UserGridDto>> GetByPage(BaseCriteria request);
        public Task<UserDetailDto> GetById(int id);
        public Task<User> SaveData(User entity);
        public Task<User> DeleteData(int id);
        public Task<List<User>> DeleteMultipleData(string ids);
    }
}

