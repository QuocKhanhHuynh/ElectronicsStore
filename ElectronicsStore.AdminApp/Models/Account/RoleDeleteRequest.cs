using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.AdminApp.Models.Account
{
    public class RoleDeleteRequest
    {
        [Display(Name = "Mã vai trò")]
        public string Id { get; set; }
    }
}
