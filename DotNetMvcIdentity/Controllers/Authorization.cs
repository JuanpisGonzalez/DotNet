using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMvcIdentity.Controllers
{
    public class Authorization : Controller
    {
        [AllowAnonymous]
        public IActionResult PublicAccess()
        {
            return View();
        }

        [Authorize]
        public IActionResult AuthenticateAccess()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public IActionResult UserAccess()
        {
            return View();
        }

        [Authorize(Roles = "Registered")]
        public IActionResult RegisteredAccess()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult AdminAccess()
        {
            return View();
        }

        //Role user or role admin
        //option 1 - roles
        [Authorize(Roles = "Administrator")]
        //option 2 - policy or directives
        //[Authorize(Policy = "Administrator")]
        public IActionResult UserAdminAccess()
        {
            return View();
        }

        //User and Admin
        [Authorize(Policy = "UserAndAdmin")]
        public IActionResult UserAndAdminAccess()
        {
            return View();
        }

        [Authorize(Policy = "AdminCreatePermission")]
        public IActionResult AdminAccessCreatePermission()
        {
            return View();
        }

        [Authorize(Policy = "AdminDeleteUpdatePermission")]
        public IActionResult AdminAccessUpdateDeletePermission()
        {
            return View();
        }

        [Authorize(Policy = "AdminCreateUpdateDeletePermission")]
        public IActionResult AdminAccessCreateUpdateDeletePermission()
        {
            return View();
        }
    }
}
