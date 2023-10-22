using ElectronicsStore.ApiServices;
using ElectronicsStore.BackendApi.Extendsions;
using ElectronicsStore.ClientApp.Models;
using ElectronicsStore.Models.SaleBills;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElectronicsStore.ClientApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IProductApiService _productApiService;
        private readonly ISaleBillApiService _saleBillApiService;
        private readonly IUserApiService _userApiService;
        public OrderController(IProductApiService productApiService, ISaleBillApiService saleBillApiService, IUserApiService userApiService)
        {
            _productApiService = productApiService;
            _saleBillApiService = saleBillApiService;
            _userApiService = userApiService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var session = HttpContext.Session.GetString("Cart");
            var cart = new List<CartItemViewModel>();
            if (session != null)
            {
                cart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
            }
            var product = await _productApiService.GetProductDetail(productId);
            int quanlity = 1;
            if (cart.Any(x => x.Id == productId))
            {
                var item = cart.Single(x => x.Id == productId);
                quanlity += item.Quanlity;
                cart.Remove(item);
            }
            var newItem = new CartItemViewModel()
            {
                Id = product.ObjectResult.Id,
                Name = product.ObjectResult.Name,
                Price = product.ObjectResult.OfferPrice > 0? product.ObjectResult.OfferPrice : product.ObjectResult.SalePrice,
                Quanlity = quanlity,
                Image = product.ObjectResult.Images[0],
                ImportBillId = product.ObjectResult.ImportBillId
            };
            cart.Add(newItem);
            HttpContext.Session.Remove("Cart");
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult LoadCart()
        {
            var session = HttpContext.Session.GetString("Cart");
            var cart = new List<CartItemViewModel>();
            if (session != null)
            {
                cart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
            }
            return Ok(cart);
        }

        [AllowAnonymous]
        public IActionResult GetListCart()
        {
            if (ViewData["Notify"] != null)
            {
                ViewBag.Notify = ViewData["Notify"];
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult UpdateQuanlityCart(int productId, int quanlity)
        {
            var session = HttpContext.Session.GetString("Cart");
            var cart = new List<CartItemViewModel>();
            if (session != null)
            {
                cart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
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

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var session = HttpContext.Session.GetString("Cart");
            var cart = new List<CartItemViewModel>();
            if (session != null)
            {
                cart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
            }
            foreach (var item in cart)
            {
                var result = await _saleBillApiService.CheckInventory(item.Id);
                if (result.ObjectResult < item.Quanlity)
                {
                    ViewData["Notify"] = $"Số lượng sản phẩm {item.Name} hiện chỉ còn {result.ObjectResult} sản phẩm";
                    return RedirectToAction("GetListCart");
                }
            }
            var user = await _userApiService.GetUserDetail(User.GetUserId());
            var request = new SaleBillCreateRequest()
            {
                RecipientName = user.ObjectResult.FullName,
                PhoneNumber = user.ObjectResult.PhoneNumber,
                Address = user.ObjectResult.Address
            };
            if (cart.Count() == 0)
            {
                ModelState.AddModelError("", "Không có sản phẩm để thanh toán");
            }
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(SaleBillCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var session = HttpContext.Session.GetString("Cart");
            var cart = new List<CartItemViewModel>();
            if (session != null)
            {
                cart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
            }
            var detailrequest = new List<SaleBillDetailRequest>();
            foreach (var item in cart)
            {
                detailrequest.Add(new SaleBillDetailRequest
                {
                    ProductId = item.Id,
                    Quantity = item.Quanlity,
                    SalePrice = item.Price,
                    ImportBillId = item.ImportBillId
                });
            }
            request.DetailBill = detailrequest;
            var result = await _saleBillApiService.CreateSaleBill(request);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            HttpContext.Session.Remove("Cart");
            ViewData["Notify"] = "Thanh toán thành công";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var bills = await _saleBillApiService.GetSaleBills(pageIndex, pageSize, User.GetUserId(),null,null);
            return View(bills.ObjectResult);
        }

        public async Task<IActionResult> GetBillDetails(int id)
        {
            var billDetails = await _saleBillApiService.GetSaleBillDetail(id);
            return View(billDetails.ObjectResult);
        }
    }
}
