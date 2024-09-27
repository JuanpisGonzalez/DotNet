using DotNetMvcIdentity.Models;
using DotNetMvcIdentity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [AllowAnonymous]
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
                    //email confirmation
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var returnUrlEmailConfirmation = Url.Action("EmailConfirmation", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                    await _emailSender.SendEmailAsync(rgViewModel.Email, "Activate account - Identity project", $"Please activate your account: {returnUrlEmailConfirmation}");


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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
                if (user == null)
                {
                    return RedirectToAction("ForgotPasswordConfirm");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var returnUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code }, protocol: HttpContext.Request.Scheme);

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

                if (result.Succeeded)
                {
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> EmailConfirmation(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            return View(result.Succeeded ? "EmailConfirmation" : "Error");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            //Facebook return url
            var urlRedirection = Url.Action("ExternalLoginCallBack", "Account", new { ReturnUrl = returnUrl });
            var props = _signInManager.ConfigureExternalAuthenticationProperties(provider, urlRedirection);
            return Challenge(props, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallBack(string returnUrl = null, string err = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (err != null)
            {
                ModelState.AddModelError(string.Empty, $"Error in external login ${err}");
                return View(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            //Login with external provider
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                //Update access tokens
                await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                return LocalRedirect(returnUrl);
            }
            else
            {
                //if user don´t have accoun, we'll ask if he want to create one
                ViewData["returnUrl"] = returnUrl;
                ViewData["providerName"] = info.ProviderDisplayName;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var name = info.Principal.FindFirstValue(ClaimTypes.Name);
                return View("ExternalLoginConfirm", new ExternalLoginConfirmationViewModel
                {
                    Email = email,
                    Name = name
                });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirm(ExternalLoginConfirmationViewModel elcViewModel, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                //get info from external provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("Error");
                }

                var user = new AppUser { UserName = elcViewModel.Email, Email = elcViewModel.Email, Name = elcViewModel.Name };
                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);

                    if (result.Succeeded) { 
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                        return LocalRedirect(returnUrl);
                    }
                }

                ValidateErrors(result);
            }
            ViewData["returnUrl"] = returnUrl;
            return View(elcViewModel);
        }
    }
}
