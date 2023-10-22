using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.Users
{
    public class PasswordForgetRequestValidator : AbstractValidator<PasswordForgetRequest>
    {
        public PasswordForgetRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName là bắt buộc");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Mật khẩu là bắt buộc").Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").WithMessage("Mật khẩu phải dài ít nhất 8 ký tự bao gồm ít nhất 1 số, 1 chữ cái viết hoa, 1 chữ cái thường");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email là bắt buộc");
            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.ConfirmNewPassword != request.NewPassword)
                    context.AddFailure("Mật khẩu không khớp");
            });
        }
    }
}
