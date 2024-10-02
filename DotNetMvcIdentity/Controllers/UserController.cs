using DotNetMvcIdentity.Data;
using DotNetMvcIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMvcIdentity.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        public UserController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Update profile
        [HttpGet]
        [Authorize]
        public IActionResult UpdateProfile(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbUser = _context.AppUsers.Find(id);
            if (dbUser == null)
            {
                return NotFound();
            }

            return View(dbUser);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.AppUsers.FindAsync(appUser.Id);
                user.PhoneNumber = appUser.PhoneNumber;
                user.Name = appUser.Name;
                user.Address = appUser.Address;
                user.Url = appUser.Url;
                user.Birthdate = appUser.Birthdate;
                user.Email = appUser.Email;
                user.City = appUser.City;
                user.Country = appUser.Country;
                user.CountryCode = appUser.CountryCode;

                await _userManager.UpdateAsync(user);

                return RedirectToAction(nameof(Index), "Home");
            }
            return View(appUser);
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel, string email)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null) { 
                    return View("~/Views/Account/Error.cshtml");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, changePasswordViewModel.Password);
                if (result.Succeeded) {
                    return RedirectToAction("ChangePasswordConfirmation");
                }
                else
                {
                    return View(changePasswordViewModel);
                }
            }
            return View(changePasswordViewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePasswordConfirmation()
        {
            return View();
        }
    }
}
