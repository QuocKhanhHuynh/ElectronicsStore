using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.Users
{
    public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
    {
        public UserUpdateRequestValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Tên đầy đủ là bắt buộc");
            RuleFor(x => x.Address).NotEmpty().WithMessage("địa chỉ là bắt buộc");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Số điện thoại là bắt buộc");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email là bắt buộc").Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Không đúng định dạng email");
        }
    }
}
