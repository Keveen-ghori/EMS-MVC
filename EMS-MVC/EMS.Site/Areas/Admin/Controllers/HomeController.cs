using Ems.Infrastructure.Services.Base;
using EMS.Application.ViewModels;
using EMS.Core.Constants;
using EMS.Repository.EF;
using EMS.Site.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Drawing.Printing;

namespace EMS.Site.Areas.Admin.Controllers
{
    [Area(Area.Admin)]
    public class HomeController : Controller
    {
        private readonly EmployeeManagementContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(EmployeeManagementContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region Index
        [ActionName(Actions.Index)]
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("AdmnId") != null && HttpContext.Session.GetString("AdmnPassUpdated") == "Yes")
            {
                return View();
            }
            else if (HttpContext.Session.GetString("AdmnPassUpdated") == "No")
            {
                return RedirectToAction(Actions.UpdatePass, Controllors.Account);
            }
            return RedirectToAction(Actions.Login, Controllors.Account);
        }

        #endregion

        #region Employee management
        [HttpGet]
        [ActionName(Actions.Employee)]
        public IActionResult Employee(int PageNumber = 1, int PageSize = 5)
        {
            if (HttpContext.Session.GetInt32("AdmnId") != null && HttpContext.Session.GetString("AdmnPassUpdated") == "Yes")
            {
                SpEmployeeListsViewModel EmpLists = new();
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "GetEmployees";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                    command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                    command.Parameters.Add(new SqlParameter("@TotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output });
                    command.Parameters.Add(new SqlParameter("@TotalPages", SqlDbType.Int) { Direction = ParameterDirection.Output });
                    command.Parameters.Add(new SqlParameter("@CurrentPage", SqlDbType.Int) { Direction = ParameterDirection.Output });

                    _context.Database.OpenConnection();
                    command.ExecuteNonQuery();

                    int totalRecords = (int)(command.Parameters["@TotalRecords"].Value ?? 0);
                    int totalPages = (int)(command.Parameters["@TotalPages"].Value ?? 0);
                    int currentPage = (int)(command.Parameters["@CurrentPage"].Value ?? 0);

                    EmpLists.TotalRecords = totalRecords;
                    EmpLists.TotalPages = totalPages;
                    EmpLists.CurrentPage = currentPage;
                }
                List<Employees> employee = _context.Employees.FromSqlInterpolated($@"EXEC GetEmployees @PageNumber={PageNumber}, @PageSize={PageSize}, @TotalRecords={EmpLists.TotalRecords} out, @TotalPages={EmpLists.TotalPages} out, @CurrentPage = {EmpLists.CurrentPage} out").ToList();

                EmpLists.EmpList = employee;
                return View(EmpLists);
            }

            else
            {
                return RedirectToAction(Actions.Login, Controllors.Account);
            }
        }
        #endregion

        #region Employee Filters
        [HttpGet]
        [ActionName(Actions.EmployeeFilter)]
        public IActionResult EmployeeFIlter(Employees employees)
        {
            try
            {
                if (employees != null)
                {
                    SpEmployeeListsViewModel EmpLists = new();
                    using (var command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = "GetEmployees";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@PageNumber", employees.PageNumber));
                        command.Parameters.Add(new SqlParameter("@PageSize", employees.PageSize));
                        command.Parameters.Add(new SqlParameter("@TotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output });
                        command.Parameters.Add(new SqlParameter("@TotalPages", SqlDbType.Int) { Direction = ParameterDirection.Output });
                        command.Parameters.Add(new SqlParameter("@CurrentPage", SqlDbType.Int) { Direction = ParameterDirection.Output });
                        command.Parameters.Add(new SqlParameter("@Username", employees.UserName));
                        command.Parameters.Add(new SqlParameter("@Email", employees.Email));
                        command.Parameters.Add(new SqlParameter("@Gender", employees.Gender));
                        command.Parameters.Add(new SqlParameter("@DOB", employees.DOB));
                        command.Parameters.Add(new SqlParameter("@IsLocked", employees.Lock));

                        _context.Database.OpenConnection();
                        command.ExecuteNonQuery();

                        int totalRecords = (int)(command.Parameters["@TotalRecords"].Value ?? 0);
                        int totalPages = (int)(command.Parameters["@TotalPages"].Value ?? 0);
                        int currentPage = (int)(command.Parameters["@CurrentPage"].Value ?? 0);

                        EmpLists.TotalRecords = totalRecords;
                        EmpLists.TotalPages = totalPages;
                        EmpLists.CurrentPage = currentPage;
                    }
                    List<Employees> employee = _context.Employees.FromSqlInterpolated($@"EXEC GetEmployees @PageNumber={employees.PageNumber}, @PageSize={employees.PageSize},@Username={employees.UserName}, @Email={employees.Email}, @Gender={employees.Gender}, @DOB={employees.DOB}, @IsLocked={employees.Lock}, @TotalRecords={EmpLists.TotalRecords} out, @TotalPages={EmpLists.TotalPages} out, @CurrentPage = {EmpLists.CurrentPage} out").ToList();

                    EmpLists.EmpList = employee;
                    return PartialView(PartialViews._EmpListsPartial, EmpLists);
                }
                return RedirectToAction(Actions.Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return StatusCode(500, "Internal server error");
            }

        }
        #endregion

        #region Delete Employee
        [HttpPost]
        [ActionName(Actions.DeleteEmp)]
        public IActionResult DeleteEmp(long EmployeeId, int PageNumber = 1, int PageSize = 5)
        {
            var IsEmpExists = _context.Employees.FirstOrDefault(Emp => Emp.EmployeeId == EmployeeId);

            if (IsEmpExists == null)
            {
                return NotFound();
            }
            IsEmpExists.Deleted_At = DateTime.Now;
            _context.SaveChanges();
            SpEmployeeListsViewModel EmpLists = new();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "GetEmployees";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                command.Parameters.Add(new SqlParameter("@TotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output });
                command.Parameters.Add(new SqlParameter("@TotalPages", SqlDbType.Int) { Direction = ParameterDirection.Output });
                command.Parameters.Add(new SqlParameter("@CurrentPage", SqlDbType.Int) { Direction = ParameterDirection.Output });

                _context.Database.OpenConnection();
                command.ExecuteNonQuery();

                int totalRecords = (int)(command.Parameters["@TotalRecords"].Value ?? 0);
                int totalPages = (int)(command.Parameters["@TotalPages"].Value ?? 0);
                int currentPage = (int)(command.Parameters["@CurrentPage"].Value ?? 0);

                EmpLists.TotalRecords = totalRecords;
                EmpLists.TotalPages = totalPages;
                EmpLists.CurrentPage = currentPage;
            }
            List<Employees> employee = _context.Employees.FromSqlInterpolated($@"EXEC GetEmployees @PageNumber={PageNumber}, @PageSize={PageSize}, @TotalRecords={EmpLists.TotalRecords} out, @TotalPages={EmpLists.TotalPages} out, @CurrentPage = {EmpLists.CurrentPage} out").ToList();

            EmpLists.EmpList = employee;
            return PartialView(PartialViews._EmpListsPartial, EmpLists);
        }
        #endregion

        #region Lock/Unlock 
        [ActionName(Actions.LockEmp)]
        [HttpPost]
        public IActionResult LockEmp(long EmployeeId, bool status, int PageNumber, int PageSize = 5)
        {
            var IsEmpExists = _context.Employees.Find(EmployeeId);
            if (IsEmpExists == null)
            {
                return NotFound();
            }
            else
            {
                IsEmpExists.IsLocked = status;
                IsEmpExists.Updated_At = DateTime.Now;
                _context.SaveChanges();
                SpEmployeeListsViewModel EmpLists = new();
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "GetEmployees";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                    command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                    command.Parameters.Add(new SqlParameter("@TotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output });
                    command.Parameters.Add(new SqlParameter("@TotalPages", SqlDbType.Int) { Direction = ParameterDirection.Output });
                    command.Parameters.Add(new SqlParameter("@CurrentPage", SqlDbType.Int) { Direction = ParameterDirection.Output });

                    _context.Database.OpenConnection();
                    command.ExecuteNonQuery();

                    int totalRecords = (int)(command.Parameters["@TotalRecords"].Value ?? 0);
                    int totalPages = (int)(command.Parameters["@TotalPages"].Value ?? 0);
                    int currentPage = (int)(command.Parameters["@CurrentPage"].Value ?? 0);

                    EmpLists.TotalRecords = totalRecords;
                    EmpLists.TotalPages = totalPages;
                    EmpLists.CurrentPage = currentPage;
                }
                List<Employees> employee = _context.Employees.FromSqlInterpolated($@"EXEC GetEmployees @PageNumber={PageNumber}, @PageSize={PageSize}, @TotalRecords={EmpLists.TotalRecords} out, @TotalPages={EmpLists.TotalPages} out, @CurrentPage = {EmpLists.CurrentPage} out").ToList();

                EmpLists.EmpList = employee;
                return PartialView(PartialViews._EmpListsPartial, EmpLists);
            }
        }
        #endregion

        #region Employee Configuration
        [HttpGet]
        [ActionName(Actions.Configuration)]
        public IActionResult Configuration()
        {
            if (HttpContext.Session.GetInt32("AdmnId") != null && HttpContext.Session.GetString("AdmnPassUpdated") == "Yes")
            {
                var emp = _context?.Employees.FirstOrDefault();
                Employees employee = new();
                employee.Total_Attemps = emp?.Total_Attemps ?? 0;
                employee.Exp_Days = emp.Exp_Days;
                return View(employee);
            }
            else
            {
                return RedirectToAction(Actions.Login, Controllors.Account);
            }
        }

        [HttpPost]
        [ActionName(Actions.Configuration)]
        public IActionResult Configuration(int Total_attemps, int Exp_days)
        {
            var emp = _context.Employees.ToList();
            foreach (var item in emp)
            {
                item.Total_Attemps = Total_attemps;
                item.Exp_Days = Exp_days;
            }

            _context.SaveChanges();
            return RedirectToAction(Actions.Configuration);
        }
        #endregion

        #region CRUD Using Web API
        [HttpGet]
        [ActionName(Actions.EmpByApi)]
        public async Task<IActionResult> GetEmployeesByApi()
        {
            var employeeService = new EmployeeServiceBase();
            var emp = await employeeService.GetAllEmployees();

            return View(emp);
        }

        [HttpPut]
        [ActionName(Actions.SaveEmpByApi)]
        public async Task<IActionResult> SaveEmployeesByApi(Response<EmployeeApiVM> model)
        {
            var employeeService = new EmployeeServiceBase();
            if (ModelState.IsValid)
            {
                var success = await employeeService.UpdateEmployee(model, model?.Content.EmployeeId);

                if (success)
                {
                    return RedirectToAction(Actions.EmpByApi);
                }
                else
                {
                    ModelState.AddModelError("", "Failed to save employees.");
                }
            }
            var responseModel = new Response<EmployeeApiVM>
            {
                Content = model.Content,
                Messages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList()
            };

            return PartialView(PartialViews.GetElementByIdApi, responseModel);

        }

        [HttpGet]
        [ActionName(Actions.GetEmpByIdApi)]
        public async Task<IActionResult> GetEmployeesByIdApi(long EmployeeId)
        {
            var employeeService = new EmployeeServiceBase();
            var model = await employeeService.GetEmployeeByid(EmployeeId);

            return PartialView(PartialViews.GetElementByIdApi, model);

        }


        [HttpGet]
        [ActionName(Actions.CreateEmpByApiPartial)]
        public IActionResult CreateEmpByApiPartial()
        {
            CreateEmpViewModel model = new();

            return PartialView(PartialViews.AddEmpApiModal, model);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmpByIdApi(long EmployeeId)
        {
            var employeeService = new EmployeeServiceBase();
            var status = await employeeService.DeleyeEmployeeByid(EmployeeId);

            return Json(new { success = status.Success });

        }

        [HttpPost]
        [ActionName(Actions.CreateEmpByApi)]
        public async Task<IActionResult> CreateEmpByApi(CreateEmpViewModel model)
        {
            var employeeService = new EmployeeServiceBase();
            if (ModelState.IsValid)
            {
                var success = await employeeService.CreateEmpApi(model);

                if (success)
                {
                    return RedirectToAction(Actions.EmpByApi);
                }
                else
                {
                    ModelState.AddModelError("", "Failed to save employees.");
                }
            }

            return PartialView(PartialViews.GetElementByIdApi, model);
        }
        #endregion
    }
}
