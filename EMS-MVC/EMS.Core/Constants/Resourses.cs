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
        public const string AdmLogin = "Admin Login";
        public const string AdmRegister = "Admin Register";
        #endregion

        #region Login
        public const string EmpLogin = "Employee Login";
        public const string EmpRegister = "Employee Registration";
        public const string NameOrEmail = "Username/ Email";
        #endregion

        #region PasswordUpdate
        public const string EmpUpdatePassTitle = "Update Password";
        public const string AdmnUpdatePassTitle = "Update Password";
        public const string OldPassword = "Old Password";
        public const string NewPassword = "New Password";
        #endregion

        #region Admin Dashboard
        public const string AdminDash = "Admin Dashboard";
        public const string Configue = "Admin Configuration";
        public const string ExpDays = "Password Expiry Days";
        public const string Total_Attempts = "Total Attempts";
        #endregion

        #region Employee Home
        public const string EmpHome = "Employee";
        public const string EmpManagement = "Employee Management";
        #endregion
    }
}
