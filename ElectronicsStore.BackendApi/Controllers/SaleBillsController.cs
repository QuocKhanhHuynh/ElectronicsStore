using ElectronicsStore.BackendApi.Data;
using ElectronicsStore.BackendApi.Data.Entities;
using ElectronicsStore.BackendApi.Extendsions;
using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Commons;
using ElectronicsStore.Models.ImportBills;
using ElectronicsStore.Models.SaleBills;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleBillsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SaleBillsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Authorize(Policy = "auth2")]
        public async Task<IActionResult> CreateSaleBill([FromBody] SaleBillCreateRequest request)
        {
            var saleBill = new SaleBill()
            {
                Address = request.Address,
                DateCreate = DateTime.Now,
                PhoneNumber = request.PhoneNumber,
                RecipientName = request.RecipientName,
                StatusId = 1,
                UserId = User.GetUserId()
            };
            int total = 0;
            saleBill.SaleBillDetails = new List<SaleBillDetail>();
            foreach(var item in request.DetailBill)
            {
                saleBill.SaleBillDetails.Add(new SaleBillDetail()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    SalePrice = item.SalePrice,
                    ImportBillId = item.ImportBillId
                });
                var product = await _context.Products.FindAsync(item.ProductId);
                var importBill = await _context.ImportBillDetails.FindAsync(item.ImportBillId,item.ProductId);
                product.PurchaseCount += item.Quantity;
                _context.Products.Update(product);
                importBill.Inventory -= item.Quantity;
                _context.ImportBillDetails.Update(importBill);
                total += item.Quantity * item.SalePrice;
            }
            saleBill.ToTalPayment = total;
            await _context.SaleBills.AddAsync(saleBill);
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
        [Authorize(Policy = "auth2")]
        public async Task<IActionResult> GetSaleBills(int pageIndex, int pageSize,string? userId, int? statusId, int? id )
        {
            var query = from b in _context.SaleBills
                        join s in _context.Statuses on b.StatusId equals s.Id
                        orderby b.DateCreate descending
                        select new { b, s};
            if(userId != null)
            {
                query = query.Where(x => x.b.UserId.Equals(userId));
            }
            if (statusId.HasValue)
            {
                query = query.Where(x => x.b.StatusId == statusId);
            }
            if (id.HasValue)
            {
                query = query.Where(x => x.b.Id == id);
            }
            var total = query.Count();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(x => new SaleBillViewModel()
            {
                DateCreate = x.b.DateCreate,
                Id = x.b.Id,
                StatusId = x.s.Id,
                StatusName = x.s.Name,
                UserId = x.b.UserId,
                TotalValue = x.b.ToTalPayment
            }).ToListAsync();
            var pagination = new Pagination<SaleBillViewModel>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items,
                TotalRecords = total
            };
            return Ok(new ApiResponseSuccess<Pagination<SaleBillViewModel>>(pagination));
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "auth2")]
        public async Task<IActionResult> GetSaleBillDetail(int id)
        {
            var query = from b in _context.SaleBillDetails
                        join p in _context.Products on b.ProductId equals p.Id
                        join i in _context.Images on p.Id equals i.ProductId
                        where b.SaleBillId == id && i.IsDefaul == true
                        select new { b, p, i };
            var listItems = await query.Select(x => new SaleBillDetailViewModel()
            {
                ProductId = x.p.Id,
                ProductName = x.p.Name,
                Image = x.i.Url,
                Quanlity = x.b.Quantity,
                SalePrice = x.b.SalePrice
            }).ToListAsync();
            var bill = _context.SaleBills.Find(id);
            var result = new SaleBillInformation()
            {
                Id = bill.Id,
                DateCreate = bill.DateCreate,
                TotalValue = bill.ToTalPayment,
                Address = bill.Address,
                PhoneNumber = bill.PhoneNumber,
                RecipientName = bill.RecipientName,
                Details = listItems
            };
            return Ok(new ApiResponseSuccess<SaleBillInformation>(result));
        }

        [HttpPut]
        [Authorize(Policy = "auth1")]
        public async Task<IActionResult> UpdateStatus([FromBody] BillStatusUpdateRequest request)
        {
            var statusCount = _context.Statuses.Count();
            if (request.StatusId > statusCount)
            {
                return BadRequest(new ApiResponseFailure<bool>($"Không thể cập nhật trạng thái vì đã đạt tối đa"));
            }
            var saleBill = await _context.SaleBills.FindAsync(request.BillId);
            if (saleBill == null)
            {
                return BadRequest(new ApiResponseFailure<bool>($"Không tìm thấy hóa đơn có mã {request.BillId}"));
            }
            saleBill.StatusId = request.StatusId;
            _context.SaleBills.Update(saleBill);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>("Không thể cập nhật trạng thái hóa đơn"));
            }
        }

        [HttpGet("Inventory/{productId}")]
        [Authorize(Policy = "auth2")]
        public async Task<IActionResult> CheckInventory(int productId)
        {

            var product = await _context.ImportBillDetails.Where(x => x.ProductId == productId && x.Inventory > 0).ToListAsync();
            var quanlity = product.Sum(x => x.Inventory);
            return Ok(new ApiResponseSuccess<int>(quanlity));
        }

        [HttpGet("Statistics")]
        [Authorize(Policy = "auth1")]
        public async Task<IActionResult> Statistics(int pageIndex, int pageSize, int? timeLine)
        {
            var Start = DateTime.Now.AddDays(timeLine.GetValueOrDefault(0) * (-1));
            var End =DateTime.Now;
            var query = from sbd in _context.SaleBillDetails
                        join p in _context.Products on sbd.ProductId equals p.Id
                        join sb in _context.SaleBills on sbd.SaleBillId equals sb.Id
                        join i in _context.Images on p.Id equals i.ProductId
                        where sb.DateCreate >= Start && sb.DateCreate <= End && i.IsDefaul == true && sb.StatusId == _context.Statuses.Count() 
                        select new { sbd, p, sb, i};
            var pagination = new Pagination<RevenueStatisticsViewModel>();
            pagination.TotalRecords = query.Count();
            var datas = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(x => new RevenueStatisticsViewModel()
            {
                ProductName = x.p.Name,
                Image = x.i.Url,
                ImporPrice = (_context.ImportBillDetails.Single(y => y.ImportBillId == x.sbd.ImportBillId && y.ProductId == x.sbd.ProductId)).ImportPrice,
                SalePrice = x.sbd.SalePrice,
                DateSale = x.sb.DateCreate
            }).ToListAsync();
            pagination.PageIndex = pageIndex;
            pagination.PageSize = pageSize;
            pagination.Items = datas;
            return Ok(new ApiResponseSuccess<Pagination<RevenueStatisticsViewModel>>(pagination));
        }
    }
}
