using ElectronicsStore.Models.Commons;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.ClientApp.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PaginationBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
