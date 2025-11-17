using Application.EntityDtos.Roles;
using Application.IReponsitories.Abstractions;
using Domain.Commons;
using Infrastructure.Commons;
using Infrastructure.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Implementations
{
    public class MenuService : IMenuService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IFunctionRoleRepository _functionRoleRepository;
        private readonly IFunctionRepository _functionRepository;
        public MenuService(IUserRoleRepository userRoleRepository, IFunctionRoleRepository functionRoleRepository, IFunctionRepository functionRepository)
        {
            _userRoleRepository = userRoleRepository;
            _functionRoleRepository = functionRoleRepository;
            _functionRepository = functionRepository;
        }
        public async Task<List<Menu>> GetMenu(int userId)
        {
            return new List<Menu>();
        }
    }
}
