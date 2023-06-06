using EMS.Repositories.EF;
using EMS.Repository.EF;

namespace EMS.Site.Areas.Admin.Models
{
    public class EmpApiViewModel
    {
        public EmpApiViewModel()
        {
            EmployeeList = new List<Employees>();
        }

        public List<Employees>? EmployeeList { get; set; }
        public Employees? Employee { get; set; }
    }
}
