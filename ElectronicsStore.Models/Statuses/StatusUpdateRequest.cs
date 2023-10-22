using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ElectronicsStore.Models.Statuses
{
    public class StatusUpdateRequest
    {
        public int Id { get; set; }
        [Display(Name = "Tên trạng thái")]
        public string Name { get; set; }
    }
}
