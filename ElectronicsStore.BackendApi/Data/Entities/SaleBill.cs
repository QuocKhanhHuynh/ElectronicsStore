using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.BackendApi.Data.Entities
{
    [Table("HOADONBANHANG")]
    public class SaleBill
    {
        [Key]
        [Column("MaDonBan")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("NgayLap")]
        public DateTime DateCreate { get; set; }

        [Required]
        [Column("TenNguoiNhan")]
        public string RecipientName { get; set; }

        [Required]
        [Column("DiaChiNhan")]
        public string Address { get; set; }

        [Required]
        [Column("SoDienThoai")]
        public string PhoneNumber { get; set; }

        [Required]
        [Column("TongThanhToan")]
        public int ToTalPayment { get; set; }
        public string UserId { get; set; }
        public int StatusId { get; set; }
        public User User { get; set; }
        public Status Status { get; set; }
        public List<SaleBillDetail> SaleBillDetails { get; set; }
    }
}
