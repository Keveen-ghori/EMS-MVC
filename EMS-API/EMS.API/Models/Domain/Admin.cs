using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EMS.API.Models.Domain
{
    public class Admin
    {
        public long AdminId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        public byte? Gender { get; set; }
        public DateTime? Created_At { get; set; } = DateTime.Now;
        public DateTime? Deleted_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public bool Status { get; set; } = true;
        public string? UserName { get; set; } = String.Empty;
        public string LoginPassword { get; set; } = nameof(Password);
        public int Exp_Days { get; set; } = 7;
        public DateTime Password_Updated_At { get; set; } = DateTime.Now;
    }
}
