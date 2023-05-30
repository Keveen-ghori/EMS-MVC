using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.API.Models.Domain
{
    public class Employees
    {
        [Key]
        public long EmployeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = String.Empty;
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        public byte Gender { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime? Deleted_AT { get; set; }
        public DateTime? Updated_At { get; set; }
        public int? Attemps { get; set; } = 0;
        public int? Total_Attemps { get; set; } = 5;
        public bool Status { get; set; }
        public bool IsLocked { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int? Exp_days { get; set; } = 7;
        public DateTime? Password_Updated_At { get; set; } = DateTime.Now;

    }
}
