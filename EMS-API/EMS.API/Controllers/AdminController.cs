using EMS.API.Models;
using EMS.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly EmployeeManagementContext _context;

        public AdminController(EmployeeManagementContext context)
        {
            _context = context;
        }

        //#region Get All Employees
        //[HttpGet]
        //public IActionResult GetEmployee()
        //{
        //    var Emp = _context.Employees.ToList();

        //    var EmpDto = new List<EmployeeDto>();

        //    foreach (var item in Emp)
        //    {
        //        EmpDto.Add(new EmployeeDto
        //        {
        //            EmployeeId= item.EmployeeId,
        //            UserName = item.UserName,
        //            Email = item.Email,
        //            Gender = item.Gender,
        //            DOB = item.DOB,
        //            IsLocked = item.IsLocked,
        //            Status = item.Status,
        //            Exp_days = item.Exp_days,
        //            Password_Updated_At = item.Password_Updated_At,
        //            Attemps = item.Attemps,
        //            Total_Attemps = item.Total_Attemps,
        //        });
        //    }
        //    return Ok(EmpDto);
        //}
        //#endregion

        //#region Get employee by Id
        //[HttpGet]
        //[Route("{EmployeeId:long}")]
        //public IActionResult GetEmployeeById([FromRoute] long EmployeeId)
        //{
        //    var model = _context.Employees.FirstOrDefault(emp => emp.EmployeeId == EmployeeId);
        //    var modelDto = new List<EmployeeDto>();

        //    if (model == null)
        //    {
        //        return NotFound();
        //    }
        //    modelDto.Add(new EmployeeDto
        //    {
        //        EmployeeId= EmployeeId,
        //        UserName = model.UserName,
        //        Email = model.Email,
        //        Gender = model.Gender,
        //        DOB = model.DOB,
        //        IsLocked = model.IsLocked,
        //        Status = model.Status,
        //        Exp_days = model.Exp_days,
        //        Password_Updated_At = model.Password_Updated_At,
        //        Attemps = model.Attemps,
        //        Total_Attemps = model.Total_Attemps,
        //    });
        //    return Ok(modelDto);

        //}
        //#endregion


    }
}
