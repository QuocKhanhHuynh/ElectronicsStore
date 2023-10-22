using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.BackendApi.Data.Entities
{
    [Table("CHITIETHOADONNHAP")]
    public class ImportBillDetail
    {
        [Required]
        [Column("SoLuong")]
        public int Quantity { get; set; }

        [Required]
        [Column("GiaNhap")]
        public int ImportPrice { get; set; }

        [Required]
        [Column("HangTon")]
        public int Inventory { get; set; }
        public int ImportBillId { get; set; }
        public int ProductId { get; set; }
        public ImportBill ImportBill { get; set; }
        public Product Product { get; set; }
    }
}
