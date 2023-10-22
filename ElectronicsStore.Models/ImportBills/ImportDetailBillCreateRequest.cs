using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.ImportBills
{
    public class ImportDetailBillCreateRequest
    {
        public int Quantity { get; set; }
        public int ImportPrice { get; set; }
        public int ProductId { get; set; }
    }
}
