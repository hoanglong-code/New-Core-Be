using Application.Data;
using Application.EntityDtos.Users;
using Domain.Constants;
using Domain.Entities.Extend;
using Domain.Exceptions.Extend;
using Elastic.Clients.Elasticsearch;
using Infrastructure.Commons;
using Infrastructure.Configurations;
using Infrastructure.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly AppDbContext _dbContext; // Inject AppDbContext
        private IConfiguration _configuration;
        private readonly string _secretKey;
        private readonly string _jwtKey;
        private readonly string _jwtExpireDays;
        private readonly string _jwtIssuer;

        public LoginService(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _secretKey = _configuration["Encryption:SecretKey"] ?? throw new ArgumentNullException("Encryption:SecretKey", "Missing secret key in configuration");
            _jwtKey = _configuration["AppSettings:JwtKey"] ?? throw new ArgumentNullException("AppSettings:JwtKey", "Missing jwt key in configuration");
            _jwtExpireDays = _configuration["AppSettings:JwtExpireDays"] ?? throw new ArgumentNullException("AppSettings:JwtExpireDays", "Missing jwt expire days in configuration");
            _jwtIssuer = _configuration["AppSettings:JwtIssuer"] ?? throw new ArgumentNullException("AppSettings:JwtIssuer", "Missing jwt issuer in configuration");
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
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var username = PasswordHasher.AesDecryption(request.UserName, _secretKey);
            var password = PasswordHasher.AesDecryption(request.Password, _secretKey);

            var user = await _dbContext.Users
                .Where(x => x.UserName == username && x.Status != Domain.Enums.ConstantEnums.EntityStatus.DELETED)
                .Select(UserLoginDto.Expression)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new NotFoundException(MessageErrorConstant.USER_NOT_FOUND);
            }

            if (!string.Equals(user.Password, password, StringComparison.Ordinal))
            {
                throw new NotFoundException(MessageErrorConstant.USER_NOT_FOUND);
            }

            if (user.Status == Domain.Enums.ConstantEnums.EntityStatus.LOCK)
            {
                throw new PermisionException(MessageErrorConstant.USER_LOCK);
            }

            if (user.Status == Domain.Enums.ConstantEnums.EntityStatus.TEMP)
            {
                throw new PermisionException(MessageErrorConstant.USER_TEMP);
            }

            return GenarateJwtToken(user);
        }
        private LoginResponse GenarateJwtToken(UserLoginDto user)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("FullName", user.FullName),
                new Claim("Phone", user.Phone),
                new Claim("Email", user.Email),
                new Claim("Gender", user.Gender),
                new Claim("Address", user.Address ?? ""),
                new Claim("Avatar", user.Avatar ?? ""),
                new Claim("Birthday", user.Birthday != null ? user.Birthday.Value.ToString("dd-MM-yyyy") : ""),
                new Claim("AccessKey", string.Join("", user.Functions ?? new List<string>()))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtExpireDays));

            var token = new JwtSecurityToken(
                _jwtIssuer,
                _jwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            var access_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new LoginResponse
            {
                UsertId = user.Id,
                AccessToken = access_token
            };
        }
    }
}

