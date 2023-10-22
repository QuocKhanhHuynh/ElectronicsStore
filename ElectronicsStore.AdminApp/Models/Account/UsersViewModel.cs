using ElectronicsStore.Models.Commons;
using ElectronicsStore.Models.Roles;
using ElectronicsStore.Models.Users;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectronicsStore.AdminApp.Models.Account
{
    public class UsersViewModel
    {
        public Pagination<UserQuickViewModel> Users { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
        public string UserName { get; set; }
    }
}
