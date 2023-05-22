using EMS.Core.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMS.Repository.EF;

namespace EMS.Site.Areas.Employee.Models
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            Employee = new Employees();
        }
        public Employees Employee { get; set; }

       
    }
}
