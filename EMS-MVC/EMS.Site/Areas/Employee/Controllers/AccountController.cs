using Azure.Core;
using EMS.Core.Constants;
using EMS.Repositories.EF;
using EMS.Repository.EF;
using EMS.Site.Areas.Employee.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;

namespace EMS.Site.Areas.Employee.Controllers
{
    [Area(Area.Employee)]
    public class AccountController : Controller
    {
        private readonly EmployeeManagementContext _context;

        public AccountController(EmployeeManagementContext context)
        {
            _context = context;
        }

        #region EmpRegisterGet
        [HttpGet]
        [ActionName(Actions.Register)]
        public IActionResult Register()
        {
            EmployeeViewModel model = new();
            return View(model);
        }
        #endregion

        #region EmpRegister
        [HttpPost]
        [ActionName(Actions.Register)]
        public async Task<IActionResult> Register(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var checkmail = await _context.Employees.Where(emp => emp.Email == model.Employee.Email).FirstOrDefaultAsync();

                if (checkmail == null)
                {
                    string passwordHashString = Crypto.HashPassword(model.Employee.Password);

                    var newEmployee = new Employees
                    {
                        FirstName = model.Employee.FirstName,
                        LastName = model.Employee.LastName,
                        Email = model.Employee.Email,
                        DOB = model.Employee.DOB,
                        Gender = model.Employee.Gender,
                        Password = passwordHashString,
                        UserName = model.Employee.FirstName + " " + model.Employee.LastName,
                        Password_Updated_At = DateTime.Now
                    };

                    _context.Employees.Add(newEmployee);
                    _context.SaveChanges();
                    TempData[ToastrMessages.EmpRegSuccess] = ToastrMessages.EmpRegSuccess;
                    return RedirectToAction(Actions.Login);
                }
                else
                {
                    TempData[ToastrMessages.EmpEmailExists] = ToastrMessages.EmpEmailExists;
                    return View(model);
                }
            }
            return View(model);

        }

        #endregion

        #region EmpLoginGet
        [HttpGet]
        [ActionName(Actions.Login)]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("EmpId") != null)
            {
                return RedirectToAction(Actions.Index, Controllors.Home);
            }
            EmployeeViewModel model = new();
            return View(model);
        }
        #endregion

        #region EmpLogin
        [HttpPost]
        [ActionName(Actions.Login)]
        public async Task<IActionResult> Login(EmployeeViewModel model)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(emp => emp.Email == model.Employee.Email);
            if (employee != null)
            {
                if ((employee.Attemps == employee.Total_Attemps) && employee.IsLocked)
                {
                    TempData[ToastrMessages.EmpisLocked] = ToastrMessages.EmpisLocked;
                    return View(model);
                }
                else
                {
                    bool passwordMatch = Crypto.VerifyHashedPassword(employee.Password, model.Employee.LoginPassword);

                    if (!passwordMatch)
                    {
                        if (employee.Attemps != employee.Total_Attemps)
                        {
                            employee.Attemps += 1;
                            int? remainingAttemps = employee.Total_Attemps - employee.Attemps;
                            TempData[ToastrMessages.EmpWrongPass] = ToastrMessages.EmpWrongPass;
                            if (remainingAttemps == 0)
                            {
                                TempData[ToastrMessages.EmpisLocked] = ToastrMessages.EmpRemainingTry;
                            }
                            if (remainingAttemps != 0)
                            {
                                TempData[ToastrMessages.EmpRemainingTry] = @$"{ToastrMessages.EmpWrongPass} You have {remainingAttemps}  {ToastrMessages.EmpRemainingTry} ";
                            }
                        }
                        if (employee.Attemps == employee.Total_Attemps)
                        {
                            employee.IsLocked = true;
                            TempData[ToastrMessages.EmpisLocked] = ToastrMessages.EmpisLocked;
                        }

                        _context.Employees.Update(employee);
                        _context.SaveChanges();
                        return View(model);
                    }

                    else
                    {
                        DateTime updatedPassDate = employee.Password_Updated_At;
                        DateTime currentDate = DateTime.Now;

                        int expDays = employee.Exp_Days;

                        TimeSpan passExpirationPeriod = TimeSpan.FromDays((double)expDays);
                        DateTime passExpirationDate = updatedPassDate + passExpirationPeriod;

                        if (currentDate > passExpirationDate)
                        {
                            employee.Attemps = 0;
                            _context.Employees.Update(employee);
                            _context.SaveChanges();
                            TempData[ToastrMessages.NeedEmpUpdatePass] = ToastrMessages.NeedEmpUpdatePass;
                            HttpContext.Session.SetString("empPassUpdated", "No");
                            HttpContext.Session.SetInt32("EmpId", Convert.ToInt32(employee.EmployeeId));
                            HttpContext.Session.SetString("Uname", employee.UserName ?? "");
                            TempData[ToastrMessages.EmpLoginSuccess] = ToastrMessages.EmpLoginSuccess;
                            return RedirectToAction(Actions.UpdatePass);
                        }
                        employee.Attemps = 0;
                        _context.Employees.Update(employee);
                        _context.SaveChanges();
                        HttpContext.Session.SetInt32("EmpId", Convert.ToInt32(employee.EmployeeId));
                        HttpContext.Session.SetString("Uname", employee.UserName ?? "");
                        HttpContext.Session.SetString("empPassUpdated", "Yes");
                        TempData[ToastrMessages.EmpLoginSuccess] = ToastrMessages.EmpLoginSuccess;
                        return RedirectToAction(Actions.Index, Controllors.Home);
                    }

                }
            }

            TempData[ToastrMessages.UserNotFound] = ToastrMessages.UserNotFound;
            return View(model);


        }
        #endregion

        #region Update Password
        [ActionName(Actions.UpdatePass)]
        [HttpGet]
        public IActionResult UpdatePassword()
        {
            if (HttpContext.Session.GetInt32("EmpId") != null)
            {
                UpdatePassword model = new();
                return View(model);
            }
            else
            {
                return View(Actions.Login);
            }
        }

        [ActionName(Actions.UpdatePass)]
        [HttpPost]
        public IActionResult UpdatePassword(UpdatePassword model)
        {

            if (ModelState.IsValid)
            {
                var EmpId = Convert.ToInt64(HttpContext.Session.GetInt32("EmpId"));

                var emp = _context.Employees.Where(emp => emp.EmployeeId == EmpId).FirstOrDefault();
                bool newPass = Crypto.VerifyHashedPassword(emp?.Password, model.NewPassword);
                if (newPass)
                {
                    ModelState.AddModelError("NewPassError", ToastrMessages.EmpDifferentPass);
                    return View(model);
                }
                else if (Crypto.VerifyHashedPassword(emp.Password, model.OldPassword))
                {
                    emp!.Password = Crypto.HashPassword(model.NewPassword);
                    emp.Password_Updated_At = DateTime.Now;
                    _context.Employees.Update(emp);
                    _context.SaveChanges();
                    HttpContext.Session.SetString("empPassUpdated", "Yes");
                    TempData[ToastrMessages.EmpSuccessfullyUpdatedPassword] = ToastrMessages.EmpSuccessfullyUpdatedPassword;
                    return RedirectToAction(Actions.Index, Controllors.Home);
                }
                else
                {
                    ModelState.AddModelError("OldPass", ToastrMessages.EmpWrongOldPass);
                    return View();
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
