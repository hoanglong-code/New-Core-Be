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
    public class UserDetailDto
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public required string Phone { get; set; }
        public string? CardId { get; set; }
        public Gender Gender { get; set; }
        public TypeRole TypeRole { get; set; }

        public static Expression<Func<User, UserDetailDto>> Expression => p => new UserDetailDto
        {
            Id = p.Id,
            UserName = p.UserName,
            FullName = p.FullName,
            Email = p.Email,
            Birthday = p.Birthday,
            Avatar = p.Avatar,
            Address = p.Address,
            Phone = p.Phone,
            CardId = p.CardId,
            Gender = p.Gender,
            TypeRole = p.TypeRole,
        };
    }
}
