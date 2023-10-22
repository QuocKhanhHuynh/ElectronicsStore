using ElectronicsStore.BackendApi.Constants;
using ElectronicsStore.BackendApi.Data;
using ElectronicsStore.BackendApi.Data.Entities;
using ElectronicsStore.BackendApi.Extendsions;
using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Categories;
using ElectronicsStore.Models.Commons;
using ElectronicsStore.Models.ImportBills;
using ElectronicsStore.Models.SaleBills;
using ElectronicsStore.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ElectronicsStore.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager,IConfiguration configuration, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> RigerterAccount([FromBody] AccountRegisterRequest request)
        {
            var userByName = await _userManager.FindByNameAsync(request.UserName);
            if (userByName != null)
            {
                return BadRequest(new ApiResponseFailure<bool>($"UserName {request.UserName} đã tồn tại"));
            }
            var userByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userByEmail != null)
            {
                return BadRequest(new ApiResponseFailure<bool>($"Email {request.Email} đã tồn tại"));
            }
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                FullName = request.FullName,
                DateCreate = DateTime.Now,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            var roleName = (await _roleManager.FindByIdAsync(request.RoleId)).Name;
            await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>($"Không thể tạo tài khoản"));
            }

        }

        [HttpPost("Login/Client")]
        public async Task<IActionResult> LoginUserAccount([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return NotFound(new ApiResponseFailure<string>($"Không thể tìm thấy tài khoản {request.UserName}"));
            }
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.Remember, false);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponseFailure<string>($"Mật khẩu đăng nhập sai"));
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,string.Join(";", roles))
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    _configuration["Tokens:Issuer"],
                    _configuration["Tokens:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: creds
                );
            return Ok(new ApiResponseSuccess<string>(new JwtSecurityTokenHandler().WriteToken(token)));
        }

        [HttpPost("Login/Admin")]
        public async Task<IActionResult> LoginAdminAccount([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return NotFound(new ApiResponseFailure<string>($"Không thể tìm thấy tài khoản {request.UserName}"));
            }
            if (!(await _userManager.GetRolesAsync(user)).Contains(Constant.ADMIN))
            {
                return BadRequest(new ApiResponseFailure<string>($"Không có quyền truy cập"));
            }
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.Remember, false);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponseFailure<string>($"Mật khẩu đăng nhập sai"));
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,string.Join(";", roles))
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    _configuration["Tokens:Issuer"],
                    _configuration["Tokens:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: creds
                );
            return Ok(new ApiResponseSuccess<string>(new JwtSecurityTokenHandler().WriteToken(token)));
        }
        
        [HttpPut("{id}")]
        [Authorize(Policy = "auth2")]
        public async Task<IActionResult> UpdateAccount(string id, [FromBody] UserUpdateRequest request)
        {
            if (!User.GetUserId().Equals(id))
            {
                return BadRequest(new ApiResponseFailure<bool>($"Bạn không có quyền cập nhật tài khoản này"));
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new ApiResponseFailure<bool>($"Không thể tìm thấy tài khoản {id}"));
            }
            var userByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userByEmail != null && !userByEmail.Id.Equals(id))
            {
                return BadRequest(new ApiResponseFailure<bool>($"Email {request.Email} đã tồn tại"));
            }
            user.FullName = request.FullName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.Address = request.Address;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>($"Không thể cập nhật tài khoản {id}"));
            }
        }

        [HttpPut("Password/Forget")]
        public async Task<IActionResult> ForgetPassword([FromBody] PasswordForgetRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return NotFound(new ApiResponseFailure<bool>($"Không thể tìm thấy tài khoản {request.UserName}"));
            }
            if (!user.Email.Equals(request.Email))
            {
                return BadRequest(new ApiResponseFailure<bool>($"Thông tin cung cấp sai"));
            }
            if (!request.NewPassword.Equals(request.ConfirmNewPassword))
            {
                return BadRequest(new ApiResponseFailure<bool>($"Mật khẩu xác nhận không khớp"));
            }
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.NewPassword);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>("Mật khẩu có chứa ít nhất tám ký tự, trong đó có ít nhất một số và bao gồm cả chữ thường và chữ hoa và ký tự đặc biệt"));
            }
        }

        [HttpPut("{id}/Password/Update")]
        [Authorize(Policy = "auth2")]
        public async Task<IActionResult> UpdatePassword(string id, [FromBody] PasswordUpdateRequest request)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new ApiResponseFailure<bool>($"Không thể tìm thấy tài khoản {id}"));
            }
            var resultCheckPassword = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
            if (!resultCheckPassword)
            {
                return BadRequest(new ApiResponseFailure<bool>($"Mật khẩu hiện tại nhập vào không khớp với mật khẩu tài khoản"));
            }
            if (!request.NewPassword.Equals(request.ConfirmNewPassword))
            {
                return BadRequest(new ApiResponseFailure<bool>($"Mật khẩu xác nhận không khớp"));
            }
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (result.Succeeded)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>($"Không thể cập nhật mật khẩu cho tài khoản {id}"));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "auth2")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new ApiResponseFailure<bool>($"Không thể tìm thấy tài khoản {id}"));
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(new ApiResponseSuccess<bool>());
            }
            else
            {
                return BadRequest(new ApiResponseFailure<bool>($"Không thể xóa tài khoản {id}"));
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "auth2")]
        public async Task<IActionResult> GetUserDetail(string id)
        {
            var query = await _userManager.FindByIdAsync(id);
            if (query == null)
            {
                return BadRequest(new ApiResponseFailure<UserViewModel>($"Không thể tìm thấy tài khoản {id}"));
            }
            var user = new UserViewModel()
            {
                Id = query.Id,
                UserName = query.UserName,
                FullName = query.FullName,
                RoleName = (await _userManager.GetRolesAsync(query))[0],
                DateCreate = query.DateCreate,
                Email = query.Email,
                PhoneNumber = query.PhoneNumber,
                Address = query.Address
            };
            return Ok(new ApiResponseSuccess<UserViewModel>(user));
        }

        [HttpGet]
        [Authorize(Policy = "auth1")]
        public async Task<IActionResult> GetUsersPagination(int pageIndex, int pageSize, string? userName, string? roleId)
        {
            var query = await _userManager.Users.ToListAsync();
            if(userName != null)
            {
                query = query.Where(x => x.UserName.Contains(userName)).ToList();
            }
            var tempQuery = new List<User>();
            if(roleId != null)
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                foreach(var user in query)
                {
                    if((await _userManager.GetRolesAsync(user)).Contains(role.Name))
                    {
                        tempQuery.Add(user);
                    }
                }
                query = tempQuery;
            }
            var totalItiem = query.Count();
            var items = new List<UserQuickViewModel>();
            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            foreach (var u in query)
            {
                var user = new UserQuickViewModel();
                user.Id = u.Id;
                user.UserName = u.UserName;
                user.DateCreate = u.DateCreate;
                user.RoleName = (await _userManager.GetRolesAsync(u))[0];
                items.Add(user);
            }
            var pagination = new Pagination<UserQuickViewModel>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items,
                TotalRecords = totalItiem
            };
            return Ok(new ApiResponseSuccess<Pagination<UserQuickViewModel>>(pagination));
        }

        [HttpGet("{id}/SaleBill")]
        [Authorize(Policy = "auth2")]
        public async Task<IActionResult> GetSaleBills(int pageIndex, int pageSize)
        {
            var query = from b in _context.SaleBills
                        join s in _context.Statuses on b.StatusId equals s.Id
                        where b.UserId.Equals(User.GetUserId())
                        orderby b.DateCreate descending
                        select new { b, s };
            var total = query.Count();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(x => new SaleBillViewModel()
            {
                DateCreate = x.b.DateCreate,
                Id = x.b.Id,
                StatusId = x.s.Id,
                StatusName = x.s.Name,
                UserId = x.b.UserId
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
        [HttpGet("admins")]
        [Authorize(Policy = "auth1")]
        public async Task<IActionResult> GetAdmins()
        {
            var admins = (await _userManager.GetUsersInRoleAsync(Constant.ADMIN)).Select(x => new UserQuickViewModel()
            {
                Id = x.Id,
                DateCreate= x.DateCreate,
                RoleName = Constant.ADMIN,
                UserName = x.UserName
            }).ToList();
            return Ok(new ApiResponseSuccess<List<UserQuickViewModel>>(admins));
        }

        [HttpGet("Statistics")]
        [Authorize(Policy = "auth1")]
        public async Task<IActionResult> GetStatistics(int pageIndex, int pageSize, int? timeLine)
        {
            var Start = DateTime.Now.AddDays(timeLine.GetValueOrDefault(0) * (-1));
            var End = DateTime.Now;
            var query = await _userManager.Users.Where(x => x.DateCreate >= Start && x.DateCreate <= End).ToListAsync();
            var totalItiem = query.Count();
            var items = new List<UserQuickViewModel>();
            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            foreach (var u in query)
            {
                var user = new UserQuickViewModel();
                user.Id = u.Id;
                user.UserName = u.UserName;
                user.DateCreate = u.DateCreate;
                user.RoleName = (await _userManager.GetRolesAsync(u))[0];
                items.Add(user);
            }
            var pagination = new Pagination<UserQuickViewModel>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items,
                TotalRecords = totalItiem
            };
            return Ok(new ApiResponseSuccess<Pagination<UserQuickViewModel>>(pagination));
        }
    }
}
