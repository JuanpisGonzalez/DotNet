using DotNetMvcIdentity.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMvcIdentity.Controllers
{
    public class AccountController : Controller
    {
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
    }
}
