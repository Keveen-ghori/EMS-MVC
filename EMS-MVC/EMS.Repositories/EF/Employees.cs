﻿using System;
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
    public class Employees
    {
        [Key]
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
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DOB { get; set; }

        [Display(Name = Resourses.Gender)]
        [Required(ErrorMessage = SystemMessages.Gender)]
        public byte? Gender { get; set; }

        [NotMapped]
        [Display(Name = Resourses.ConfirmPassword)]
        [Required(ErrorMessage = SystemMessages.ConfirmPasswordRequired)]
        [Compare(nameof(Password), ErrorMessage = SystemMessages.ConfirmPasswordValidation)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;

        public DateTime? Created_At { get; set; } = DateTime.Now;
        public DateTime? Deleted_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public int? Attemps { get; set; } = 0;

        [Display(Name =Resourses.Total_Attemps)]
        public int? Total_Attemps { get; set; } = 3;
        public bool Status { get; set; } = true;
        public bool IsLocked { get; set; } = false;

        [Display(Name = Resourses.UserName)]
        public string? UserName { get; set; } = String.Empty;

        [NotMapped]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = SystemMessages.PasswordRequired)]
        [Display(Name = Resourses.Password)]
        public string? LoginPassword { get; set; } = nameof(Password);

        [Display(Name = Resourses.ExpDays)]
        public int Exp_Days { get; set; } = 7;

        public DateTime Password_Updated_At { get; set; } = DateTime.Now;

        [NotMapped]
        public bool PasswordUpdated { get; set; } = false;

        [NotMapped]
        public bool isUserExists { get; set; } = false;

        [NotMapped]
        public int PageNumber { get; set; } = 1;
        [NotMapped]
        public int PageSize { get; set; } = 5;
        [NotMapped]
        public bool? Lock { get; set; } = null;
    }
}
