using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.SaleBills
{
    public class SaleBillCreateRequestValidator : AbstractValidator<SaleBillCreateRequest>
    {
        public SaleBillCreateRequestValidator()
        {
            RuleFor(x => x.RecipientName).NotEmpty().WithMessage("Tên người nhận phải có");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Địa chỉ phải có");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Số điện thoại phải có");
        }
    }
}
