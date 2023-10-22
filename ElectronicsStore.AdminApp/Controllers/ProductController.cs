using ElectronicsStore.AdminApp.Models.Product;
using ElectronicsStore.ApiServices;
using ElectronicsStore.BackendApi.Data.Entities;
using ElectronicsStore.Models.Brands;
using ElectronicsStore.Models.Categories;
using ElectronicsStore.Models.Products;
using ElectronicsStore.Models.Statuses;
using ElectronicsStore.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectronicsStore.AdminApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ICategoryApiService _categoryApiService;
        private readonly IBrandApiService _brandApiService;
        private readonly IProductApiService _productApiService;
        private readonly IStatusApiService _statusApiService;
        public ProductController(ICategoryApiService categoryApiService, IBrandApiService brandApiService, IProductApiService productApiService, IStatusApiService statusApiService)
        {
            _categoryApiService = categoryApiService;
            _brandApiService = brandApiService;
            _productApiService = productApiService;
            _statusApiService = statusApiService;
        }
        public async Task<IActionResult> GetCategoryPagination(int pageIndex = 1, int pageSize = 10)
        {
            if (TempData["Success"] != null)
            {
                 ViewBag.Success = TempData["Success"];
            }
            var categoryPagination = await _categoryApiService.GetCategoryPagination(pageIndex, pageSize);
            return View(categoryPagination.ObjectResult);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateRequest request)
        {

            if (!ModelState.IsValid)
                return View(request);
            var result = await _categoryApiService.CreateCategory(request);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Thêm mới loại hàng thành công";
            return RedirectToAction("GetCategoryPagination");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var category = await _categoryApiService.GetCategoryDetail(id);
            var request = new CategoryUpdateRequest()
            {
                Id = category.ObjectResult.Id,
                Name = category.ObjectResult.Name
            };
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _categoryApiService.UpdateCategory(request);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Cập nhật loại hàng thành công";
            return RedirectToAction("GetCategoryPagination");
        }

        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            var request = new CategoryDeleteRequest()
            {
                Id = id
            };
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(CategoryDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _categoryApiService.DeleteCategory(request.Id);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Xóa loại hàng thành công";
            return RedirectToAction("GetCategoryPagination");
        }

        public async Task<IActionResult> GetBrandPagination(int pageIndex = 1, int pageSize = 10)
        {
            if (TempData["Success"] != null)
            {
                ViewBag.Success = TempData["Success"];
            }
            var brandPagination = await _brandApiService.GetBrandPagination(pageIndex, pageSize);
            return View(brandPagination.ObjectResult);
        }

        [HttpGet]
        public IActionResult CreateBrand()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateBrand(BrandCreateRequest request)
        {

            if (!ModelState.IsValid)
                return View(request);
            var result = await _brandApiService.CreateBrand(request);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Thêm mới nhãn hiệu thành công";
            return RedirectToAction("GetBrandPagination");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBrand(int id)
        {
            var brand = await _brandApiService.GetBrandDetail(id);
            var request = new BrandUpdateRequest()
            {
                Id = brand.ObjectResult.Id,
                Name = brand.ObjectResult.Name
            };
            return View(request);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateBrand(BrandUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _brandApiService.UpdateBrand(request);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Cập nhật nhãn hiệu thành công";
            return RedirectToAction("GetBrandPagination");
        }

        [HttpGet]
        public IActionResult DeleteBrand(int id)
        {
            var request = new BrandDeleteRequest()
            {
                Id = id
            };
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBrand(BrandDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _brandApiService.DeleteBrand(request.Id);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Xóa nhãn hiệu thành công";
            return RedirectToAction("GetBrandPagination");
        }

        public async Task<IActionResult> Index(string? keyword, int? categoryId, int pageIndex = 1, int pageSize = 10)
        {
            if (TempData["Success"] != null)
            {
                ViewBag.Success = TempData["Success"];
            }
            var categoryList = await _categoryApiService.GetCategories();
            var categories = categoryList.ObjectResult.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = categoryId.HasValue && x.Id == categoryId
            });
            var productPagination = await _productApiService.GetProductInformation(keyword, categoryId, pageIndex, pageSize);
            var model = new ProductsViewModel()
            {
                Keyword = keyword,
                Categories = categories,
                Products = productPagination.ObjectResult
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var categoryList = await _categoryApiService.GetCategories();
            ViewBag.Categories = categoryList.ObjectResult.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            });
            var brandList = await _brandApiService.GetBrands();
            ViewBag.Brands = brandList.ObjectResult.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            });
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateProduct(ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var categoryList = await _categoryApiService.GetCategories();
                ViewBag.Categories = categoryList.ObjectResult.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                });
                var brandList = await _brandApiService.GetBrands();
                ViewBag.Brands = brandList.ObjectResult.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                });
                return View(request);
            }
            var result = await _productApiService.CreateProduct(request);
            if (!result.Status)
            {
                var categoryList = await _categoryApiService.GetCategories();
                ViewBag.Categories = categoryList.ObjectResult.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                });
                var brandList = await _brandApiService.GetBrands();
                ViewBag.Brands = brandList.ObjectResult.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                });
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Thêm mới sản phẩm thành công";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var product = await _productApiService.GetDetailInformation(id);//
            var request = new ProductUpdateRequest()
            {
               Id = product.ObjectResult.Id,
               Description = product.ObjectResult.Description,
               Name = product.ObjectResult.Name,
               SalePrice = product.ObjectResult.SalePrice,
               OfferPrice = product.ObjectResult.OfferPrice,
               Origin = product.ObjectResult.Origin,
               Introduce = product.ObjectResult.Introduce
            };
            return View(request);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProduct(ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _productApiService.UpdateProduct(request);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Cập nhật sản phẩm thành công";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetProductDetail(int id)
        {
            var product = await _productApiService.GetDetailInformation(id);//
            return View(product.ObjectResult);
        }

        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            var request = new ProductDeleteRequest()
            {
                Id = id
            };
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(ProductDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _productApiService.DeleteProduct(request.Id);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Xóa sản phẩm thành công";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetStatuses()
        {
            if (TempData["Success"] != null)
            {
                ViewBag.Success = TempData["Success"];
            }
            var statuses = await _statusApiService.GetStatuses();
            return View(statuses.ObjectResult);
        }

        [HttpGet]
        public IActionResult CreateStatus()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatus(StatusCreateRequest request)
        {

            if (!ModelState.IsValid)
                return View(request);
            var result = await _statusApiService.CreateStatus(request);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Thêm mới trạng thái thành công";
            return RedirectToAction("GetStatuses");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int id)
        {
            var status = await _statusApiService.GetStatusDetail(id);
            var request = new StatusUpdateRequest()
            {
                Id = status.ObjectResult.Id,
                Name = status.ObjectResult.Name
            };
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(StatusUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _statusApiService.UpdateStatus(request.Id,request);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Cập nhật trạng thái thành công";
            return RedirectToAction("GetStatuses");
        }

        [HttpGet]
        public IActionResult DeleteStatus(int id)
        {
            var request = new StatusDeleteRequest()
            {
                Id = id
            };
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStatus(StatusDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _statusApiService.DeleteStatus(request.Id);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Xóa trạng thái thành công";
            return RedirectToAction("GetStatuses");
        }
    }
}
