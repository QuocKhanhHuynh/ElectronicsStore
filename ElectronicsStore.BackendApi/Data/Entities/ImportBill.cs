using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.BackendApi.Data.Entities
{
    [Table("HOADONNHAPHANG")]
    public class ImportBill
    {
        [Key]
        [Column("MaDonNhap")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("NgayNhap")]
        public DateTime DateCreate { get; set; }

        [Required]
        [Column("TongGiaTri")]
        public int TotalValue { get; set; }
        public string UserId { get; set; }
        public int SupplierId { get; set; }
        public User User { get; set; }
        public Supplier Supplier { get; set; }
        public List<ImportBillDetail> ImportBillDetails { get; set; }
    }
}
