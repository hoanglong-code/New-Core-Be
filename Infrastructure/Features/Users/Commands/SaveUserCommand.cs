using Domain.Entities.Extend;
using MediatR;
using static Domain.Enums.ConstantEnums;

namespace Infrastructure.Features.Users.Commands
{
    public class SaveUserCommand : IRequest<User>
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime? Birthday { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string? CardId { get; set; }
        public Gender Gender { get; set; }
        public int? CountLoginFail { get; set; }
        public ICollection<UserRole>? UserRole { get; set; }
    }
}

