using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.SaleBills
{
    public class BillStatusUpdateRequestValidator : AbstractValidator<BillStatusUpdateRequest>
    {
        public BillStatusUpdateRequestValidator()
        {
            RuleFor(x => x.BillId).NotEmpty().WithMessage(" Mã hóa đơn là bắt buộc");
            RuleFor(x => x.StatusId).NotEmpty().WithMessage(" Mã trạng thái là bắt buộc");
        }
    }
}
