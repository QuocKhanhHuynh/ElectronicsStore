using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.Suppliers
{
    public class SupplierCreateRequestValidator : AbstractValidator<SupplierCreateRequest>
    {
        public SupplierCreateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên phải có");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Địa chỉ phải có");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Số điện thoại phải có");
            RuleFor(x => x.BankName).NotEmpty().WithMessage("Tên ngân hàng phải có");
            RuleFor(x => x.BankNumber).NotEmpty().WithMessage("Số tài khoản ngân hàng phải có");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email phải có").Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Không đúng định dạng email");
        }
    }
}
