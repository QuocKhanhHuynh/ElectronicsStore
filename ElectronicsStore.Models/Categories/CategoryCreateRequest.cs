using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ElectronicsStore.Models.Categories
{
    public class CategoryCreateRequest
    {
        [Display(Name = "Tên loại hàng")]
        public string? Name { get; set; }
    }
}
