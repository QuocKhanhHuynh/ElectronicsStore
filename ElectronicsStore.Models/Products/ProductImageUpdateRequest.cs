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
    public class ProductImageUpdateRequest
    {
        public int ProductId { get; set; }
        [Display(Name = "Ảnh 1 (hiện mặc định)")]
        public IFormFile? Image1 { get; set; }
        [Display(Name = "Ảnh 2")]
        public IFormFile? Image2 { get; set; }
        [Display(Name = "Ảnh 3")]
        public IFormFile? Image3 { get; set; }
        [Display(Name = "Ảnh 4")]
        public IFormFile? Image4 { get; set; }
        [Display(Name = "Ảnh 5")]
        public IFormFile? Image5 { get; set; }
    }
}
