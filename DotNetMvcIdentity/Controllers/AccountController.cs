using DotNetMvcIdentity.Models;
using DotNetMvcIdentity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMvcIdentity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailSender emailSender)
        {
            _emailSender = emailSender;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            RegisterViewModel registerVM = new RegisterViewModel();//Create a new object view

            return View(registerVM);//Return the view to be showed in the browser
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel rgViewModel, string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new AppUser()
                {
                    UserName = rgViewModel.Email,
                    Email = rgViewModel.Email,
                    Name = rgViewModel.Name,
                    Url = rgViewModel.Url,
                    CountryCode = rgViewModel.CountryCode,
                    Phone = rgViewModel.Phone,
                    Country = rgViewModel.Country,
                    City = rgViewModel.City,
                    Address = rgViewModel.Address,
                    Birthdate = rgViewModel.Birthdate,
                    State = rgViewModel.State
                };

                var result = await _userManager.CreateAsync(user, rgViewModel.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    //return RedirectToAction("Index", "Home");//method, controller
                    //return Redirect(returnUrl); Avoid open redirect attacks
                    return LocalRedirect(returnUrl);
                }
                ValidateErrors(result);
            }

            return View(rgViewModel);
        }

        private void ValidateErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        //Show login form
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel lgViewModel, string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(lgViewModel.Email);
                var result = await _signInManager.PasswordSignInAsync(
                    lgViewModel.Email,
                    lgViewModel.Password,
                    lgViewModel.RememberMe,
                    lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    //return RedirectToAction("Index", "Home");//method, controller
                    return LocalRedirect(returnUrl);
                }
                if (await _userManager.CheckPasswordAsync(user, lgViewModel.Password))
                {
                    if (result.IsLockedOut)
                    {
                        //return RedirectToAction("Index", "Home");//method, controller
                        return View("Blocked");
                    }
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Invalid email or password");
                    return View(lgViewModel);
                }
            }

            return View(lgViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//avid xss atacks
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//avid xss atacks
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel fpViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(fpViewModel.Email);
                if (user == null) {
                    return RedirectToAction("ForgotPasswordConfirm");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var returnUrl = Url.Action("ResetPassword", "Account", new {userId = user.Id, code }, protocol: HttpContext.Request.Scheme);

                await _emailSender.SendEmailAsync(fpViewModel.Email, "Password recovery - Identity project", $"Please recover your password clicking here: {returnUrl}");
                return RedirectToAction("ForgotPasswordConfirm");
            }

            return View(fpViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirm()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(PasswordRecoverViewModel prViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(prViewModel.Email);
                if (user == null)
                {
                    return RedirectToAction("RecoverPasswordConfirm");
                }

                var result = await _userManager.ResetPasswordAsync(user, prViewModel.Code, prViewModel.ConfirmPassword);

                if (result.Succeeded) {
                    return RedirectToAction("RecoverPasswordConfirm");
                }
                ValidateErrors(result);
            }

            return View(prViewModel);
        }

        [HttpGet]
        public IActionResult RecoverPasswordConfirm()
        {
            return View();
        }

    }
}
