using Domain.Entities.Base;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations.Base
{
    public class BaseValidation<T, TId> : AbstractValidator<T> where T : BaseEntity<TId>, IEntity<TId>
    {
        public BaseValidation()
        {
            RuleFor(x => x.CreatedAt)
                .Cascade(CascadeMode.Continue)
                .NotNull()
                .WithMessage("Vui lòng nhập ngày tạo!")
                .NotEmpty()
                .WithMessage("Vui lòng nhập ngày tạo!");
            //.LessThanOrEqualTo(DateTime.Now)
            //.WithMessage("Ngày tạo không được nhỏ hơn hiện tại.");
            RuleFor(x => x.UpdatedAt)
               .Cascade(CascadeMode.Continue)
               .NotNull()
               .WithMessage("Vui lòng nhập ngày cập nhật!")
               .NotEmpty()
               .WithMessage("Vui lòng nhập ngày cập nhật!");
            // Trường hợp 1 nhiều
            //When(x => x.MaintainceProductItems.Any(), () =>
            //{
            //    RuleForEach(x => x.MaintainceProductItems).SetValidator(new CreateMaintainceProductItemValidation());
            //});
        }
    }
}
