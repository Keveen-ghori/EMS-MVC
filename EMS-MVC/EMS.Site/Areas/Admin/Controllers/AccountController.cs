using EMS.Core.Constants;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Site.Areas.Admin.Controllers
{
    [Area(Area.Employee)]
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
