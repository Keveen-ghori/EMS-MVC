using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Constants
{
    public static class RegularExpression
    {
        public const string Email = @"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$";
        public const string Password = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@#$%^&+=])(?=.*[!a-zA-Z0-9@#$%^&+=]).{8,}$";
        public const string FirstName = @"^[A-Za-z]+$";
        public const string LastName = @"^[A-Za-z]+$";
        
    }
}
