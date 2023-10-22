using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ElectronicsStore.BackendApi.Data.Entities
{
    [Table("NGUOIDUNG")]
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public DateTime DateCreate { get; set; }
        public List<ImportBill> ImportBills { get; set; }
        public List<SaleBill> SaleBills { get; set; }
    }
}
