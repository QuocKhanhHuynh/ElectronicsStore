using ElectronicsStore.ApiServices;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.ClientApp.Controllers.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryApiService _categoryApiService;
        public CategoryViewComponent(ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryApiService.GetCategories();
            return View("Default", categories.ObjectResult);
        }
    }
}
