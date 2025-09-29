using Domain.Entities.Extend;
using Infrastructure.Configurations;
using Infrastructure.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly AppDbContext _dbContext; // Inject AppDbContext
        public LoginService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> GetUserByUserName(string userName)
        {
            try
            {
                // Sử dụng DbContext để truy vấn dữ liệu
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có lỗi xảy ra : {ex.Message}");
                return null;
            }
        }
        public async Task Login()
        {

        }
    }
}
