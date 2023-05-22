using EMS.Core.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EMS.Site.Areas.Admin.Models
{
    public class AdminUpdatePassword
    {
        [NotMapped]
        [Display(Name = Resourses.OldPassword)]
        [Required(ErrorMessage = SystemMessages.OldPassword)]
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; } = string.Empty;

        [NotMapped]
        [Display(Name = Resourses.NewPassword)]
        [Required(ErrorMessage = SystemMessages.NewPassword)]
        [DataType(DataType.Password)]
        [RegularExpression(RegularExpression.Password, ErrorMessage = SystemMessages.PasswordValidation)]
        public string? NewPassword { get; set; } = String.Empty;

        [NotMapped]
        [Display(Name = Resourses.ConfirmPassword)]
        [Required(ErrorMessage = SystemMessages.ConfirmPasswordRequired)]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = SystemMessages.ConfirmPasswordValidation)]
        public string? ConfirmNewPassword { get; set; } = String.Empty;
    }
}
