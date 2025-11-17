using Application.EntityDtos.Users;
using Application.IReponsitories.Abstractions;
using Domain.Commons;
using Infrastructure.Extensions;
using Infrastructure.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Implementations
{
    public class MenuService : IMenuService
    {
        private readonly IUserRepository _entityRepo;

        public MenuService(IUserRepository entityRepo)
        {
            _entityRepo = entityRepo;
        }

        public async Task<List<Menu>> GetMenu(int userId)
        {
            var menus = await _entityRepo.All().Where(x => x.Id == userId).Select(UserMenuDto.Expression).FirstOrDefaultAsync();
            if(menus == null)
            {
                return new List<Menu>();
            }
            return CheckRoleExtension.CreateMenu(menus.Menus, 0);
        }
    }
}
