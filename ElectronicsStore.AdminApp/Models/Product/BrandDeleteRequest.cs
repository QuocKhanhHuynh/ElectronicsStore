using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.AdminApp.Models.Product
{
    public class BrandDeleteRequest
    {
        [Display(Name = "Mã nhãn hiệu")]
        public int Id { get; set; }
    }
}
