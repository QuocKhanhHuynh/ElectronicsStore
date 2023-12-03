using ElectronicsStore.AdminApp.Models.Home;
using ElectronicsStore.ApiServices;
using ElectronicsStore.Models.SaleBills;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectPdf;

namespace ElectronicsStore.AdminApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        private readonly ISaleBillApiService _saleBillApiService;
        private readonly IStatusApiService _statusApiService;
        public HomeController(ISaleBillApiService saleBillApiService, IStatusApiService statusApiService)
        {
            _saleBillApiService = saleBillApiService;
            _statusApiService = statusApiService;
        }

        public async Task<IActionResult> Index(int? statusId, int? id, int pageIndex = 1, int pageSize = 16)
        {
            if (TempData["Success"] != null)
            {
                ViewBag.Success = TempData["Success"];
            }
            var statusList = await _statusApiService.GetStatuses();
            var statuses = statusList.ObjectResult.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = statusId.HasValue && x.Id == statusId
            });
            var statusRecord= statusList.ObjectResult.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            });
            var billPagination = await _saleBillApiService.GetSaleBills(pageIndex, pageSize, null, statusId, id);
            var model = new HomeViewModel()
            {
                Id = id,
                Statuses = statuses,
                StatuRecord = statusRecord,
                Bills = billPagination.ObjectResult
            };
            return View(model);
        }

        public async Task<IActionResult> GetDetail(int id, int statusId)
        {
            var billDetails = await _saleBillApiService.GetSaleBillDetail(id);
            var statusList = await _statusApiService.GetStatuses();
            var statuses = statusList.ObjectResult.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = x.Id == statusId
            });
            //   ViewBag.StatusId = statusId;
            ViewBag.Statuses = statuses;
            return View(billDetails.ObjectResult);
        }

        public async Task<IActionResult> UpdateBill(int id, int statusId)
        {
            var request = new BillStatusUpdateRequest()
            {
                BillId = id,
                StatusId = statusId
            };
            var result = await _saleBillApiService.UpdateStatus(request);
            if (result.Status)
            {
                TempData["Success"] = $"Cập nhật trạng thái hóa đơn {id} thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Success"] = $"Cập nhật trạng thái hóa đơn {id} thất bại";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GeneratePdf(string content, int billId)
        {
             var converter = new HtmlToPdf();
             var document = converter.ConvertHtmlString(content);
             byte[] pdfBytes = document.Save();
             document.Close();
             return File(pdfBytes, "application/pdf", "SaleBill" + billId+ ".pdf");

        }
    }
}
