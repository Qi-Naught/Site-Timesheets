using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.Data;
using Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TPFeuilleDeTemps_JeanGirard.ApplicationLogic;
using TPFeuilleDeTemps_JeanGirard.Models.ViewModels;

namespace TPFeuilleDeTemps_JeanGirard.Controllers
{
    public class AccountController : Controller
    {
        private readonly TimeSheetContext context;

        public AccountController(TimeSheetContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            LoginViewModel loginViewModel = new();

            if (Request.Cookies["Email"] != null)
            {
                loginViewModel.RememberMe = true;
                loginViewModel.Email = Request.Cookies["Email"];
            }

            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidateLogin(LoginViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (loginViewModel.RememberMe)
                {
                    CookieOptions options = new()
                    {
                        Expires = DateTime.Now.AddDays(30)
                    };
                    Response.Cookies.Append("Email", loginViewModel.Email, options);
                }
                else
                {
                    Response.Cookies.Delete("Email");
                }

                string pwd = PwdHasherAndSalter.ComputeSha256SaltedHash(loginViewModel.Email, loginViewModel.Pwd);
                User user = context.Users.FirstOrDefault(user => user.Email == loginViewModel.Email);
                if (user != null && pwd == user.Pwd)
                {
                    List<Claim> claims = new()
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.FullName),
                        new Claim("Id", user.Id.ToString())
                    };
                    ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new(claimsIdentity);
                    AuthenticationProperties authProperties = new()
                    {
                        ExpiresUtc = DateTime.Now.AddMinutes(10)
                    };
                    await HttpContext.SignInAsync(claimsPrincipal, authProperties);

                    if (returnUrl != null) return Redirect(returnUrl);

                    return Redirect("/");
                }

                TempData["UserMessage"] = new ValidationMessageForView
                    { CssClassName = "alert-danger", Title = "Login Failed!", Message = "Invalid credentials!" };
            }

            return View("Login", loginViewModel);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        public IActionResult Create()
        {
            UserCreationViewModel model = new();

            UserCreationViewModelProcessing processing = new(context);
            processing.LoadModel(model);

            return View(model);
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(context.Users.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreationViewModel userCreationViewModel)
        {
            if (context.Users.FirstOrDefault(x => x.Email == userCreationViewModel.Email) != null)
                ModelState.AddModelError(string.Empty, "This email is already in use.");
            if (ModelState.IsValid)
            {
                UserCreationViewModelProcessing processing = new(context);
                await processing.SaveModel(userCreationViewModel);
                userCreationViewModel = new UserCreationViewModel();
                processing.LoadModel(userCreationViewModel);
                TempData["UserMessage"] = new ValidationMessageForView
                    { Message = "The user was created successfully!" };
                return View("Create", userCreationViewModel);
            }

            //throw new Exception();
            TempData["UserMessage"] = new ValidationMessageForView
                { CssClassName = "alert-danger", Title = "Failure!", Message = "The user was not created!" };
            return View(userCreationViewModel);
        }
    }
}