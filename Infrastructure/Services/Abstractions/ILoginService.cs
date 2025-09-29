using Domain.Entities.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Abstractions
{
    public interface ILoginService
    {
        public Task<User> GetUserByUserName(string userName);
    }
}
