using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ElectronicsStore.Models.Suppliers
{
    public class SupplierUpdateRequest
    {
        [Display(Name = "Mã nhà cung cấp")]
        public int Id { get; set; }
        [Display(Name = "Tên nhà cung cấp")]
        public string Name { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Số tài khoản")]
        public string BankNumber { get; set; }
        [Display(Name = "Tên ngân hàng")]
        public string BankName { get; set; }
        public string Email { get; set; }
    }
}
