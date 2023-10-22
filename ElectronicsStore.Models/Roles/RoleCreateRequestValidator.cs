using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.Roles
{
    public class RoleCreateRequestValidator: AbstractValidator<RoleCreateRequest>
    {
        public RoleCreateRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Mã vai trò là bắt buộc");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên vai trò là bắt buộc");
        }
    }
}
