using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.BackendApi.Data.Entities
{
    [Table("NHACUNGCAP")]
    public class Supplier
    {
        [Key]
        [Column("MaNhaCungCap")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("TenNhaCungCap")]
        public string Name { get; set; }

        [Required]
        [Column("Diachi")]
        public string Address { get; set; }

        [Required]
        [Column("DienThoai")]
        public string PhoneNumber { get; set; }
        [Required]
        [Column("SoTaiKhoan")]
        public string BankNumber { get; set; }
        [Required]
        [Column("TenNganHang")]
        public string BankName { get; set; }
        [Required]
        public string Email { get; set; }
        public List<ImportBill> ImportBills { get; set; }
    }
}
