using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ElectronicsStore.Models.Users
{
    public class PasswordForgetRequest
    {
        [Display(Name = "Username")]
        public string? UserName { get; set; }
        public string? Email { get; set; }
        [Display(Name = "Mật khẩu mới")]
        public string? NewPassword { get; set; }
        [Display(Name = "Xác nhận mật khẩu mới")]
        public string? ConfirmNewPassword { get; set; }
    }
}
