using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.Statuses
{
    public class StatusUpdateRequestValidator : AbstractValidator<StatusUpdateRequest>
    {
        public StatusUpdateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên trạng thái là bắt buộc");
        }
    }
}
