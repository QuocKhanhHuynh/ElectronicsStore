using ElectronicsStore.Models.Products;

namespace ElectronicsStore.ClientApp.Models
{
    public class HomeViewModel
    {
        public List<ProductQuickViewModel> Banners { get; set; }
        public List<ProductQuickViewModel> SaleProduct { get; set; }
        public List<ProductQuickViewModel> FeatureProduct { get; set; }
        public List<ProductQuickViewModel> LastProduct { get; set; }
    }
}
