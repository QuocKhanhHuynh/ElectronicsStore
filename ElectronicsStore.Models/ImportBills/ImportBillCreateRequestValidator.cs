using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.ImportBills
{
    public class ImportBillCreateRequestValidator : AbstractValidator<ImportBillCreateRequest>
    {
        public ImportBillCreateRequestValidator()
        {
            RuleFor(x => x.SupplierId).NotEmpty().WithMessage("Nhà cung cấp phải có");
        }
    }
}
