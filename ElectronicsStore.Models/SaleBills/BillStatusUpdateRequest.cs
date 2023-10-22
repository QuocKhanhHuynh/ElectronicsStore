using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.SaleBills
{
    public class BillStatusUpdateRequest
    {
        public int BillId { get; set; }
        public int StatusId { get; set; }
    }
}
