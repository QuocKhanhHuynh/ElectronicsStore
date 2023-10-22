using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.AdminApp.Models.Import
{
    public class SupplierDeleteRequest
    {
        [Display(Name = "Mã nhà cung cấp")]
        public int Id { get; set; }
    }
}
