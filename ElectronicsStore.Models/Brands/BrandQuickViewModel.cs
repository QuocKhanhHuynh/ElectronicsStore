using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ElectronicsStore.Models.Brands
{
    public class BrandQuickViewModel
    {
        [Display(Name = "Mã nhãn hiệu")]
        public int Id { get; set; }
        [Display(Name = "Tên nhãn hiệu")]
        public string Name { get; set; }
        public string Logo { get; set; }
    }
}
