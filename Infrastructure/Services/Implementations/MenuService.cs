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
            return CreateMenu(menus.Menus, 0);
        }
        public static List<Menu> CreateMenu(List<Menu> list, int k)
        {
            var listMenu = list.Where(e => e.FunctionParentId == k).ToList();
            if (listMenu.Count > 0)
            {
                List<Menu> menus = new List<Menu>();
                foreach (var item in listMenu)
                {
                    char[] str = item.ActiveKey.ToCharArray();
                    if (int.Parse(str[0].ToString()) == 1)
                    {
                        Menu menu = new Menu()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            FunctionParentId = item.FunctionParentId,
                            Url = item.Url,
                            Location = item.Location,
                            Icon = item.Icon,
                            ActiveKey = item.ActiveKey,
                            Childrens = CreateMenu(list, item.Id),
                        };
                        menus.Add(menu);
                    }
                }
                return menus;
            }
            return new List<Menu>();
        }
    }
}
