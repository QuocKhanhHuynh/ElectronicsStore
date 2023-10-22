using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.BackendApi.Data.Entities
{
    [Table("SANPHAM")]
    public class Product
    {
        [Key]
        [Column("MaSanPham")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("TenSanPham")]
        public string Name { get; set; }

        [Required]
        [Column("GiaBan")]
        public int SalePrice { get; set; }

        [Column("GiaGiam")]
        [Required]
        public int OfferPrice { get; set; }

        [Required]
        [Column("NguonGoc")]
        public string Origin { get; set; }

        [Required]
        [Column("GioiThieu")]
        public string Introduce { get; set; }

        [Required]
        [Column("MoTa")]
        public string Description { get; set; }

        [Column("SoLuongMua")]
        public int PurchaseCount { get; set; }
        [Column("NgayTao")]
        public DateTime DateCreate { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public List<Image> Images { get; set; }
        public List<ImportBillDetail> ImportBillDetails { get; set; }
        public List<SaleBillDetail> SaleBillDetails { get; set; }
    }
}
