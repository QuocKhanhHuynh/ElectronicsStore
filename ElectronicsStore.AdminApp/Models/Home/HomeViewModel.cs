using ElectronicsStore.Models.Commons;
using ElectronicsStore.Models.Products;
using ElectronicsStore.Models.SaleBills;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectronicsStore.AdminApp.Models.Home
{
    public class HomeViewModel
    {
        public Pagination<SaleBillViewModel> Bills { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }
        public IEnumerable<SelectListItem> StatuRecord { get; set; }
        public int? Id { get; set; }
    }
}
