using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.AdminApp.Models.Product
{
    public class StatusDeleteRequest
    {

        [Display(Name = "Mã trạng thái")]
        public int Id { get; set; }
    }
}
