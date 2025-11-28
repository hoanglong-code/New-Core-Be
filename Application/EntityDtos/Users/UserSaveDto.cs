using Domain.Entities.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.ConstantEnums;

namespace Application.EntityDtos.Users
{
    public class UserSaveDto
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string UserName { get; set; }
        public string? Password { get; set; }
        public required string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public required string Phone { get; set; }
        public string? CardId { get; set; }
        public Gender Gender { get; set; }
        public int? CountLoginFail { get; set; }
        public ICollection<UserRole>? UserRole { get; set; }
    }
}

