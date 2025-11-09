using Domain.Entities.Extend;
using System;
using System.Linq.Expressions;
using static Domain.Enums.ConstantEnums;

namespace Application.EntityDtos
{
	public class UserDto
	{
		public int Id { get; set; }
		public required string FullName { get; set; }
		public required string Phone { get; set; }
		public required string Email { get; set; }
		public required string Gender { get; set; }
		public string? Address { get; set; }
		public DateTime? Birthday { get; set; }

		public static Expression<Func<User, UserDto>> Expression => p => new UserDto
		{
			Id = p.Id,
			FullName = p.FullName,
			Phone = p.Phone,
			Email = p.Email,
			Gender = p.Gender == Domain.Enums.ConstantEnums.Gender.MALE ? "Nam" : "Nữ",
			Address = p.Address,
			Birthday = p.Birthday,
		};
	}
}


