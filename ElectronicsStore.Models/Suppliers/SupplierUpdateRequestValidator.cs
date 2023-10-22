using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.Suppliers
{
    public class SupplierUpdateRequestValidator : AbstractValidator<SupplierUpdateRequest>
    {
        public SupplierUpdateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên phải có");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Địa chỉ phải có");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Số điện thoại phải có");
            RuleFor(x => x.BankName).NotEmpty().WithMessage("Tên ngân hàng phải có");
            RuleFor(x => x.BankNumber).NotEmpty().WithMessage("Số tài khoản ngân hàng phải có");
        }
    }
}
