using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Constants
{
    public static class SystemMessages
    {
        #region Account
        public const string FirstNameRequired = "First name field is required.";
        public const string LastNameRequired = "Last name field is required.";
        public const string EmailRequired = "Email Address is Required.";
        public const string EmailValidation = "Please enter valid email address.";
        public const string PasswordRequired = "Please enter your passowrd.";
        public const string PasswordValidation = "Please Provide Valid Password";
        public const string ConfirmPasswordRequired = "Please enter your confirm passowrd.";
        public const string ConfirmPasswordValidation = "Password does not match.";
        public const string DOB = "Please provide your date of birth.";
        public const string Gender = "Please select your gender.";
        public const string RemainingAttempts = "Remaining Attempts to Login. after that your account will be blocked.";
        public const string IsLocked = "Your Account is lock. Plase connect with Admin.";
        public const string EmailExists = "This email address is already exists. Try with different email address.";
        public const string FirstNameValid = "Please enter first name only in alphabets.";
        public const string LastNameValid = "Please enter last name in only alphabets.";
        public const string EmailNotExist = "This email address does not contain any account. Create account";
        public const string UserOrEmailRequired = "User name or Email is required.";
        public const string Block = "You account is blocked. plese connect to admin.";
        public const string RegisterSuccess = "Successfully registered. Login to your account.";
        #endregion
    }
}
