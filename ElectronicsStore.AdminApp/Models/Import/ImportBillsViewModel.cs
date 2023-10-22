using ElectronicsStore.Models.Commons;
using ElectronicsStore.Models.ImportBills;
using ElectronicsStore.Models.Products;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectronicsStore.AdminApp.Models.Import
{
    public class ImportBillsViewModel
    {
        public Pagination<ImportBillViewModel> Bills { get; set; }
        public IEnumerable<SelectListItem> Admins { get; set; }
        public int? Id { get; set; }
    }
}
