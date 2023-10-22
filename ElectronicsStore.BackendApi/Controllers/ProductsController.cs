using ElectronicsStore.BackendApi.Data.Entities;
using ElectronicsStore.BackendApi.Data;
using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.BackendApi.Services;
using ElectronicsStore.Models.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectronicsStore.Models.Commons;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace ElectronicsStore.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStorageService _storageService;
        public ProductsController(ApplicationDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Policy = "auth1")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateRequest request)
        {
            var product = new Product()
            {
                Name = request.Name,
                SalePrice = request.SalePrice.GetValueOrDefault(),
                OfferPrice = request.OfferPrice.GetValueOrDefault(0),
                Origin = request.Origin,
                Introduce = request.Introduce,
                Description = request.Description,
                BrandId = request.BrandId.GetValueOrDefault(),
                CategoryId = request.CategoryId.GetValueOrDefault(),
                PurchaseCount = 0
            };
            product.Images = new List<Image>();
            product.Images.Add(new Image()
            {
                IsDefaul = true,
                Url = await this.SaveFile(request.Image[0])
            });
            if (request.Image.Count() > 1)
            {
                for (var i = 1; i < request.Image.Count(); i++)
                {
                    product.Images.Add(new Image()
                    {
                        Url = await this.SaveFile(request.Image[i])
                    });
                }
            }
            await _context.Products.AddAsync(product);
            var result = _context.SaveChanges();
            if (result > 0)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>("Không thể thêm sản phẩm"));
            }
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [Authorize(Policy = "auth1")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new ApiResponseFailure<bool>($"Không tìm thấy sản phẩm có mã {id}"));
            }
            product.Name = request.Name;
            product.SalePrice = request.SalePrice.GetValueOrDefault();
            product.OfferPrice = request.OfferPrice.GetValueOrDefault();
            product.Origin = request.Origin;
            product.Introduce = request.Introduce;
            product.Description = request.Description;
            _context.Products.Update(product);
            if (request.Image != null)
            {
                var productImage = _context.Images.Where(x => x.ProductId == id);
                if (productImage != null)
                {
                    _context.Images.RemoveRange(productImage);
                }
                foreach (var image in productImage)
                {
                    await _storageService.DeleteFileAsync(image.Url);
                }
                await _context.Images.AddAsync(new Image()
                {
                    IsDefaul = true,
                    ProductId = id,
                    Url = await this.SaveFile(request.Image[0])
                });
                if (request.Image.Count() > 1)
                {
                    for (var i = 1; i < request.Image.Count(); i++)
                    {
                        await _context.Images.AddAsync(new Image()
                        {
                            ProductId = id,
                            Url = await this.SaveFile(request.Image[i])
                        });
                    }
                }
            }
            var result = _context.SaveChanges();
            if (result > 0)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>("Không thể cập nhật sản phẩm"));
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Policy = "auth1")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new ApiResponseFailure<bool>($"Không tìm thấy sản phẩm có mã {id}"));
            }
            _context.Products.Remove(product);
            var productImage = _context.Images.Where(x => x.ProductId == id);
            if (productImage != null)
            {
                _context.Images.RemoveRange(productImage);
            }
            foreach (var image in productImage)
            {
                await _storageService.DeleteFileAsync(image.Url);
            }
            var result = _context.SaveChanges();
            if (result > 0)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>("Không thể xóa sản phẩm"));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id
                        join b in _context.Brands on p.BrandId equals b.Id
                        join i in _context.Images on p.Id equals i.ProductId
                        join ibd in _context.ImportBillDetails on p.Id equals ibd.ProductId 
                        where ibd.Inventory > 0 && i.IsDefaul == true
                        select new { p, c, b, i, ibd };
            var products = await query.Select(x => new ProductQuickViewModel()
            {
                Id = x.p.Id,
                ImportBillId = x.ibd.ImportBillId,
                Name = x.p.Name,
                ImportPrice = x.ibd.ImportPrice,
                SalePrice = x.p.SalePrice,
                OfferPrice = x.p.OfferPrice,
                Inventory = x.ibd.Inventory,
                PurchaseCount = x.p.PurchaseCount,
                DateCreate = x.p.DateCreate,
                DefaultImage = x.i.Url,
                CategoryId = x.p.CategoryId
            }).ToListAsync();
            return Ok(new ApiResponseSuccess<List<ProductQuickViewModel>>(products));
        }
      
        [HttpGet("Pagination")]
        public async Task<IActionResult> GetProductsPagination(string? keyword, int? categoryId, int pageIndex, int pageSize)
        {
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id
                        join b in _context.Brands on p.BrandId equals b.Id
                        join i in _context.Images on p.Id equals i.ProductId
                        join ibd in _context.ImportBillDetails on p.Id equals ibd.ProductId
                        where ibd.Inventory > 0 && i.IsDefaul == true
                        select new { p, c, b, i, ibd };
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.p.Name.Contains(keyword));
            }
            if (categoryId.HasValue)
            {
                query = query.Where(x => x.p.CategoryId == categoryId);
            }
            var totalItem = query.Count();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(x => new ProductQuickViewModel()
            {
                Id = x.p.Id,
                ImportBillId = x.ibd.ImportBillId,
                Name = x.p.Name,
                ImportPrice = x.ibd.ImportPrice,
                SalePrice = x.p.SalePrice,
                OfferPrice = x.p.OfferPrice,
                Inventory = x.ibd.Inventory,
                PurchaseCount = x.p.PurchaseCount,
                DefaultImage = x.i.Url
            }).ToListAsync();

            var pagination = new Pagination<ProductQuickViewModel>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items,
                TotalRecords = totalItem
            };
            return Ok(new ApiResponseSuccess<Pagination<ProductQuickViewModel>>(pagination));
        }

        [HttpGet("Information")]
        [Authorize(Policy = "auth1")]
        public async Task<IActionResult> GetProductsInformation(string? keyword, int? categoryId, int pageIndex, int pageSize)
        {
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id
                        join b in _context.Brands on p.BrandId equals b.Id
                        join i in _context.Images on p.Id equals i.ProductId
                        where i.IsDefaul == true
                        orderby p.DateCreate descending
                        select new { p, c, b, i };
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.p.Name.Contains(keyword));
            }
            if (categoryId.HasValue)
            {
                query = query.Where(x => x.p.CategoryId == categoryId);
            }
            var totalItem = query.Count();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(x => new ProductBaseViewModel()
            {
                Id = x.p.Id,
                Name = x.p.Name,
                SalePrice = x.p.SalePrice,
                OfferPrice = x.p.OfferPrice,
                PurchaseCount = x.p.PurchaseCount,
                DefaultImage = x.i.Url
            }).ToListAsync();

            var pagination = new Pagination<ProductBaseViewModel>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items,
                TotalRecords = totalItem
            };
            return Ok(new ApiResponseSuccess<Pagination<ProductBaseViewModel>>(pagination));
        }

        [HttpGet("soldout")]
        [Authorize(Policy = "auth1")]
        public async Task<IActionResult> GetSoldoutProduct(int pageIndex, int pageSize)
        {
            var query = from p in _context.Products
                        join i in _context.Images on p.Id equals i.ProductId
                        join ibd in _context.ImportBillDetails on p.Id equals ibd.ProductId
                        where ibd.Inventory == 0 && i.IsDefaul == true
                        select new { p, i };
            var totalItem = query.Count();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(x => new ProductBaseViewModel()
            {
                Id = x.p.Id,
                Name = x.p.Name,
                SalePrice = x.p.SalePrice,
                OfferPrice = x.p.OfferPrice,
                PurchaseCount = x.p.PurchaseCount,
                DefaultImage = x.i.Url
            }).ToListAsync();

            var pagination = new Pagination<ProductBaseViewModel>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items,
                TotalRecords = totalItem
            };
            return Ok(new ApiResponseSuccess<Pagination<ProductBaseViewModel>>(pagination));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id
                        join b in _context.Brands on p.BrandId equals b.Id
                        join ibd in _context.ImportBillDetails on p.Id equals ibd.ProductId
                        where ibd.Inventory > 0 && ibd.ProductId == id
                        select new { p, c, b, ibd };
            var product = (await query.ToListAsync())[0];
            if (product == null)
            {
                return NotFound(new ApiResponseFailure<ProductViewModel>($"Không tìm thấy sản phẩm có mã {id}"));
            }
            var result = new ProductViewModel()
            {
                Id = product.p.Id,
                Name = product.p.Name,
                BrandName = (await _context.Brands.FindAsync(product.b.Id)).Name,
                CategoryName = (await _context.Categories.FindAsync(product.c.Id)).Name,
                Description = product.p.Description,
                Introduce = product.p.Introduce,
                SalePrice = product.p.SalePrice,
                Origin = product.p.Origin,
                PurchaseCount = product.p.PurchaseCount,
                OfferPrice = product.p.OfferPrice,
                CategoryId = product.p.CategoryId,
                ImportBillId = product.ibd.ImportBillId
            };
            var images = await _context.Images.Where(x => x.ProductId == id).Select(x => x.Url).ToListAsync();
            if (images.Count() > 0)
            {
                result.Images = images; 
            }
            return Ok(new ApiResponseSuccess<ProductViewModel>(result));
        }

        [HttpGet("Detail/{id}")]
        public async Task<IActionResult> GetDetailInformation(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new ApiResponseFailure<ProductViewModel>($"Không tìm thấy sản phẩm có mã {id}"));
            }
            var result = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                BrandName = (await _context.Brands.FindAsync(product.BrandId)).Name,
                CategoryName = (await _context.Categories.FindAsync(product.CategoryId)).Name,
                Description = product.Description,
                Introduce = product.Introduce,
                SalePrice = product.SalePrice,
                Origin = product.Origin,
                PurchaseCount = product.PurchaseCount,
                OfferPrice = product.OfferPrice,
                CategoryId = product.CategoryId
            };
            var images = await _context.Images.Where(x => x.ProductId == id).Select(x => x.Url).ToListAsync();
            if (images.Count() > 0)
            {
                result.Images = images;
            }
            return Ok(new ApiResponseSuccess<ProductViewModel>(result));
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
