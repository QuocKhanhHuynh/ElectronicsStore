using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.BackendApi.Data.Entities
{
    [Table("NHANHIEU")]
    public class Brand
    {
        [Key]
        [Column("MaNhanHieu")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("TenNhanHieu")]
        public string Name { get; set; }

        [Required]
        public string Logo { get; set; }
        public List<Product> Products { get; set; }
    }
}
