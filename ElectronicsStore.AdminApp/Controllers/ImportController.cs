using ElectronicsStore.AdminApp.Models.Import;
using ElectronicsStore.AdminApp.Models.Product;
using ElectronicsStore.ApiServices;
using ElectronicsStore.BackendApi.Data.Entities;
using ElectronicsStore.Models.Categories;
using ElectronicsStore.Models.ImportBills;
using ElectronicsStore.Models.Suppliers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace ElectronicsStore.AdminApp.Controllers
{
    [Authorize]
    public class ImportController : Controller
    {
        private readonly ISupplierApiService _supplierApiService;
        private readonly IProductApiService _productApiService;
        private readonly IImportBillApiService _importBillApiService;
        public readonly IUserApiService _userApiService;
        public ImportController(ISupplierApiService supplierApiService, IProductApiService productApiService, IImportBillApiService importBillApiService, IUserApiService userApiService)
        {
            _supplierApiService = supplierApiService;
            _productApiService = productApiService;
            _importBillApiService = importBillApiService;
            _userApiService = userApiService;
        }

        public async Task<IActionResult> GetSupplierPagination(int pageIndex = 1, int pageSize = 10)
        {
            if (TempData["Success"] != null)
            {
                ViewBag.Success = TempData["Success"];
            }
            var supplierPagination = await _supplierApiService.GetSupplierPagination(pageIndex, pageSize);
            return View(supplierPagination.ObjectResult);
        }

        [HttpGet]
        public IActionResult CreateSupplier()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier(SupplierCreateRequest request)
        {

            if (!ModelState.IsValid)
                return View(request);
            var result = await _supplierApiService.CreateSupplier(request);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Thêm mới nhà cung cấp thành công";
            return RedirectToAction("GetSupplierPagination");
        }

        public async Task<IActionResult> GetSupplierDetail(int id)
        {
            var supplier = await _supplierApiService.GetSupplierDetail(id);
            return View(supplier.ObjectResult);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSupplier(int id)
        {
            var supplier = await _supplierApiService.GetSupplierDetail(id);
            var request = new SupplierUpdateRequest()
            {
                Id = supplier.ObjectResult.Id,
                Name = supplier.ObjectResult.Name,
                Address = supplier.ObjectResult.Address,
                BankName = supplier.ObjectResult.BankName,
                BankNumber = supplier.ObjectResult.BankNumber,
                Email = supplier.ObjectResult.Email,
                PhoneNumber = supplier.ObjectResult.PhoneNumber
            };
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSupplier(SupplierUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _supplierApiService.UpdateSupplier(request);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Cập nhật nhà cung cấp thành công";
            return RedirectToAction("GetSupplierPagination");
        }

        [HttpGet]
        public IActionResult DeleteSupplier(int id)
        {
            var request = new SupplierDeleteRequest()
            {
                Id = id
            };
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSupplier(SupplierDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _supplierApiService.DeleteSupplier(request.Id);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Xóa nhà cung cấp thành công";
            return RedirectToAction("GetSupplierPagination");
        }



        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var session = HttpContext.Session.GetString("Cart");
            var cart = new List<ImportItemViewModel>();
            if (session!= null)
            {
                cart = JsonConvert.DeserializeObject<List<ImportItemViewModel>>(session);
            }
            var product = await _productApiService.GetDetailInformation(productId);
            if (cart.Any(x => x.Id == productId) == false)
            {
                cart.Add(new ImportItemViewModel()
                {
                    Id = product.ObjectResult.Id,
                    Name = product.ObjectResult.Name,
                    Image = product.ObjectResult.Images[0],
                    Quanlity = 1,
                    Price = 0
                });
            }
            HttpContext.Session.Remove("Cart");
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            return Ok();
        }

        [HttpGet]
        public IActionResult LoadCart()
        {
            var session = HttpContext.Session.GetString("Cart");
            var cart = new List<ImportItemViewModel>();
            if (session != null)
            {
                cart = JsonConvert.DeserializeObject<List<ImportItemViewModel>>(session);
            }
            return Ok(cart);
        }

        public IActionResult GetListCart()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateQuanlityCart(int productId, int quanlity)
        {
            var session = HttpContext.Session.GetString("Cart");
            var cart = new List<ImportItemViewModel>();
            if (session != null)
            {
                cart = JsonConvert.DeserializeObject<List<ImportItemViewModel>>(session);
            }
            var item = cart.Single(x => x.Id == productId);
            item.Quanlity = quanlity;
            if (quanlity == 0)
            {
                cart.Remove(item);
            }
            HttpContext.Session.Remove("Cart");
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            return Ok();
        }

        [HttpPost]
        public IActionResult UpdatePriceCart(int productId, int importPrice)
        {
            var session = HttpContext.Session.GetString("Cart");
            var cart = new List<ImportItemViewModel>();
            if (session != null)
            {
                cart = JsonConvert.DeserializeObject<List<ImportItemViewModel>>(session);
            }
            var item = cart.Single(x => x.Id == productId);
            item.Price = importPrice;
            HttpContext.Session.Remove("Cart");
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id, string? userId,int pageIndex = 1, int pageSize = 10)
        {
            if (TempData["Success"] != null)
            {
                ViewBag.Success = TempData["Success"];
            }
            var adminsList = await _userApiService.GetAdmins();
            var admins = adminsList.ObjectResult.Select(x => new SelectListItem()
            {
                Text = x.UserName,
                Value = x.Id,
                Selected = userId != null && x.Id.Equals(userId)
            });
            var billPagination = await _importBillApiService.GetImportBills(id,userId,pageIndex, pageSize);
            var model = new ImportBillsViewModel()
            {
                Admins = admins,
                Id = id,
                Bills = billPagination.ObjectResult
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductNotSoldout(int pageIndex = 1, int pageSize = 10)
        {
            var products = await _productApiService.GetProductPagination(null,null,pageIndex, pageSize);
            return View(products.ObjectResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductSoldout(int pageIndex = 1, int pageSize = 10)
        {
            var products = await _productApiService.GetSoldoutProduct(pageIndex, pageSize);
            return View(products.ObjectResult);
        }

        [HttpGet]
        public async Task<IActionResult> CreateImportBill()
        {
            var suppliersList = await _supplierApiService.GetSuppliers();
            var suppliers = suppliersList.ObjectResult.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.Suppliers = suppliers;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateImportBill(ImportBillCreateRequest request)
        {
            var suppliersList = await _supplierApiService.GetSuppliers();
            var suppliers = suppliersList.ObjectResult.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.Suppliers = suppliers;
            if (!ModelState.IsValid)
                return View(request);
            var session = HttpContext.Session.GetString("Cart");
            var cart = new List<ImportItemViewModel>();
            if (session != null)
            {
                cart = JsonConvert.DeserializeObject<List<ImportItemViewModel>>(session);
            }
            if (cart.Count() == 0)
            {
                TempData["Success"] = "Hãy chọn sản phẩm để tạo hóa đơn";
                return RedirectToAction("Index","Product");
            }
            var requestDetail = cart.Select(x => new ImportDetailBillCreateRequest()
            {
                ProductId = x.Id,
                Quantity = x.Quanlity,
                ImportPrice = x.Price
            }).ToList();
            request.DetailBill = requestDetail;
            var result = await _importBillApiService.CreateImportBill(request);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            HttpContext.Session.Remove("Cart");
            TempData["Success"] = "Tạo hoá đơn thành công";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetImportBillDetail(int id)
        {
            var bill = await _importBillApiService.GetImportBillDetail(id);
            return View(bill.ObjectResult);
        }
    }
}
