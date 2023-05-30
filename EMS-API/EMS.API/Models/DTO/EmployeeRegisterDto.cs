using EMS.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace EMS.API.Models.DTO
{
    public class EmployeeRegisterDto
    {
        [Required]
        [RegularExpression(RegularExpression.FirstName, ErrorMessage =SystemMessages.FirstNameValid)]
        public string FirstName { get; set; } = string.Empty;
        [RegularExpression(RegularExpression.FirstName, ErrorMessage = SystemMessages.LastNameValid)]
        public string? LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [RegularExpression(RegularExpression.Email, ErrorMessage =SystemMessages.EmailValidation)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [RegularExpression(RegularExpression.Password, ErrorMessage =SystemMessages.PasswordValidation)]
        public string Password { get; set; } = String.Empty;
        [Required]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        [Required]
        public byte Gender { get; set; }

        #region Custome Fields
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
        #endregion
    }
}
