using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Constants
{
    public static class Actions
    {
        public const string Login = "Login";
        public const string Register = "Register";
        public const string Index = "Index";
        public const string UpdatePass = "UpdatePassword";
        public const string Employee = "Employee";
        public const string Logout = "Logout";
        public const string EmployeeFilter = "EmployeeFilter";
        public const string DeleteEmp = "DeleteEmp";
        public const string LockEmp = "LockEmp";
        public const string Configuration = "Configuration";
        public const string EmpByApi = "GetEmployeesByApi";
        public const string SaveEmpByApi = "SaveEmployeesByApi";
        public const string GetEmpByIdApi = "GetEmployeesByIdApi";
    }
}
