using EMS.Core.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Site.Areas.Employee.Controllers
{
    [Area(Area.Employee)]
    public class HomeController : Controller
    {
        [HttpGet]
        [ActionName(Actions.Index)]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("EmpId") != null && HttpContext.Session.GetString("empPassUpdated") == "Yes")
            {
                return View();
            }
            else if (HttpContext.Session.GetString("empPassUpdated") == "No")
            {
                return RedirectToAction(Actions.UpdatePass, Controllors.Account);
            }
            return RedirectToAction(Actions.Login, Controllors.Account);
        }
    }
}
