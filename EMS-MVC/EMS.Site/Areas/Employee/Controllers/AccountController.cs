using Azure.Core;
using EMS.Core.Constants;
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
            #region Register
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
                        UserName = model.Employee.FirstName + " " + model.Employee.LastName
                    };

                    _context.Employees.Add(newEmployee);
                    _context.SaveChanges();
                    TempData[SystemMessages.RegisterSuccess] = SystemMessages.RegisterSuccess;
                    return RedirectToAction(Actions.Login);
                }
                else
                {
                    TempData[Resourses.EmailExist] = SystemMessages.EmailExists;
                    return View(model);
                }
            }
            return View(model);
            #endregion
        }

        [HttpGet]
        [ActionName(Actions.Login)]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ActionName(Actions.Login)]
        public async Task<IActionResult> Login(EmployeeViewModel model)
        {
            #region Login
            var employee = await _context.Employees.FirstOrDefaultAsync(emp => emp.Email == model.Employee.Email);
            #region Email
            if (employee == null)
            {
                TempData[Resourses.EmailNotFound] = SystemMessages.EmailNotExist;
                return View(model);
            }
            #endregion
            #region Locked
            else if ((bool)employee.IsLocked)
            {
                TempData[SystemMessages.Block] = SystemMessages.Block;
                return View(Actions.Login);
            }
            #endregion
            else
            {
                bool passwordMatch = Crypto.VerifyHashedPassword(employee.Password, model.Employee.Password);

                if (employee.Attemps == employee.Total_Attemps)
                {
                    employee.IsLocked = true;
                    employee.Status = false;
                    _context.Employees.Update(employee);
                    _context.SaveChanges();
                    TempData[SystemMessages.Block] = SystemMessages.Block;
                    return View(Actions.Login);
                }

                else if (passwordMatch)
                {
                    employee.Attemps = 0;
                    employee.IsLocked = false;
                    _context.Update(employee);
                    _context.SaveChanges();
                    TempData[Resourses.LoginSuccess] = ToastrMessages.LoginSuccess;

                    HttpContext.Session.SetInt32("EmpId", Convert.ToInt32(employee.EmployeeId));
                    HttpContext.Session.SetString("Uname", employee.UserName ?? "");

                    return RedirectToAction(Actions.Index, Controllors.Home);
                }
                else
                {
                    employee.Attemps += 1;
                    employee.IsLocked = true;
                    _context.Employees.Update(employee);
                    _context.SaveChanges();
                    var remaining = employee.Total_Attemps - employee.Attemps;
                    TempData[Resourses.Attemps] = @$"You have {remaining} {SystemMessages.RemainingAttempts}";
                    return RedirectToAction(Actions.Login);
                }
            }
            #endregion
        }
    }
}
