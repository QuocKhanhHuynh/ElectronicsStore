using ElectronicsStore.Models.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ElectronicsStore.ApiServices;
using Azure.Core;
using ElectronicsStore.BackendApi.Extendsions;
using ElectronicsStore.AdminApp.Models.Account;
using Microsoft.AspNetCore.Mvc.Rendering;
using ElectronicsStore.AdminApp.Models.Product;
using Microsoft.AspNetCore.Authorization;
using ElectronicsStore.Models.Statuses;
using ElectronicsStore.Models.Roles;

namespace ElectronicsStore.AdminApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserApiService _userApiService;
        private readonly IRoleApiService _roleApiService;
        private readonly IConfiguration _configuration;
        public AccountController(IUserApiService userApiService, IRoleApiService roleApiService, IConfiguration configuration)
        {
            _userApiService = userApiService;
            _roleApiService = roleApiService;
            _configuration = configuration;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (TempData["Success"] != null)
            {
                ViewBag.Success = TempData["Success"];
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var token = await _userApiService.LoginAdminAccount(request);
            if (!token.Status)
            {
                ModelState.AddModelError("", token.Message);
                return View(request);
            }
            var userPrincial = this.ValidateToken(token.ObjectResult);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
                IsPersistent = false
            };
            HttpContext.Session.SetString("Token", token.ObjectResult);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincial, authProperties);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPassword(PasswordForgetRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _userApiService.ForgetPassword(request);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Thay đổi mật khẩu thành công";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(PasswordUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var userId = User.GetUserId();
            var result = await _userApiService.UpdatePassword(userId, request);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Thay đổi mật khẩu thành công";
            return RedirectToAction("GetMyDetail");
        }

        [HttpGet]
        public async Task<IActionResult> GetMyDetail()
        {
            if (TempData["Success"] != null)
            {
                ViewBag.Success = TempData["Success"];
            }
            var userId = User.GetUserId();
            var result = await _userApiService.GetUserDetail(userId);
            return View(result.ObjectResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserDetail(string id)
        {
            var result = await _userApiService.GetUserDetail(id);
            return View(result.ObjectResult);
        }

        [HttpGet]
        public IActionResult CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin(AccountRegisterRequest request)
        {

            if (!ModelState.IsValid)
                return View(request);
            var result = await _userApiService.RigerterAccount(request);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Thêm mới tài khoản admin thành công";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAccount()
        {
            var userId = User.GetUserId();
            var user = await _userApiService.GetUserDetail(userId);
            var request = new UserUpdateRequest()
            {
                Email = user.ObjectResult.Email,
                FullName = user.ObjectResult.FullName,
                PhoneNumber = user.ObjectResult.PhoneNumber,
                Address = user.ObjectResult.Address
            };
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAccount(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var userId = User.GetUserId();
            var result = await _userApiService.UpdateAccount(userId,request);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Cập nhật tài khoản thành công";
            return RedirectToAction("GetMyDetail");
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? userName, string? roleId, int pageIndex = 1, int pageSize = 10 )
        {
            if (TempData["Success"] != null)
            {
                ViewBag.Success = TempData["Success"];
            }
            var users = await _userApiService.GetUsers(pageIndex, pageSize, userName, roleId);
            var rolesList = await _roleApiService.GetRoles();
            var roles = rolesList.ObjectResult.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id,
                Selected = roleId != null && x.Id == roleId
            });
            var model = new UsersViewModel()
            {
                Users = users.ObjectResult,
                Roles = roles,
                UserName = userName
            };
            return View(model);
        }

        public async Task<IActionResult> GetRoles()
        {
            if (TempData["Success"] != null)
            {
                ViewBag.Success = TempData["Success"];
            }
            var statuses = await _roleApiService.GetRoles();
            return View(statuses.ObjectResult);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleCreateRequest request)
        {

            if (!ModelState.IsValid)
                return View(request);
            var result = await _roleApiService.CreateRole(request);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Thêm mới vai trò thành công";
            return RedirectToAction("GetRoles");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRole(string id)
        {
            var status = await _roleApiService.GetRoelDetail(id);
            var request = new RoleUpdateRequest()
            {
                Id = status.ObjectResult.Id,
                Name = status.ObjectResult.Name
            };
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _roleApiService.UpdateRole(request.Id, request);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Cập nhật Vai trò thành công";
            return RedirectToAction("GetRoles");
        }

        [HttpGet]
        public IActionResult DeleteRole(string id)
        {
            var request = new RoleDeleteRequest()
            {
                Id = id
            };
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(RoleDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _roleApiService.DeleteRole(request.Id);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Success"] = "Xóa vai trò thành công";
            return RedirectToAction("GetRoles");
        }
        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;
            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = _configuration["Tokens:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Tokens:Issuer"],
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"])),
            };
            return new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);
        }
    }
}
