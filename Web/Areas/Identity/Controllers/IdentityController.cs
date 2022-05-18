using Application.Models;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Areas.Identity.ViewModels;
using Domain.Exceptions;
using static Infrastructure.IdentityConstants.IdentityRoles;

namespace Web.Areas.Identity.Controllers
{
    [Area(WebConstraints.Areas.Identity)]
    public class IdentityController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailService emailService;
        private readonly IFarmerDbContext farmerDbContext;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public IdentityController(
            UserManager<User> userManager,
            IEmailService emailService,
            IFarmerDbContext farmerDbContext,
            IUserService userService,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.farmerDbContext = farmerDbContext;
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {

            var user = await userManager.FindByNameAsync(loginViewModel.Email);

            if (!user.Active)
            {
                throw new ApplicationException($"User with email: {user.UserName}, don't active");
            }

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

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme,Roles = AdminRole)]
        public async Task<IActionResult> ListUsers()
        {
            var users = await userService.ListUsers();

            return View(mapper.Map<List<ListUserViewModel>>(users));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = AdminRole)]
        public IActionResult AddUser() => View();

        [HttpPost]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = AdminRole)]
        public async Task<IActionResult> AddUser(AddUserViewModel addUser)
        {
            if (!ModelState.IsValid)
            {
                return View(addUser);
            }
            var user = new User(addUser.Email);

            var userResult = await userManager.CreateAsync(user, addUser.Password);

            if (!userResult.Succeeded)
            {
                throw new DomainException($"User with email: {user.UserName}, already exists");
            }

            user.UpdateActive(true);

            var userRoleResult = await userManager.AddToRoleAsync(user, "User");

            if (!userRoleResult.Succeeded)
            {
                return BadRequest(userRoleResult.Errors);
            }

            await emailService.SendUserCreatedEmail(addUser.Email, addUser.Password);

            return RedirectToAction(nameof(ListUsers));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = AdminRole)]
        public async Task<IActionResult> EditUser(string id)
        {
            var result = await userService.GetUser(id);

            return View(mapper.Map<EditUserViewModel>(result));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = AdminRole)]
        public async Task<IActionResult> EditUser(EditUserViewModel editUser)
        {
            if (editUser == null)
            {
                return BadRequest();
            }

            await userService.EditUser(editUser.Id, editUser.Active);

            return RedirectToAction(nameof(ListUsers));
        }

    }
}
