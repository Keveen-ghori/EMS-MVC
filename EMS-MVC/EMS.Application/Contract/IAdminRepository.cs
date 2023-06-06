using EMS.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.Contract
{
    public interface IAdminRepository
    {
        Task<List<EmployeeApiVM>> GetAllEmployee();
        Task<Response<EmployeeApiVM>> GetEmpById(long EMployeeid);
        Task DeleteEmp(long EmployeeId);
        Task UpdateEmp(long EMployeeId, UpdateEMployeeViewModel model);
        Task CreateEmp(CreateEmpViewModel model);
    }
}
