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
            if(HttpContext.Session.GetInt32("EmpId") == 0)
            {
                return RedirectToAction(Actions.Login, Controllors.Account);
            }
            return View();
        }
    }
}
