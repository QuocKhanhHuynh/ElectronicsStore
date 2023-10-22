using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ElectronicsStore.Models.SaleBills
{
    public class SaleBillInformation
    {
        [Display(Name = "Mã hóa đơn")]
        public int Id { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime DateCreate { get; set; }
        [Display(Name = "Tổng giá trị")]
        public int TotalValue { get; set; }
        [Display(Name = "Tên người nhận")]
        public string RecipientName { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        public List<SaleBillDetailViewModel> Details { get; set; }
    }
}
