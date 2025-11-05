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
    public class FunctionValidation : BaseValidation<Function, int>
    {
        public FunctionValidation()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Continue)
                .NotNull()
                .WithMessage("Vui lòng nhập tên chức năng!")
                .NotEmpty()
                .WithMessage("Vui lòng nhập tên chức năng!")
                .MaximumLength(200)
                .WithMessage("Tên chức năng không được dài quá 200 ký tự");

            RuleFor(x => x.Code)
                .Cascade(CascadeMode.Continue)
                .NotNull()
                .WithMessage("Vui lòng nhập mã chức năng!")
                .NotEmpty()
                .WithMessage("Vui lòng nhập mã chức năng!")
                .MaximumLength(50)
                .WithMessage("Mã chức năng không được dài quá 50 ký tự");

            RuleFor(x => x.FunctionParentId)
                .GreaterThan(0)
                .WithMessage("Chức năng cha không hợp lệ!")
                .When(x => x.FunctionParentId.HasValue);

            RuleFor(x => x.Url)
                .MaximumLength(500)
                .WithMessage("URL không được dài quá 500 ký tự")
                .When(x => !string.IsNullOrEmpty(x.Url));

            RuleFor(x => x.Note)
               .MaximumLength(2000)
               .WithMessage("Ghi chú không được dài quá 2000 ký tự");

            RuleFor(x => x.Location)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Vị trí phải lớn hơn hoặc bằng 0!")
                .When(x => x.Location.HasValue);

            RuleFor(x => x.Icon)
                .MaximumLength(100)
                .WithMessage("Icon không được dài quá 100 ký tự")
                .When(x => !string.IsNullOrEmpty(x.Icon));
        }
    }
}

