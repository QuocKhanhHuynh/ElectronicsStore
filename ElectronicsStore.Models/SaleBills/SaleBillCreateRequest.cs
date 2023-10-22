using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.SaleBills
{
    public class SaleBillCreateRequest
    {
        [Display(Name ="Tên người nhận")]
        public string RecipientName { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        public List<SaleBillDetailRequest>? DetailBill { get; set; } = new List<SaleBillDetailRequest>();
    }
}
