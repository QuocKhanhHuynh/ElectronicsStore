using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.Users
{
    public class AccountRegisterRequest
    {
        [Display(Name = "Username")]
        public string? UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        public string? Password { get; set; }
        [Display(Name = "Xác nhận mật khẩu")]
        public string? ConfirmPassword { get; set; }
        [Display(Name = "Tên đầy đủ")]
        public string? FullName { get; set; }
        public string? Email { get; set; }
        [Display(Name = "Số điện thoại")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }
        public string? RoleId { get; set; }
    }
}
