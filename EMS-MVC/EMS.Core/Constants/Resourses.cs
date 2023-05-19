using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Constants
{
    public static class Resourses
    {
        #region Register
        public const string FirstName = "First Name";
        public const string LastName = "Last Name";
        public const string Email = "Email Address";
        public const string Password = "Password";
        public const string DOB = "Date of Birth";
        public const string Gender = "Gender";
        public const string Attemps = "Tried to login.";
        public const string Total_Attemps = "Total Attempts";
        public const string IsLocked = "Lock";
        public const string ConfirmPassword = "Confirm Password";
        public const string UserName = "User Name";
        #endregion

        #region Admin
        public const string AdminTitle = "Admin dashboard";
        #endregion

        #region Login
        public const string EmpLogin = "Employee Login";
        public const string EmailExist = "Email Exists";
        public const string EmailNotFound = "Email Not Found";
        public const string LoginSuccess = "Login Success";
        public const string NameOrEmail = "Username/ Email";
        #endregion
    }
}
