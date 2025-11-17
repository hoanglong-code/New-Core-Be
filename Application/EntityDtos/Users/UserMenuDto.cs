using Domain.Commons;
using Domain.Entities.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.ConstantEnums;

namespace Application.EntityDtos.Users
{
    public class UserMenuDto
    {
        public List<Menu> Menus { get; set; } = new List<Menu>();
        public static Expression<Func<User, UserMenuDto>> Expression => p => new UserMenuDto
        {
            Menus = p.UserRole!
                    .Where(ur => ur.Role != null && ur.Role.Status != EntityStatus.DELETED)
                    .SelectMany(ur => ur.Role!.FunctionRole!)
                    .Where(fr => fr.Function != null && fr.Function.Status != EntityStatus.DELETED)
                    .GroupBy(fr => new 
                    { 
                        fr.Function!.Id,
                        fr.Function!.Name,
                        fr.Function!.FunctionParentId,
                        fr.Function!.Url,
                        fr.Function!.Location,
                        fr.Function!.Icon,
                        fr.ActiveKey 
                    })
                    .Select(g => new Menu 
                    {
                        Id = g.Key.Id,
                        Name = g.Key.Name,
                        FunctionParentId = g.Key.FunctionParentId,
                        Url = g.Key.Url,
                        Location = g.Key.Location,
                        Icon = g.Key.Icon,
                        ActiveKey = g.Key.ActiveKey,
                    })
                    .ToList()
        };
    }
}
