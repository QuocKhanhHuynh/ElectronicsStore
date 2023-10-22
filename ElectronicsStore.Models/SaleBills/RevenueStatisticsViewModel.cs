using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.SaleBills
{
    public class RevenueStatisticsViewModel
    {
        public string Image { get; set; }
        public string ProductName { get; set; }
        public int ImporPrice { get; set; }
        public int SalePrice { get; set; }
        public DateTime DateSale { get; set; }
    }
}
