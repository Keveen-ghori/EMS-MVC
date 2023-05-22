using EMS.Core.Constants;
using EMS.Repositories.EF;
using EMS.Repository.EF;
using EMS.Site.Areas.Admin.Models;
using EMS.Site.Areas.Employee.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.EntityFrameworkCore;
using System.Web.Helpers;

namespace EMS.Site.Areas.Admin.Controllers
{
    [Area(Area.Admin)]
    public class AccountController : Controller
    {
        private readonly EmployeeManagementContext _context;

        public AccountController(EmployeeManagementContext context)
        {

            _context = context;
        }

        #region AdminLoginGet
        [HttpGet]
        [ActionName(Actions.Login)]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("AdmnId") != null)
            {
                return RedirectToAction(Actions.Index, Controllors.Home);
            }
            AdminViewModel model = new();

            var admin = _context.Admin.ToList();

            if (admin.Count == 0)
            {
                model.Admin.isUserExists = false;
                return View(model);
            }
            model.Admin.isUserExists = true;
            return View(model);
        }

        #endregion

        #region AdminRegisterGet
        [HttpGet]
        [ActionName(Actions.Register)]
        public async Task<IActionResult> Register()
        {
            AdminViewModel model = new();

            var admin = await _context.Admin.ToListAsync();

            if (admin.Count != 0)
            {
                return RedirectToAction(Actions.Login);
            }
            return View();
        }
        #endregion

        #region AdminRegisterPost
        [HttpPost]
        [ActionName(Actions.Register)]
        public async Task<IActionResult> Register(AdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                var checkmail = await _context.Admin.Where(admn => admn.Email == model.Admin.Email).FirstOrDefaultAsync();

                if (checkmail == null)
                {
                    string passwordHashString = Crypto.HashPassword(model.Admin.Password);

                    var newAdmin = new Admins
                    {
                        FirstName = model.Admin.FirstName,
                        LastName = model.Admin.LastName,
                        Email = model.Admin.Email,
                        DOB = model.Admin.DOB,
                        Gender = model.Admin.Gender,
                        Password = passwordHashString,
                        UserName = model.Admin.FirstName + " " + model.Admin.LastName,
                        Password_Updated_At = DateTime.Now
                    };

                    _context.Admin.Add(newAdmin);
                    _context.SaveChanges();
                    TempData[ToastrMessages.AdmnRegisterSuccess] = ToastrMessages.AdmnRegisterSuccess;
                    return RedirectToAction(Actions.Login);
                }
                else
                {
                    TempData[ToastrMessages.AdminExists] = ToastrMessages.AdminExists;
                    return View(model);
                }
            }
            return View(model);
        }
        #endregion

        #region Login
        [HttpPost]
        [ActionName(Actions.Login)]
        public async Task<IActionResult> Login(AdminViewModel model)
        {

            var admin = await _context.Admin.FirstOrDefaultAsync(admn => admn.Email == model.Admin.Email);

            if (admin == null)
            {
                TempData[ToastrMessages.AdminNotMach] = ToastrMessages.AdminNotMach;

                return RedirectToAction(Actions.Login);
            }

            else
            {
                bool passwordMatch = Crypto.VerifyHashedPassword(admin.Password, model.Admin.LoginPassword);

                if (passwordMatch)
                {
                    DateTime updatedPassDate = admin.Password_Updated_At;
                    DateTime currentDate = DateTime.Now;

                    int expDays = admin.Exp_Days;

                    TimeSpan passExpirationPeriod = TimeSpan.FromDays((double)expDays);
                    DateTime passExpirationDate = updatedPassDate + passExpirationPeriod;

                    if (currentDate > passExpirationDate)
                    {
                        TempData[ToastrMessages.NeedAdminUpdatePass] = ToastrMessages.NeedAdminUpdatePass;
                        TempData[ToastrMessages.AdminLoginSuccess] = ToastrMessages.AdminLoginSuccess;
                        HttpContext.Session.SetString("AdmnPassUpdated", "No");
                        HttpContext.Session.SetInt32("AdmnId", Convert.ToInt32(admin.AdminId));
                        HttpContext.Session.SetString("AdminUsername", admin.UserName ?? "");
                        return RedirectToAction(Actions.UpdatePass);
                    }

                    else
                    {
                        TempData[ToastrMessages.AdminLoginSuccess] = ToastrMessages.AdminLoginSuccess;
                        HttpContext.Session.SetInt32("AdmnId", Convert.ToInt32(admin.AdminId));
                        HttpContext.Session.SetString("AdminUsername", admin.UserName ?? "");
                        HttpContext.Session.SetString("AdmnPassUpdated", "Yes");
                        return RedirectToAction(Actions.Index, Controllors.Home);
                    }

                }
                TempData[ToastrMessages.AdminNotMach] = ToastrMessages.AdminNotMach;
                return RedirectToAction(Actions.Login);

            }

        }
        #endregion

        #region Update password
        [ActionName(Actions.UpdatePass)]
        [HttpGet]
        public IActionResult UpdatePassword()
        {
            if (HttpContext.Session.GetInt32("AdmnId") != null)
            {
                AdminUpdatePassword model = new();
                return View(model);
            }
            else
            {
                return View(Actions.Login);
            }
        }

        [ActionName(Actions.UpdatePass)]
        [HttpPost]
        public IActionResult UpdatePassword(AdminUpdatePassword model)
        {

            if (ModelState.IsValid)
            {
                var AdminId = Convert.ToInt64(HttpContext.Session.GetInt32("AdmnId"));

                var admn = _context.Admin.Where(admn => admn.AdminId == AdminId).FirstOrDefault();
                if(Crypto.VerifyHashedPassword(admn.Password, model.NewPassword))
                {
                    ModelState.AddModelError("NewPassError", ToastrMessages.AdminDifferentPass);
                    return View();
                }
                else if (Crypto.VerifyHashedPassword(admn.Password, model.OldPassword))
                {
                    admn!.Password = Crypto.HashPassword(model.NewPassword);
                    admn.Password_Updated_At = DateTime.Now;
                    _context.Admin.Update(admn);
                    _context.SaveChanges();
                    HttpContext.Session.SetString("AdmnPassUpdated", "Yes");
                    TempData[ToastrMessages.AdminSuccessfullyUpdatedPassword] = ToastrMessages.AdminSuccessfullyUpdatedPassword;
                    return RedirectToAction(Actions.Index, Controllors.Home);
                }
                else
                {
                    ModelState.AddModelError("OldPass", ToastrMessages.AdminWrongOldPass);
                    return View(model);
                }

            }
            else
            {
                return View(model);
            }
        }
        #endregion
    }
}
