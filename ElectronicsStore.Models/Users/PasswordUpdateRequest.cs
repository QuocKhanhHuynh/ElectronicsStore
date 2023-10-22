using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.Users
{
    public class PasswordUpdateRequest
    {
        [Display(Name ="Mật khẩu hiện tại")]
        public string? CurrentPassword { get; set; }
        [Display(Name = "Mật khẩu mới")]
        public string? NewPassword { get; set; }
        [Display(Name = "Xác nhận mật khẩu")]
        public string? ConfirmNewPassword { get; set; }
    }
}
