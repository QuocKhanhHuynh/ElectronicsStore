using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ElectronicsStore.Models.Products
{
    public class ProductUpdateRequest
    {
        public int Id { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string? Name { get; set; }
        [Display(Name = "Giá bán")]
        public int? SalePrice { get; set; }
        [Display(Name = "Giá giảm")]
        public int? OfferPrice { get; set; }
        [Display(Name = "Nguồn gốc")]
        public string? Origin { get; set; }
        [Display(Name = "Giới thiệu")]
        public string? Introduce { get; set; }
        [Display(Name = "Mô tả")]
        public string? Description { get; set; }
        [Display(Name = "Ảnh")]
        public List<IFormFile>? Image { get; set; }
    }
}
