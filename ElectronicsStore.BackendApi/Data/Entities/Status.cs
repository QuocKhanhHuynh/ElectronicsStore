using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.BackendApi.Data.Entities
{
    [Table("TRANGTHAI")]
    public class Status
    {
        [Key]
        [Column("MaTrangThai")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("TenTrangThai")]
        public string Name { get; set; }
        public List<SaleBill> SaleBills { get; set; }
    }
}
