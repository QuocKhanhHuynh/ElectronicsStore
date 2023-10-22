using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.Brands
{
    public class BrandUpdateRequestvalidator : AbstractValidator<BrandUpdateRequest>
    {
        public BrandUpdateRequestvalidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên nhãn hiệu là bắt buộc");
        }
    }
}
