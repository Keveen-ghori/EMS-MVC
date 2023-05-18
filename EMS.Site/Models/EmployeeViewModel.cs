using EMS.Core.Constants;
using EMS.Repository.EF;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Site.Models
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            Employee = new Employee();
        }
        public Employee Employee { get; set; }
    }
}
