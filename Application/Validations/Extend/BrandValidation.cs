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
    public class BrandValidation : BaseValidation<Brand, int>
    {
        public BrandValidation()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Continue)
                .NotNull()
                .WithMessage("Vui lòng nhập tên thương hiệu!")
                .NotEmpty()
                .WithMessage("Vui lòng nhập tên thương hiệu!")
                .MaximumLength(200)
                .WithMessage("Tên thương hiệu không được dài quá 200 ký tự");
            RuleFor(x => x.Code)
                .Cascade(CascadeMode.Continue)
                .NotNull()
                .WithMessage("Vui lòng nhập mã thương hiệu!")
                .NotEmpty()
                .WithMessage("Vui lòng nhập mã thương hiệu!")
                .MaximumLength(50)
                .WithMessage("Mã thương hiệu không được dài quá 50 ký tự");
            RuleFor(x => x.Note)
               .MaximumLength(2000)
               .WithMessage("Ghi chú không được dài quá 2000 ký tự");
        }
    }
}

