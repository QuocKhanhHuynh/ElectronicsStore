using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.AdminApp.Models.Product
{
    public class CategoryDeleteRequest
    {
        [Display(Name = "Mã loại hàng")]
        public int Id { get; set; }
    }
}
