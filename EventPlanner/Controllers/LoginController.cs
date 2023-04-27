using EventPlannerModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace EventPlanner.Controllers
{
    public class LoginController : Controller
    {
        
        // GET: Login/Login
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Password")] LoginUser user)
        {
            Token ResultToken = await Functions.APIServices.UsersLogin(user);
            if (ResultToken.generalResult.Result)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
                claims.Add(new Claim("token", ResultToken.token));
                claims.Add(new Claim(ClaimTypes.Role, ResultToken.roleId.ToString()));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return RedirectToAction("Index", "Home");
            }
            TempData["errorMsg"] = ResultToken.generalResult.ErrorMessage;
            return View(user);
        }
        // GET: Login/Register
        public async Task<IActionResult> Register()
        {
            return View();
        }

        // POST: Login/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("FirstName,LastName,CompanyName,MailVisible,Email,PhoneVisible,ContactPhone,IsCompany,Password")] User user)
        {
            Token token = await Functions.APIServices.UsersCreate(user);
            if (token.generalResult.Result)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
                claims.Add(new Claim("token", token.token));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return RedirectToAction("Index", "Users");
            }
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
