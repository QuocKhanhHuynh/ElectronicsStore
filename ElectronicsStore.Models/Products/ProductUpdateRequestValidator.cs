using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.Products
{
    public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
    {
        public ProductUpdateRequestValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Tên sản phẩm là bắt buộc");
            RuleFor(p => p.SalePrice).NotEmpty().WithMessage("Giá bán là bắt buộc");
            RuleFor(p => p.Introduce).NotEmpty().WithMessage("Giới thiệu là bắt buộc");
            RuleFor(p => p.Origin).NotEmpty().WithMessage("Nguồn gốc là bắt buộc");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Mô tả là bắt buộc");
            RuleFor(p => p.OfferPrice).NotEmpty().WithMessage("Giá giảm là bắt buộc");
        }
    }
}
