using ElectronicsStore.ApiServices;
using ElectronicsStore.Models.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ElectronicsStore.BackendApi.Extendsions;
using Microsoft.AspNetCore.Authorization;

namespace ElectronicsStore.ClientApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserApiService _userApiService;
        private readonly IConfiguration _configuration;
        public AccountController(IUserApiService userApiService, IConfiguration configuration)
        {
            _userApiService = userApiService;
            _configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (TempData["Notify"] != null)
            {
                ViewBag.Notify = TempData["Notify"];
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var token = await _userApiService.LoginUserAccount(request);
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
            return RedirectToAction("Index","Home");
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
            TempData["Notify"] = "Thay đổi mật khẩu thành công";
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
            TempData["Notify"] = "Thay đổi mật khẩu thành công";
            return RedirectToAction("GetMyDetail");
        }

        [HttpGet]
        public async Task<IActionResult> GetMyDetail()
        {
            if (TempData["Notify"] != null)
            {
                ViewBag.Notify = TempData["Notify"];
            }
            var userId = User.GetUserId();
            var result = await _userApiService.GetUserDetail(userId);
            return View(result.ObjectResult);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterAccount()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAccount(AccountRegisterRequest request)
        {

            if (!ModelState.IsValid)
                return View(request);
            var result = await _userApiService.RigerterAccount(request);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            var loginRequest = new LoginRequest()
            {
                UserName = request.UserName,
                Password = request.Password
            };
            var token = await _userApiService.LoginUserAccount(loginRequest);
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
        public async Task<IActionResult> UpdateAccount()
        {
            var userId = User.GetUserId();
            var user = await _userApiService.GetUserDetail(userId);
            var request = new UserUpdateRequest()
            {
                Email = user.ObjectResult.Email,
                FullName = user.ObjectResult.FullName,
                PhoneNumber = user.ObjectResult.PhoneNumber
            };
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAccount(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var userId = User.GetUserId();
            var result = await _userApiService.UpdateAccount(userId, request);
            if (!result.Status)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            TempData["Notify"] = "Cập nhật tài khoản thành công";
            return RedirectToAction("GetMyDetail");
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
