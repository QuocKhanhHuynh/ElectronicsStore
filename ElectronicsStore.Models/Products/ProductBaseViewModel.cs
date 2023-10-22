using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ElectronicsStore.Models.Products
{
    public class ProductBaseViewModel
    {
        [Display(Name = "Mã sản phẩm")]
        public int Id { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Giá bán")]
        public int SalePrice { get; set; }
        [Display(Name = "Giá giảm")]
        public int OfferPrice { get; set; }
        [Display(Name = "Lượng mua")]
        public int PurchaseCount { get; set; }
        [Display(Name = "Ảnh mặc định")]
        public string DefaultImage { get; set; }
    }
}
