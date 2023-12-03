using ElectronicsStore.ApiServices;
using ElectronicsStore.ClientApp.Helpers;
using ElectronicsStore.ClientApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectronicsStore.ClientApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiService _productApiService;

        public ProductController(IProductApiService productApiService)
        {
            _productApiService = productApiService;
        }

        public async Task<IActionResult> Index(string? keyword, int? categoryId, int? brandId, int pageIndex = 1, int pageSize = 16)
        {
            ViewBag.Keyword = keyword;
            var productPagination = await _productApiService.GetProductPagination(keyword, categoryId, brandId, pageIndex, pageSize);
            Helper.LimitNameLength(productPagination.ObjectResult.Items);
            return View(productPagination.ObjectResult);
        }

        public async Task<IActionResult> GetProductDetail(int id)
        {
            var productDetail = await _productApiService.GetProductDetail(id);
            var products = await _productApiService.Getproducts();
            var model = new ProductDetailViewModel()
            {
                Product = productDetail.ObjectResult,
                FeaturedProduct = products.ObjectResult.OrderByDescending(x => x.PurchaseCount).Take(8).ToList(),
                SaleProduct = products.ObjectResult.Where(x => x.OfferPrice > 0).OrderByDescending(x => x.OfferPrice).Take(8).ToList(),
                RelatedProduct = products.ObjectResult.Where(x => x.CategoryId == productDetail.ObjectResult.CategoryId).Take(6).ToList(),
            };
            return View(model);
        }
    }
}
