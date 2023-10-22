using ElectronicsStore.BackendApi.Data;
using ElectronicsStore.BackendApi.Data.Entities;
using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Categories;
using ElectronicsStore.Models.Statuses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "auth1")]
    public class StatusesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StatusesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] StatusCreateRequest request)
        {
            var statues = new Status()
            {
                Name = request.Name
            };
            await _context.Statuses.AddAsync(statues);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>("Không thể thêm trạng thái"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusUpdateRequest request)
        {
            var statues = await _context.Statuses.FindAsync(id);
            if (statues == null)
            {
                return NotFound(new ApiResponseFailure<bool>($"Không thể tìm thấy trạng thái có mã {id}"));
            }
            statues.Name = request.Name;
            _context.Statuses.Update(statues);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>("Không thể cập nhật trạng thái"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound(new ApiResponseFailure<bool>($"Không thể tìm thấy trạng thái có mã {id}"));
            }
            _context.Statuses.Remove(status);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>("Không thể xoá trạng thái"));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStatuses()
        {
            var statuses = await _context.Statuses.Select(x => new StatusViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
            return Ok(new ApiResponseSuccess<List<StatusViewModel>>(statuses));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatusDetail(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
            {
                return BadRequest(new ApiResponseFailure<StatusViewModel>($"Không thể tìm thấy trạng thái có mã {id}"));
            }
            return Ok(new ApiResponseSuccess<StatusViewModel>(new StatusViewModel()
            {
                Id = status.Id,
                Name = status.Name
            }));
        }
    }
}
