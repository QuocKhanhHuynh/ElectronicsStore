using ElectronicsStore.ApiServices;
using ElectronicsStore.ClientApp.Helpers;
using ElectronicsStore.ClientApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ElectronicsStore.ClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductApiService _productApiService;
        public HomeController(IProductApiService productApiService)
        {
            _productApiService = productApiService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productApiService.Getproducts();
            Helper.LimitNameLength(products.ObjectResult);
            var model = new HomeViewModel()
            {
                Banners = products.ObjectResult.Where(x => x.OfferPrice > 0).OrderByDescending(x => x.OfferPrice).Take(5).ToList(),
                SaleProduct = products.ObjectResult.Where(x => x.OfferPrice > 0).OrderByDescending(x => x.OfferPrice).Take(10).ToList(),
                FeatureProduct = products.ObjectResult.OrderByDescending(x => x.PurchaseCount).Take(10).ToList(),
                LastProduct = products.ObjectResult.OrderByDescending(x => x.DateCreate).Take(10).ToList()
            };
            return View(model);
        }
    }
}