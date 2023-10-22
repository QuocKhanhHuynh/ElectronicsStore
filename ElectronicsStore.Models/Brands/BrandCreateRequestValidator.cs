using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.Brands
{
    public class BrandCreateRequestValidator : AbstractValidator<BrandCreateRequest>
    {
        public BrandCreateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên nhãn hiệu là bắt buộc");
            RuleFor(x => x.Logo).NotEmpty().WithMessage("Logo nhãn hiệu là bắt buộc");
        }
    }
}