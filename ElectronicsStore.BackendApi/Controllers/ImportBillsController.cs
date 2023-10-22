using ElectronicsStore.BackendApi.Data;
using ElectronicsStore.BackendApi.Data.Entities;
using ElectronicsStore.BackendApi.Extendsions;
using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Commons;
using ElectronicsStore.Models.ImportBills;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "auth1")]
    public class ImportBillsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ImportBillsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateImportBill([FromBody] ImportBillCreateRequest request)
        {
            var importBill = new ImportBill()
            {
                DateCreate = DateTime.Now,
                SupplierId = request.SupplierId.GetValueOrDefault(),
                UserId = User.GetUserId()
            };
            int totalValue = 0;
            importBill.ImportBillDetails = new List<ImportBillDetail>();
            foreach (var item in request.DetailBill)
            {
                importBill.ImportBillDetails.Add(new ImportBillDetail()
                {
                    ProductId = item.ProductId,
                    ImportPrice = item.ImportPrice,
                    Quantity = item.Quantity,
                    Inventory = item.Quantity
                    ///
                });
                totalValue += item.ImportPrice * item.Quantity;
            }
            importBill.TotalValue = totalValue;
            await _context.ImportBills.AddAsync(importBill);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>("Không thể tạo hóa đơn"));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetImportBills(int pageIndex, int pageSize, int? id, string? userId)
        {
            var query = from i in _context.ImportBills
                        join s in _context.Suppliers on i.SupplierId equals s.Id
                        join u in _context.Users on i.UserId equals u.Id
                        orderby i.DateCreate descending
                        select new { i, s, u };
            if(id.HasValue)
            {
                query = query.Where(x => x.i.Id == id);
            }
            if (userId != null)
            {
                query = query.Where(x => x.u.Id.Equals(userId));
            }
            var total = query.Count();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(x => new ImportBillViewModel()
            {
                Datecreate = x.i.DateCreate,
                Id = x.i.Id,
                TotalValue = x.i.TotalValue,
                SupplierName = x.s.Name,
                UserName = x.u.UserName
            }).ToListAsync();
            var pagination = new Pagination<ImportBillViewModel>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items,
                TotalRecords = total
            };
            return Ok(new ApiResponseSuccess<Pagination<ImportBillViewModel>>(pagination));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImportBillDetail(int id)
        {
            var query = from b in _context.ImportBillDetails
                               join p in _context.Products on b.ProductId equals p.Id
                               join i in _context.Images on p.Id equals i.ProductId
                               where b.ImportBillId == id && i.IsDefaul == true
                               select new { b, p, i };
            var listItems = await query.Select(x => new ImportBillDetailViewModel()
            {
                ProductName = x.p.Name,
                ImportPrice = x.b.ImportPrice,
                Quanlity = x.b.Quantity,
                Image = x.i.Url
            }).ToListAsync();
            return Ok(new ApiResponseSuccess<List<ImportBillDetailViewModel>>(listItems));
        }
    }
}
