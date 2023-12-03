using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ElectronicsStore.Models.ImportBills
{
    public class ImportBillDetailViewModel
    {
        [Display(Name = "Mã hóa đơn")]
        public int Id { get; set; }
        [Display(Name = "Ngày lập")]
        public DateTime Datecreate { get; set; }
        [Display(Name = "Nhà cung cấp")]
        public string SupplierName { get; set; }
        [Display(Name = "Người lập")]
        public string UserName { get; set; }
        [Display(Name = "Tổng giá trị")]
        public int TotalValue { get; set; }
        public List<ImportBillDetailModel> Details { get; set; }
    }
}
