using System.ComponentModel.DataAnnotations;

namespace EMS.API.Models.DTO
{
    public class EmployeeDto
    {
        public long EmployeeId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        public byte Gender { get; set; }
        public bool IsLocked { get; set; }
        public bool Status { get; set; }
        public int? Attemps { get; set; }
        public int? Total_Attemps { get; set; }
        public int? Exp_days { get; set; }
        public DateTime? Password_Updated_At { get; set; } = DateTime.Now;
    }
}
