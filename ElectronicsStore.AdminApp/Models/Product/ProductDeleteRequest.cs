using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.AdminApp.Models.Product
{
    public class ProductDeleteRequest
    {
        [Display(Name = "Mã sản phẩm")]
        public int Id { get; set; }
    }
}
