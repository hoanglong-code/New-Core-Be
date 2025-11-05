using Application.Validations.Base;
using Domain.Entities.Extend;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations.Extend
{
    public class RoleValidation : BaseValidation<Role, int>
    {
        public RoleValidation()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Continue)
                .NotNull()
                .WithMessage("Vui lòng nhập tên vai trò!")
                .NotEmpty()
                .WithMessage("Vui lòng nhập tên vai trò!")
                .MaximumLength(200)
                .WithMessage("Tên vai trò không được dài quá 200 ký tự");

            RuleFor(x => x.Code)
                .Cascade(CascadeMode.Continue)
                .NotNull()
                .WithMessage("Vui lòng nhập mã vai trò!")
                .NotEmpty()
                .WithMessage("Vui lòng nhập mã vai trò!")
                .MaximumLength(50)
                .WithMessage("Mã vai trò không được dài quá 50 ký tự");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("Loại vai trò không hợp lệ!");

            RuleFor(x => x.Note)
               .MaximumLength(2000)
               .WithMessage("Ghi chú không được dài quá 2000 ký tự");
        }
    }
}

