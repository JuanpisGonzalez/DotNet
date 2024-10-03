using DotNetMvcIdentity.Data;
using DotNetMvcIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetMvcIdentity.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        public UserController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _context.AppUsers.ToListAsync();
            var userRoles = await _context.UserRoles.ToListAsync();
            var roles = await _context.Roles.ToListAsync();

            foreach (var user in users)
            {
                var role = userRoles.FirstOrDefault(u => u.UserId == user.Id);
                if (role == null)
                {
                    user.Role = "None";
                }
                else
                {
                    user.Role = roles.FirstOrDefault(u => u.Id == role.RoleId).Name;
                }
            }

            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult UpdateUser(string id)
        {
            var userDb = _context.AppUsers.FirstOrDefault(u => u.Id == id);
            if (userDb == null)
            {
                return NotFound();
            }

            var userRol = _context.UserRoles.ToList();
            var roles = _context.Roles.ToList();
            var role = userRol.FirstOrDefault(u => u.UserId == userDb.Id);

            if(role != null)
            {
                userDb.IdRol = roles.FirstOrDefault(u => u.Id == role.RoleId).Id;
            }
            userDb.Roles = _context.Roles.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            });
            return View(userDb);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateUser(AppUser user)
        {
            if (ModelState.IsValid)
            {
                var userDb = _context.AppUsers.FirstOrDefault(u => u.Id == user.Id);
                if(userDb == null)
                {
                    return NotFound();
                }

                var userRole = _context.UserRoles.FirstOrDefault(r => r.UserId == user.Id);
                if (userRole != null)
                {
                    var currentRole = _context.Roles.Where(r => r.Id == userRole.RoleId).Select(e => e.Name).FirstOrDefault();
                    //delete actual role
                    await _userManager.RemoveFromRoleAsync(userDb, currentRole);
                }

                //Add user to a new role
                await _userManager.AddToRoleAsync(userDb, _context.Roles.FirstOrDefault(u => u.Id == user.IdRol).Name);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));    
            }

            user.Roles = _context.Roles.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            });
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public IActionResult BlockUnblockUser(string id)
        {
            var userDb = _context.AppUsers.FirstOrDefault(u => u.Id == id);
            if(userDb == null)
            {
                return NotFound();
            }

            if(userDb.LockoutEnd != null && userDb.LockoutEnd > DateTime.Now)
            {
                TempData["Successful"] = "The user has been unlocked";
                //is block
                userDb.LockoutEnd = DateTime.Now;

            }
            else
            {
                TempData["Successful"] = "The user has been locked";
                //Block user
                userDb.LockoutEnd = DateTime.Now.AddDays(1);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            var userDb = _context.AppUsers.FirstOrDefault(u => u.Id == id);
            if (userDb == null)
            {
                return NotFound();
            }

            _userManager.DeleteAsync(userDb);

            TempData["Successful"] = "The user has been deleted";
            //_context.SaveChanges();
            return RedirectToAction(nameof(Index));
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
            TempData["Successful"] = "The user has been updated";
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
                if (user == null)
                {
                    return View("~/Views/Account/Error.cshtml");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, changePasswordViewModel.Password);
                if (result.Succeeded)
                {
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
