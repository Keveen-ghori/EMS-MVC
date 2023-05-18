using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Core.Constants;
using Microsoft.EntityFrameworkCore;

namespace EMS.Repository.EF
{
    public class Employee
    {
        public long EmployeeId { get; set; }

        [Required(ErrorMessage = SystemMessages.FirstNameRequired)]
        [RegularExpression(RegularExpression.FirstName, ErrorMessage = SystemMessages.FirstNameValid)]
        [Display(Name = Resourses.FirstName)]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = Resourses.LastName)]
        [RegularExpression(RegularExpression.LastName, ErrorMessage = SystemMessages.LastNameValid)]
        public string? LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = SystemMessages.EmailRequired)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(RegularExpression.Email, ErrorMessage = SystemMessages.EmailValidation)]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessage = SystemMessages.PasswordRequired)]
        [DataType(DataType.Password)]
        [RegularExpression(RegularExpression.Password, ErrorMessage = SystemMessages.PasswordValidation)]
        public string Password { get; set; } = String.Empty;

        [Display(Name = Resourses.DOB)]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        [Display(Name = Resourses.Gender)]
        [Required(ErrorMessage = SystemMessages.Gender)]
        public byte Gender { get; set; }

        [NotMapped]
        [Display(Name = Resourses.ConfirmPassword)]
        [Required(ErrorMessage = SystemMessages.ConfirmPasswordRequired)]
        [Compare(nameof(Password), ErrorMessage = SystemMessages.ConfirmPasswordValidation)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;

        public DateTime Created_At { get; set; }
        public DateTime? Deleted_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public int Attemps { get; set; }
        public int Total_Attemps { get; set; }
        public bool Status { get; set; }
        public bool IsLocked { get; set; }

        [Display(Name =Resourses.UserName)]
        public string UserName { get; set; } = String.Empty;
    }
}
