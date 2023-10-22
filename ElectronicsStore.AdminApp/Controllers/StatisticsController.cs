using ElectronicsStore.ApiServices;
using ElectronicsStore.Models.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.AdminApp.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly ISaleBillApiService _saleBillApiService;
        private readonly IUserApiService _userApiService;
        public StatisticsController(ISaleBillApiService saleBillApiService, IUserApiService userApiService)
        {
            _saleBillApiService = saleBillApiService;
            _userApiService = userApiService;
        }
        [HttpGet]
        public async Task<IActionResult> Revenue(int? timeLine, int pageIndex =1, int pageSize =16 )
        {
            if (timeLine.HasValue)
            {
                ViewBag.TimeLine = timeLine.Value;
            }
            var datas = await _saleBillApiService.StatisticsRevenue(pageIndex, pageSize, timeLine);
            return View(datas.ObjectResult);
        }

        [HttpGet]
        public async Task<IActionResult> User(int? timeLine, int pageIndex = 1, int pageSize = 16)
        {
            if (timeLine.HasValue)
            {
                ViewBag.TimeLine = timeLine.Value;
            }
            var datas = await _userApiService.GetStatistics(pageIndex, pageSize, timeLine);
            return View(datas.ObjectResult);
        }
    }
}
