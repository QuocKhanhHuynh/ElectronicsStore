using ElectronicsStore.Models.Commons;
using ElectronicsStore.Models.Products;
using ElectronicsStore.Models.Users;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectronicsStore.AdminApp.Models.Product
{
    public class ProductsViewModel
    {
        public Pagination<ProductBaseViewModel> Products { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public string Keyword { get; set; }
    }
}
