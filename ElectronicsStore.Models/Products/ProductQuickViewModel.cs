using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ElectronicsStore.Models.Products
{
    public class ProductQuickViewModel
    {
        [Display(Name = "Mã sản phẩm")]
        public int Id { get; set; }
        [Display(Name = "Mã hóa đơn nhập")]
        public int ImportBillId { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Giá gốc")]
        public int ImportPrice { get; set; }
        [Display(Name = "Giá bán")]
        public int SalePrice { get; set; }
        [Display(Name = "Giá giảm")]
        public int OfferPrice { get; set; }
        [Display(Name = "Số luợng")]
        public int Inventory { get; set; }
        [Display(Name = "Lượng mua")]
        public int PurchaseCount { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime DateCreate { get; set; }
        [Display(Name = "Ảnh mặc định")]
        public string DefaultImage { get; set; }
        public int CategoryId { get; set; }
    }
}
