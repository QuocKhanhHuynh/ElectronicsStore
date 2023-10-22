using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.BackendApi.Data.Entities
{
    [Table("CHITIETHOADONBAN")]
    public class SaleBillDetail
    {
        [Required]
        [Column("SoLuong")]
        public int Quantity { get; set; }

        [Required]
        [Column("GiaBan")]
        public int SalePrice { get; set; }
        public int ProductId { get; set; }
        public int SaleBillId { get; set; }
        public int ImportBillId { get; set; }
        public Product Product { get; set; }
        public SaleBill SaleBill { get; set; }
    }
}
