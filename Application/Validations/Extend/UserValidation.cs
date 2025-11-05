using Application.Validations.Base;
using Domain.Entities.Extend;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Validations.Extend
{
    public class UserValidation : BaseValidation<User, int>
    {
        public UserValidation()
        {
            RuleFor(x => x.FullName)
                .Cascade(CascadeMode.Continue)
                .NotNull()
                .WithMessage("Vui lòng nhập họ tên!")
                .NotEmpty()
                .WithMessage("Vui lòng nhập họ tên!")
                .MaximumLength(200)
                .WithMessage("Họ tên không được dài quá 200 ký tự");

            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.Continue)
                .NotNull()
                .WithMessage("Vui lòng nhập tên đăng nhập!")
                .NotEmpty()
                .WithMessage("Vui lòng nhập tên đăng nhập!")
                .MaximumLength(100)
                .WithMessage("Tên đăng nhập không được dài quá 100 ký tự")
                .MinimumLength(3)
                .WithMessage("Tên đăng nhập phải có ít nhất 3 ký tự");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Continue)
                .NotNull()
                .WithMessage("Vui lòng nhập mật khẩu!")
                .NotEmpty()
                .WithMessage("Vui lòng nhập mật khẩu!")
                .MinimumLength(6)
                .WithMessage("Mật khẩu phải có ít nhất 6 ký tự")
                .MaximumLength(100)
                .WithMessage("Mật khẩu không được dài quá 100 ký tự");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email không hợp lệ!")
                .When(x => !string.IsNullOrEmpty(x.Email))
                .MaximumLength(200)
                .WithMessage("Email không được dài quá 200 ký tự");

            RuleFor(x => x.Phone)
                .Matches(@"^[0-9]{10,11}$")
                .WithMessage("Số điện thoại không hợp lệ! Vui lòng nhập 10-11 chữ số.")
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.CardId)
                .MaximumLength(20)
                .WithMessage("Số CMND/CCCD không được dài quá 20 ký tự")
                .When(x => !string.IsNullOrEmpty(x.CardId));

            RuleFor(x => x.Address)
                .MaximumLength(500)
                .WithMessage("Địa chỉ không được dài quá 500 ký tự")
                .When(x => !string.IsNullOrEmpty(x.Address));

            RuleFor(x => x.Birthday)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Ngày sinh không được lớn hơn ngày hiện tại!")
                .When(x => x.Birthday.HasValue);

            RuleFor(x => x.Avatar)
                .MaximumLength(500)
                .WithMessage("Đường dẫn avatar không được dài quá 500 ký tự")
                .When(x => !string.IsNullOrEmpty(x.Avatar));
        }
    }
}

