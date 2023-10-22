using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ElectronicsStore.Models.Roles
{
    public class RoleCreateRequest
    {
        [Display(Name = "Mã vai trò")]
        public string Id { get; set; }
        [Display(Name = "Tên vai trò")]
        public string Name { get; set; }
    }
}
