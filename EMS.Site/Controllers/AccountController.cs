using EMS.Core.Constants;
using EMS.Site.Models;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Site.Controllers
{
    public class AccountController : Controller
    {

        [HttpGet]
        [ActionName(Actions.Register)]
        public IActionResult Register()
        {
            EmployeeViewModel model = new();
            return View(model);
        }

        [HttpPost]
        [ActionName(Actions.Register)]
        public async Task<IActionResult> Register(EmployeeViewModel model)
        {
            return View();
        }

        [HttpGet]
        [ActionName(Actions.Login)]
        public IActionResult Login()
        {
            EmployeeViewModel model = new();
            return View(model);
        }

        [HttpPost]
        [ActionName(Actions.Login)]
        public async Task<IActionResult> Login(EmployeeViewModel model)
        {
            return View();
        }
    }
}
