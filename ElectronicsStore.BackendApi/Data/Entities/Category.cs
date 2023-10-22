using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.BackendApi.Data.Entities
{
    [Table("LOAIHANG")]
    public class Category
    {
        [Key]
        [Column("MaLoaiHang")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("TenLoai")]
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
