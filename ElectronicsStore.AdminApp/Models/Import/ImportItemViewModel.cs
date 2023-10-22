using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.AdminApp.Models.Import
{
    public class ImportItemViewModel
    {
        [Display(Name = "Mã sản phẩm")]
        public int Id { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Ảnh")]
        public string Image { get; set; }
        [Display(Name = "Số lượng")]
        public int Quanlity { get; set; }
        [Display(Name = "Giá nhập")]
        public int Price { get; set; }
    }
}
