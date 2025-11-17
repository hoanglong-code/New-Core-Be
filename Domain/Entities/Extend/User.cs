using Domain.Entities.Base;
using static Domain.Enums.ConstantEnums;

namespace Domain.Entities.Extend
{
    public class User : BaseEntity<int>
    {
        public required string FullName { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public required string Phone { get; set; }
        public string? CardId { get; set; }
        public Gender Gender { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public int? CountLoginFail { get; set; }
        public virtual ICollection<UserRole>? UserRole { get; set; }
    }
}
