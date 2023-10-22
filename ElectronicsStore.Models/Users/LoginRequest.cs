﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models.Users
{
    public class LoginRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool Remember { get; set; }
    }
}
