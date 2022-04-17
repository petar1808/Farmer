using Application.Services;
using Infrastructure.DbContect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web.Areas.Identity.ViewModels;

namespace Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class IdentityController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        // user service

        public IdentityController(
            UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login() => View();


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var user = await userManager.FindByNameAsync(loginViewModel.Email);

            if (user == null)
            {
                return BadRequest();
            }

            if (!await userManager.CheckPasswordAsync(user, loginViewModel.Password))
            {
                return BadRequest();
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

            var userRole = await userManager.GetRolesAsync(user);

            identity.AddClaim(new Claim(ClaimTypes.Name, loginViewModel.Email));
            identity.AddClaim(new Claim(ClaimTypes.Role, userRole.First()));

            var principal = new ClaimsPrincipal(identity);

            await this.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            return RedirectToAction("all", "workingSeasons", new { area = "" });
        }

        //[HttpGet]
        //public async Task<IActionResult> ListUsers()
        //{
        //   var listUser = await farmerDbContext
        //        .Users
        //        .Select(x => new ListUserViewModel
        //        {
        //            Email = x.UserName
        //        })
        //        .ToListAsync();

        //    return View(listUser);
        //}

    }
}
