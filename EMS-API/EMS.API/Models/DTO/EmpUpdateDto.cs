using System.ComponentModel.DataAnnotations;

namespace EMS.API.Models.DTO
{
    public class EmpUpdateDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        [Required]
        public DateTime? DOB { get; set; }
        [Required]
        public byte Gender { get; set; }
        public DateTime Updated_At { get; set; } = DateTime.Now;
        [Required]
        public int? Total_Attemps { get; set; } = 5;
        public bool Status { get; set; }
        public bool IsLocked { get; set; }
        public int? Exp_days { get; set; } = 7;
    }
}
