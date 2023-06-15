using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.ViewModels
{
    [DataContract]
    public class EmployeeApiVM
    {
        [DataMember]
        public string FirstName { get; set; } = string.Empty;
        [DataMember]
        public string Lastname { get; set; } = string.Empty;
        [DataMember]
        public long EmployeeId { get; set; }
        [DataMember]
        public string? UserName { get; set; } = string.Empty;
        [DataMember]
        public string Email { get; set; } = string.Empty;
        [DataMember]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; } = new DateTime(2022, 06, 15);
        [DataMember]
        public byte Gender { get; set; } 
        [DataMember]
        public int? Attemps { get; set; }
        [DataMember]
        public bool Status { get; set; }
        [DataMember]
        public bool IsLocked { get; set; }
        [DataMember]
        public int? Exp_days { get; set; }
        [DataMember]
        public DateTime? Password_Updated_At { get; set; }
    }
}
