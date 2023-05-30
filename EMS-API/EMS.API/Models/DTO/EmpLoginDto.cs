using EMS.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace EMS.API.Models.DTO
{
    public class EmpLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;
    }
}
