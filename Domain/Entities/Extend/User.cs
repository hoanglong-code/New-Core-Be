using Domain.Entities.Base;

namespace Domain.Entities.Extend
{
    public class User : BaseEntity<int>
    {
        public long? UserMapId { get; set; }
        public string? FullName { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Code { get; set; }
        public string? Avatar { get; set; }
        public int? Type { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? CardId { get; set; }
        public string? KeyLock { get; set; }
        public byte? TypeThird { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public string? RegEmail { get; set; }
        public int? RoleMax { get; set; }
        public byte? RoleLevel { get; set; }
        public bool? IsRoleGroup { get; set; }
        public bool? IsPhoneConfirm { get; set; }
        public bool? IsEmailConfirm { get; set; }
        public string? RegisterCode { get; set; }
        public int? CountLogin { get; set; }
        public int? LanguageId { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
