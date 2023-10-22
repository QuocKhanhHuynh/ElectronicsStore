using ElectronicsStore.BackendApi.Data.Entities;
using ElectronicsStore.BackendApi.Data;
using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Brands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectronicsStore.BackendApi.Services;
using System.Net.Http.Headers;
using ElectronicsStore.Models.Commons;
using Microsoft.AspNetCore.Authorization;

namespace ElectronicsStore.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "auth1")]
    public class BrandsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStorageService _storageService;
        public BrandsController(ApplicationDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateBrand([FromForm] BrandCreateRequest request)
        {
            var brand = new Brand()
            {
                Name = request.Name,
                Logo = await this.SaveFile(request.Logo)
            };
            await _context.Brands.AddAsync(brand);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>("Không thể thêm thương hiệu"));
            }
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateBrand(int id, [FromForm] BrandUpdateRequest request)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound(new ApiResponseFailure<bool>($"Không thể tìm thấy thương hiệu có mã {id}"));
            }
            brand.Name = request.Name;
            if (request.Logo != null)
            {
                await _storageService.DeleteFileAsync(brand.Logo);
                brand.Logo = await this.SaveFile(request.Logo);
            }
            _context.Brands.Update(brand);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>("Không thể cập nhật thương hiệu"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound(new ApiResponseFailure<bool>($"Không thể tìm thấy thương hiệu có mã {id}"));
            }
            await _storageService.DeleteFileAsync(brand.Logo);
            _context.Brands.Remove(brand);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>("Không xoá thương hiệu"));
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _context.Brands.Select(x => new BrandQuickViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Logo = x.Logo
            }).ToListAsync();
            return Ok(new ApiResponseSuccess<List<BrandQuickViewModel>>(brands));
        }

        [HttpGet("Pagination")]
        public async Task<IActionResult> GetBrandsPagination(int pageIndex, int pageSize)
        {
            var brands = _context.Brands;
            var totalItiem = brands.Count();
            var items = await brands.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(x => new BrandViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Logo = x.Logo,
            }).ToListAsync();
            var pagination = new Pagination<BrandViewModel>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items,
                TotalRecords = totalItiem
            };
            return Ok(new ApiResponseSuccess<Pagination<BrandViewModel>>(pagination));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandDetail(int id)
        {
            var query = await _context.Brands.FindAsync(id);
            if (query == null)
            {
                return NotFound(new ApiResponseFailure<BrandViewModel>($"Không thể tìm thấy thương hiệu có mã {id}"));
            }
            var brand = new BrandViewModel()
            {
                Id = query.Id,
                Name = query.Name,
                Logo = query.Logo
            };
            return Ok(new ApiResponseSuccess<BrandViewModel>(brand));
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
