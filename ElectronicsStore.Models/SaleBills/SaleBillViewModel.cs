using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.SaleBills
{
    public class SaleBillViewModel
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string UserId { get; set; }
        public int TotalValue { get; set; }
    }
}
