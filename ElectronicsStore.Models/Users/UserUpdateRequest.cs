﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ElectronicsStore.Models.Users
{
    public class UserUpdateRequest
    {
        [Display(Name = "Tên đầy đủ")]
        public string? FullName { get; set; }
        public string? Email { get; set; }
        [Display(Name = "Số điện thoại")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }
    }
}
