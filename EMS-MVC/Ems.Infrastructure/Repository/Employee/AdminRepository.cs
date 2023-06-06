using EMS.Application.Contract;
using EMS.Application.ViewModels;
using EMS.Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Infrastructure.Repository.Employee
{
    public class AdminRepository : IAdminRepository
    {
        private readonly EmployeeManagementContext context;

        public AdminRepository(EmployeeManagementContext context)
        {
            this.context = context;
        }

        public Task CreateEmp(CreateEmpViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEmp(long EmployeeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeApiVM>> GetAllEmployee()
        {
            throw new NotImplementedException();
        }

        public Task<Response<EmployeeApiVM>> GetEmpById(long EMployeeid)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEmp(long EMployeeId, UpdateEMployeeViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
