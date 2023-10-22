using ElectronicsStore.BackendApi.Data.Entities;
using ElectronicsStore.BackendApi.Data;
using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ElectronicsStore.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "auth1")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        public RolesController(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleCreateRequest request)
        {
            var role = new Role()
            {
                Id = request.Id.ToUpper(),
                Name = request.Name
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>("Không thể thêm vai trò"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(string id, [FromBody] RoleUpdateRequest request)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound(new ApiResponseFailure<bool>($"Không tìm thấy vai trò có mã {id}"));
            }
            role.Name = request.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>($"Không thể cập nhật vai trò có mã {id}"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound(new ApiResponseFailure<bool>($"Không tìm thấy vai trò có mã {id}"));
            }
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>($"Không thể xóa vai trò{id}"));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRoels()
        {
            var roles = await _roleManager.Roles.Select(x => new RoleViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return Ok(new ApiResponseSuccess<List<RoleViewModel>>(roles));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoelDetail(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return Ok(new ApiResponseSuccess<RoleViewModel>(new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            }));
        }
    }
}
