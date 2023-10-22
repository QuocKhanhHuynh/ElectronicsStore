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
        [Display(Name = "Tên sản phẩm")]
        public string ProductName { get; set; }
        [Display(Name = "Ảnh")]
        public string Image { get; set; }
        [Display(Name = "Số lượng")]
        public int Quanlity { get; set; }
        [Display(Name = "Giá bán")]
        public int ImportPrice { get; set; }
    }
}
