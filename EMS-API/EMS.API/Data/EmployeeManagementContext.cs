using EMS.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Models
{
    public class EmployeeManagementContext : DbContext
    {
        public EmployeeManagementContext()
        {
        }

        public EmployeeManagementContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
    }
}
