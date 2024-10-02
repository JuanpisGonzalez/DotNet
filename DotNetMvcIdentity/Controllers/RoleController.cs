using DotNetMvcIdentity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMvcIdentity.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public RoleController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            if (await _roleManager.RoleExistsAsync(role.Name))
            {
                TempData["Error"] = "The role alredy exists";
                return RedirectToAction(nameof(Index));
            }

            //Create role
            await _roleManager.CreateAsync(new IdentityRole() { Name = role.Name });
            TempData["Successful"] = "The role has been created";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return View();
            }
            else
            {
                //Update role
                var rolDb = _context.Roles.FirstOrDefault(r => r.Id == id);
                return View(rolDb);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(IdentityRole role)
        {
            if (await _roleManager.RoleExistsAsync(role.Name))
            {
                TempData["Error"] = "The role alredy exists";
                return View();
            }

            //Update role
            var rolDb = _context.Roles.FirstOrDefault(r => r.Id == role.Id);
            if (rolDb == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                rolDb.Name = role.Name;
                rolDb.NormalizedName = role.Name.ToUpper();
                var result = await _roleManager.UpdateAsync(rolDb);
                TempData["Successful"] = "The role has been updated";
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var rolDb = _context.Roles.FirstOrDefault(r => r.Id == id);
            if (rolDb == null)
            {
                TempData["Error"] = "The role doesn't exist";
                return RedirectToAction(nameof(Index));
            }

            var roleUsers = _context.UserRoles.Where(u => u.RoleId == id).Count();
            if (roleUsers > 0)
            {
                TempData["Error"] = "The role has users";
                return RedirectToAction(nameof(Index));
            }

            await _roleManager.DeleteAsync(rolDb);
            TempData["Successful"] = "The role has been deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}