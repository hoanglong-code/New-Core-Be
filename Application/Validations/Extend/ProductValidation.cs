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
    public class ProductValidation : BaseValidation<Product, int>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Continue)
                .NotNull()
                .WithMessage("Vui lòng nhập tên sản phẩm!")
                .NotEmpty()
                .WithMessage("Vui lòng nhập tên sản phẩm!")
                .MaximumLength(200)
                .WithMessage("Tên sản phẩm không được dài quá 200 ký tự");
            RuleFor(x => x.Code)
                .Cascade(CascadeMode.Continue)
                .NotNull()
                .WithMessage("Vui lòng nhập mã sản phẩm!")
                .NotEmpty()
                .WithMessage("Vui lòng nhập mã sản phẩm!")
                .MaximumLength(50)
                .WithMessage("Tên sản phẩm không được dài quá 50 ký tự");
            RuleFor(x => x.Note)
               .MaximumLength(50)
               .WithMessage("Mô tả sản phẩm không được dài quá 2000 ký tự");
            RuleFor(x => x.BrandId)
                .Cascade(CascadeMode.Continue)
                .NotNull()
                .WithMessage("Vui lòng chọn thương hiệu!")
                .NotEmpty()
                .WithMessage("Vui lòng chọn thương hiệu!")
                .Must(brandId => brandId > 0)
                .WithMessage("Thương hiệu không hợp lệ.");
            RuleFor(x => x.Price)
                .Cascade(CascadeMode.Continue)
                .NotNull()
                .WithMessage("Vui lòng nhập giá sản phẩm!")
                .NotEmpty()
                .WithMessage("Vui lòng nhập giá sản phẩm!")
                .GreaterThan(0)
                .WithMessage("Giá sản phẩm phải lớn hơn 0."); ;
        }
    }
}
