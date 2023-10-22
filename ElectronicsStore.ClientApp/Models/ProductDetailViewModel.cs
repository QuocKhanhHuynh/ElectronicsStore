using ElectronicsStore.Models.Products;

namespace ElectronicsStore.ClientApp.Models
{
    public class ProductDetailViewModel
    {
        public ProductViewModel Product { get; set; }
        public List<ProductQuickViewModel> SaleProduct { get; set; }
        public List<ProductQuickViewModel> FeaturedProduct { get; set; }
        public List<ProductQuickViewModel> RelatedProduct { get; set; }
    }
}
