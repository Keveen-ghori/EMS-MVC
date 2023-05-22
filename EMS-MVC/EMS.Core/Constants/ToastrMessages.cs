using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Constants
{
    public static class ToastrMessages
    {
        public const string RegisterFailed = "Some problems have occured. Try after some times.";
        public const string LoginFailed = "Login Failed.";

        #region Employee
        public const string EmpLoginSuccess = "Login Successfully.";
        public const string EmpEmailExists = "Already have account on this email address. Please use another email address.";
        public const string EmpRegSuccess = "Your account has been created successfully. Please proceed to login to access your account.";
        public const string EmpisLocked = "Your account is blocked. Try to connect to the admin.";
        public const string EmpWrongPass = "You enterd wrong Password.";
        public const string EmpRemainingTry = "remaining try. After that this account will be blocked.";
        public const string UserNotFound = "User does not exists. Create your account.";
        public const string NeedEmpUpdatePass = "Needs to update your password.";
        public const string EmpDifferentPass = "New password must be different from your old password...";
        public const string EmpWrongOldPass = "Please enter correct old password...";
        public const string EmpSuccessfullyUpdatedPassword = "Password updated successfully.";
        #endregion

        #region Admin
        public const string AdmnRegisterSuccess = "Your account has been successfully created. Please proceed to login to access your account.";
        public const string AdminExists = "Admin Already exists.";
        public const string AdminNotMach = "Username or password is wrong.";
        public const string NeedAdminUpdatePass = "Needs to update your password.";
        public const string AdminLoginSuccess = "Login Successfully.";
        public const string AdminDifferentPass = "New password must be different from your old password...";
        public const string AdminWrongOldPass = "Please enter correct old password...";
        public const string AdminSuccessfullyUpdatedPassword = "Password updated successfully.";

        #endregion
    }
}
