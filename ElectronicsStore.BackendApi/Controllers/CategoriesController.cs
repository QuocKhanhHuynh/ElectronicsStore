using ElectronicsStore.BackendApi.Data.Entities;
using ElectronicsStore.BackendApi.Data;
using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectronicsStore.Models.Brands;
using ElectronicsStore.Models.Commons;
using Microsoft.AspNetCore.Authorization;

namespace ElectronicsStore.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "auth1")]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateRequest request)
        {
            var category = new Category()
            {
                Name = request.Name
            };
            await _context.Categories.AddAsync(category);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>("Không thể thêm loại hàng"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateRequest request)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound(new ApiResponseFailure<bool>($"Không thể tìm thấy loại hàng có mã {id}"));
            }
            category.Name = request.Name;
            _context.Categories.Update(category);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>("Không thể cập nhật loại hàng"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound(new ApiResponseFailure<bool>($"Không thể tìm thấy loại hàng có mã {id}"));
            }
            _context.Categories.Remove(category);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>("Không thể xoá loại hàng"));
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories.Select(x => new CategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
            return Ok(new ApiResponseSuccess<List<CategoryViewModel>>(categories));
        }

        [HttpGet("Pagination")]
        public async Task<IActionResult> GetCategoriesPagination(int pageIndex, int pageSize)
        {
            var categories = _context.Categories;
            var totalItiem = categories.Count();
            var items = await categories.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(x => new CategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
            var pagination = new Pagination<CategoryViewModel>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items,
                TotalRecords = totalItiem
            };
            return Ok(new ApiResponseSuccess<Pagination<CategoryViewModel>>(pagination));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryDetail(int id)
        {
            var query = await _context.Categories.FindAsync(id);
            if (query == null)
            {
                return NotFound(new ApiResponseFailure<bool>($"Không thể tìm thấy loại hàng có mã {id}"));
            }
            var category = new CategoryViewModel()
            {
                Id = query.Id,
                Name = query.Name
            };
            return Ok(new ApiResponseSuccess<CategoryViewModel>(category));
        }
    }
}
