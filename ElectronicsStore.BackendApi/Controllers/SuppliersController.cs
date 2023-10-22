using ElectronicsStore.BackendApi.Data.Entities;
using ElectronicsStore.BackendApi.Data;
using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Categories;
using ElectronicsStore.Models.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ElectronicsStore.Models.Suppliers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ElectronicsStore.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "auth1")]
    public class SuppliersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SuppliersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] SupplierCreateRequest request)
        {
            var supplier = new Supplier()
            {
                Name = request.Name,
                Address = request.Address,
                BankName = request.BankName,
                BankNumber = request.BankNumber,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };
            await _context.Suppliers.AddAsync(supplier);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>("Không thể thêm nhà cung cấp"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] SupplierUpdateRequest request)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound(new ApiResponseFailure<bool>($"Không thể tìm thấy nhà cung cấp có mã {id}"));
            }
            supplier.Name = request.Name;
            supplier.Address = request.Address;
            supplier.Email = request.Email;
            supplier.PhoneNumber = request.PhoneNumber;
            supplier.BankName = request.BankName;
            supplier.BankNumber = request.BankNumber;
            _context.Suppliers.Update(supplier);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>("Không thể cập nhật nhà cung cấp"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound(new ApiResponseFailure<bool>($"Không thể tìm thấy nhà cung cấp có mã {id}"));
            }
            _context.Suppliers.Remove(supplier);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>("Không thể xoá nhà cung cấp"));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSuppliers()
        {
            var suppliers = await _context.Suppliers.Select(x => new SupplierQuickViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return Ok(new ApiResponseSuccess<List<SupplierQuickViewModel>>(suppliers));
        }

        [HttpGet("Pagination")]
        public async Task<IActionResult> GetSupplierssPagination(int pageIndex, int pageSize)
        {
            var suppliers = _context.Suppliers;
            var totalItiem = suppliers.Count();
            var items = await suppliers.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(x => new SupplierQuickViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            var pagination = new Pagination<SupplierQuickViewModel>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items,
                TotalRecords = totalItiem
            };
            return Ok(new ApiResponseSuccess<Pagination<SupplierQuickViewModel>>(pagination));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var suppliers = await _context.Suppliers.FindAsync(id);
            var result = new SupplierViewModel()
            {
                Id = suppliers.Id,
                Name = suppliers.Name,
                Email = suppliers.Email,
                Address = suppliers.Address,
                BankName = suppliers.BankName,
                BankNumber = suppliers.BankNumber,
                PhoneNumber = suppliers.PhoneNumber
            };
            return Ok(new ApiResponseSuccess<SupplierViewModel>(result));
        }
    }
}
