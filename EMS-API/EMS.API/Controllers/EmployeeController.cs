using EMS.API.Models;
using EMS.API.Models.Domain;
using EMS.API.Models.DTO;
using EMS.Core.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using System.Web.Helpers;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeManagementContext _context;

        public EmployeeController(EmployeeManagementContext context)
        {
            _context = context;
        }


        #region Get All Employees
        [HttpGet]
        public IActionResult GetEmployee()
        {
            var Emp = _context.Employees.ToList();

            var EmpDto = new List<EmployeeDto>();

            foreach (var item in Emp)
            {
                EmpDto.Add(new EmployeeDto
                {
                    EmployeeId = item.EmployeeId,
                    UserName = item.UserName,
                    Email = item.Email,
                    Gender = item.Gender,
                    DOB = item.DOB,
                    IsLocked = item.IsLocked,
                    Status = item.Status,
                    Exp_days = item.Exp_days,
                    Password_Updated_At = item.Password_Updated_At,
                    Attemps = item.Attemps,
                    Total_Attemps = item.Total_Attemps,
                });
            }
            return Ok(EmpDto);
        }
        #endregion
        #region Get Employees
        [HttpGet]
        [Route("{EmployeeId:long}")]
        public IActionResult GetEmpById([FromRoute] long EmployeeId)
        {
            var model = _context.Employees.FirstOrDefault(emp => emp.EmployeeId == EmployeeId && emp.Deleted_AT == null);
            var modelDto = new List<EmployeeDto>();

            if (model == null)
            {
                return NotFound();
            }
            modelDto.Add(new EmployeeDto
            {
                EmployeeId = EmployeeId,
                UserName = model.UserName,
                Email = model.Email,
                Gender = model.Gender,
                DOB = model.DOB,
                IsLocked = model.IsLocked,
                Status = model.Status,
                Exp_days = model.Exp_days,
                Password_Updated_At = model.Password_Updated_At,
                Attemps = model.Attemps,
                Total_Attemps = model.Total_Attemps,
            });
            return Ok(modelDto);
        }
        #endregion


        #region Register Employee
        [Route("Register")]
        [HttpPost]
        public IActionResult Register([FromBody] EmployeeRegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var isEmailExists = _context.Employees.FirstOrDefault(emp => emp.Email == model.Email);

                if (isEmailExists != null)
                {
                    return BadRequest("Email already exists. Please choose a different email.");
                }
                else
                {
                    var newEmployee = new Employees
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        UserName = model.FirstName + " " + model.LastName,
                        Password = Crypto.HashPassword(model.Password),
                        Gender = model.Gender,
                        DOB = model.DOB,
                        Password_Updated_At = DateTime.Now
                    };

                    _context.Add(newEmployee);
                    _context.SaveChanges();

                    var modelDto = new List<EmployeeDto>();
                    modelDto.Add(new EmployeeDto
                    {
                        EmployeeId = newEmployee.EmployeeId,
                        UserName = newEmployee.UserName,
                        Email = newEmployee.Email,
                        Gender = newEmployee.Gender,
                        DOB = newEmployee.DOB,
                        IsLocked = newEmployee.IsLocked,
                        Status = newEmployee.Status,
                        Exp_days = newEmployee.Exp_days,
                        Password_Updated_At = newEmployee.Password_Updated_At,
                        Attemps = newEmployee.Attemps,
                        Total_Attemps = newEmployee.Total_Attemps
                    });

                    return CreatedAtAction(nameof(GetEmpById), new { EmployeeId = newEmployee.EmployeeId }, modelDto);
                }
            }
        }
        #endregion

        #region Employee Login
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] EmpLoginDto model)
        {
            var employee = _context.Employees.FirstOrDefault(emp => emp.Email == model.Email && emp.Deleted_AT == null);

            if (employee == null)
            {
                return BadRequest("User does not exist.");
            }

            if (employee.IsLocked)
            {
                return BadRequest("Account locked. Please contact the administrator.");
            }

            bool isPasswordValid = Crypto.VerifyHashedPassword(employee.Password, model.Password);

            if (isPasswordValid)
            {
                var employeeDto = new EmployeeDto
                {
                    EmployeeId = employee.EmployeeId,
                    UserName = employee.UserName,
                    Email = employee.Email,
                    Gender = employee.Gender,
                    DOB = employee.DOB,
                    IsLocked = employee.IsLocked,
                    Status = employee.Status,
                    Exp_days = employee.Exp_days,
                    Password_Updated_At = employee.Password_Updated_At,
                    Attemps = employee.Attemps,
                    Total_Attemps = employee.Total_Attemps
                };

                // Reset failed login attempts if successful
                employee.Attemps = 0;
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetEmpById), new { EmployeeId = employee.EmployeeId }, employeeDto);
            }
            else
            {
                employee.Attemps += 1;
                _context.SaveChanges();

                if (employee.Attemps == employee.Total_Attemps)
                {
                    employee.IsLocked = true;
                    employee.Status = false;
                    _context.SaveChanges();
                    return BadRequest("Account locked. Please contact the administrator.");
                }

                return BadRequest("Invalid password.");
            }
        }
        #endregion

        #region Emp Delete
        [HttpDelete]
        [Route("Delete/{EmployeeId:long}")]
        public IActionResult DeleteEmp([FromRoute] long EmployeeId)
        {
            var IsEmpExists = _context.Employees.FirstOrDefault(emp => emp.EmployeeId == EmployeeId && emp.Deleted_AT == null);

            if (IsEmpExists == null)
            {
                return BadRequest("Emp not exists");
            }
            else
            {
                IsEmpExists.Deleted_AT = DateTime.Now;
                _context.SaveChanges();
                return Ok(IsEmpExists);

            }

        }
        #endregion

        #region Emp Update
        [HttpPut]
        [Route("Update/{EmployeeId:long}")]
        public IActionResult UpdateEmp([FromRoute] long EmployeeId, [FromBody] EmpUpdateDto model)
        {
            var IsEmpExists = _context.Employees.FirstOrDefault(emp => emp.EmployeeId == EmployeeId && emp.Deleted_AT == null);

            if (IsEmpExists == null)
            {
                return null;
            }
            else
            {
                IsEmpExists.Updated_At = DateTime.Now;
                IsEmpExists.FirstName = model.FirstName;
                IsEmpExists.Email = model.Email;
                IsEmpExists.LastName = model.LastName;
                IsEmpExists.Gender = model.Gender;
                IsEmpExists.DOB= model.DOB;
                IsEmpExists.Exp_days = model.Exp_days;
                IsEmpExists.IsLocked = model.IsLocked;
                IsEmpExists.Status = model.Status;
                IsEmpExists.Exp_days = model.Exp_days;
                IsEmpExists.Total_Attemps = model.Total_Attemps;
                if(ModelState.IsValid)
                {
                    _context.SaveChanges();
                    return Ok(model);
                }

                else
                {
                    return BadRequest(ModelState);
                }
            }

        }
        #endregion
    }
}
