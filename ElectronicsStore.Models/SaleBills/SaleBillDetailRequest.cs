using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.SaleBills
{
    public class SaleBillDetailRequest
    {
        public int Quantity { get; set; }
        public int SalePrice { get; set; }
        public int ProductId { get; set; }
        public int ImportBillId { get; set; }
    }
}
