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
        public HomeController(EmployeeManagementContext context)
        {
            _context = context;
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
        public IActionResult Employee(int PageNumber = 1, int PageSize = 2)
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

                    int totalRecords = (int)command.Parameters["@TotalRecords"].Value;
                    int totalPages = (int)command.Parameters["@TotalPages"].Value;
                    int currentPage = (int)command.Parameters["@CurrentPage"].Value;

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

        [HttpGet]
        [ActionName(Actions.EmployeeFilter)]
        public IActionResult EmployeeFIlter(Employees employees)
        {
            if(employees != null)
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

                    _context.Database.OpenConnection();
                    command.ExecuteNonQuery();

                    int totalRecords = (int)command.Parameters["@TotalRecords"].Value;
                    int totalPages = (int)command.Parameters["@TotalPages"].Value;
                    int currentPage = (int)command.Parameters["@CurrentPage"].Value;

                    EmpLists.TotalRecords = totalRecords;
                    EmpLists.TotalPages = totalPages;
                    EmpLists.CurrentPage = currentPage;
                }
                List<Employees> employee =_context.Employees.FromSqlInterpolated($@"EXEC GetEmployees @PageNumber={employees.PageNumber}, @PageSize={employees.PageSize}, @TotalRecords={EmpLists.TotalRecords} out, @TotalPages={EmpLists.TotalPages} out, @CurrentPage = {EmpLists.CurrentPage} out").ToList();

                EmpLists.EmpList = employee;
                return PartialView(PartialViews._EmpListsPartial, EmpLists);
            }
            return RedirectToAction(Actions.Employee);
        }
    }
}
