using ElectronicsStore.ApiServices;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.ClientApp.Controllers.Components
{
    public class BrandViewComponent : ViewComponent
    {
        private readonly IBrandApiService _brandApiService;
        public BrandViewComponent(IBrandApiService brandApiService)
        {
            _brandApiService = brandApiService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brand = await _brandApiService.GetBrands();
            return View("Default", brand.ObjectResult);
        }
    }
}
