using EMS.Core.Constants;
using EMS.Repository.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Site.Areas.Admin.Controllers
{
    [Area(Area.Admin)]
    public class HomeController : Controller
    {
        private readonly EmployeeManagementContext _context;

        public HomeController(EmployeeManagementContext context)
        {
            _context = context;
        }

        [ActionName(Actions.Index)]
        [HttpGet]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("AdmnId") != null && HttpContext.Session.GetString("AdmnPassUpdated") == "Yes")
            {
                return View();
            }
            else if(HttpContext.Session.GetString("AdmnPassUpdated") == "No")
            {
                return RedirectToAction(Actions.UpdatePass, Controllors.Account);
            }
            return RedirectToAction(Actions.Login, Controllors.Account);
        }

        public IActionResult Employee()
        {
            return View();
        }
    }
}
