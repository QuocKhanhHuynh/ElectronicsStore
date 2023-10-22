using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.Products
{
    public class ProductImageUpdateRequestValidator : AbstractValidator<ProductImageUpdateRequest>
    {
        public ProductImageUpdateRequestValidator()
        {
            RuleFor(p => p.Image1).NotEmpty().WithMessage("Ảnh mặc định là bắt buộc");
        }
    }
}
