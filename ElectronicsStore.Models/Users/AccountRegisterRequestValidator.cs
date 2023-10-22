using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.Users
{
    public class AccountRegisterRequestValidator : AbstractValidator<AccountRegisterRequest>
    {
        public AccountRegisterRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName là bắt buộc");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu là bắt buộc").Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").WithMessage("Mật khẩu phải dài ít nhất 8 ký tự bao gồm ít nhất 1 số, 1 chữ cái viết hoa, 1 chữ cái thường");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email là bắt buộc").Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Không đúng định dạng email");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Tên đầy đủ là bắt buộc");
            RuleFor(x => x.Address).NotEmpty().WithMessage("địa chỉ là bắt buộc");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Số điện thoại là bắt buộc");
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Vai trò là bắt buộc");
            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.ConfirmPassword != request.Password)
                    context.AddFailure("Mật khẩu không khớp");
            });
        }
    }
}
