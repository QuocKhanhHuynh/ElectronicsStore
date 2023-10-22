using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.ImportBills
{
    public class ImportBillCreateRequest
    {
        [Display(Name = "Nhà cung cấp")]
        public int? SupplierId { get; set; }
        public List<ImportDetailBillCreateRequest>? DetailBill { get; set; }
    }
}
