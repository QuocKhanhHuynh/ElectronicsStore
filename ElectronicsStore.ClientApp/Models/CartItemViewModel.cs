using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.ClientApp.Models
{
    public class CartItemViewModel
    {
        [Display(Name = "Mã sản phẩm")]
        public int Id { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Ảnh")]
        public string Image { get; set; }
        [Display(Name = "Số lượng")]
        public int Quanlity { get; set; }
        [Display(Name = "Giá")]
        public int Price { get; set; }
        public int ImportBillId { get; set; }
    }
}
