using Application.EntityDtos.Functions;
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
    public class UserLoginDto
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string FullName { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required string Gender { get; set; }
        public string? Address { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Avatar { get; set; }
        public EntityStatus Status { get; set; }
        public List<string>? Functions { get; set; }

        public static Expression<Func<User, UserLoginDto>> Expression => p => new UserLoginDto
        {
            Id = p.Id,
            UserName = p.UserName,
            Password = p.Password,
            FullName = p.FullName,
            Phone = p.Phone,
            Email = p.Email,
            Gender = p.Gender == Domain.Enums.ConstantEnums.Gender.MALE ? "Nam" : "Nữ",
            Address = p.Address,
            Birthday = p.Birthday,
            Avatar = p.Avatar,
            Status = p.Status,
            Functions = p.UserRole!
                    .Where(ur => ur.Role != null && ur.Role.Status != EntityStatus.DELETED)
                    .SelectMany(ur => ur.Role!.FunctionRole!)
                    .Where(fr => fr.Function != null && fr.Function.Status != EntityStatus.DELETED)
                    .GroupBy(fr => new { fr.Function!.Code, fr.ActiveKey })
                    .Select(g => $"{g.Key.Code}:{g.Key.ActiveKey}")
                    .ToList()
        };
    }
}
