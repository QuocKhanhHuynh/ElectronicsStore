using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.BackendApi.Data.Entities
{
    [Table("ANH")]
    public class Image
    {
        [Key]
        [Column("MaAnh")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("DuongDan")]
        public string Url { get; set; }

        [Column("MacDinh")]
        public bool IsDefaul { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
