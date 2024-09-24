using DotNetMvcIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMvcIdentity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            RegisterViewModel registerVM = new RegisterViewModel();//Create a new object view

            return View(registerVM);//Return the view to be showed in the browser
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel rgViewModel)
        {
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

                if (result.Succeeded) {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");//method, controller
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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel lgViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    lgViewModel.Email,
                    lgViewModel.Password,
                    lgViewModel.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");//method, controller
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
    }
}
