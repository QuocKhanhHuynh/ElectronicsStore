using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ElectronicsStore.Models.Categories
{
    public class CategoryViewModel
    {
        [Display(Name = "Mã loại hàng")]
        public int Id { get; set; }
        [Display(Name = "Tên loại hàng")]
        public string Name { get; set; }
    }
}
