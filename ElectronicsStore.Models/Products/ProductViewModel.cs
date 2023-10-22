using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ElectronicsStore.Models.Products
{
    public class ProductViewModel
    {
        [Display(Name = "Mã sản phẩm")]
        public int Id { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Loại hàng")]
        public string CategoryName { get; set; }
        [Display(Name = "Nhãn hiệu")]
        public string BrandName { get; set; }
        [Display(Name = "Giá bán")]
        public int SalePrice { get; set; }
        [Display(Name = "Giá giảm")]
        public int OfferPrice { get; set; }
        [Display(Name = "Nguồn gốc")]
        public string Origin { get; set; }
        [Display(Name = "Giới thiệu")]
        public string Introduce { get; set; }
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [Display(Name = "Số lượng mua")]
        public int PurchaseCount { get; set; }
        [Display(Name = "Ảnh mặc định")]

        public List<string> Images { get; set; }
        public int CategoryId { get; set; }
        public int ImportBillId { get; set; }

    }
}
