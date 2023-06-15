using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Common
{
    public static class CommonSettings
    {
        public static string EmployeeBaseUrl { get; set; } = "https://localhost:44341/api/Employee/";
        public static string EmployeeWcfClient { get; set; } = "http://localhost:50076/EmployeeWebService.svc/";

    }
}
