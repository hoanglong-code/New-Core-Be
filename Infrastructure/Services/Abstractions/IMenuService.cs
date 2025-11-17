using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Abstractions
{
    public interface IMenuService
    {
        public Task<List<Menu>> GetMenu(int userId);
    }
}
