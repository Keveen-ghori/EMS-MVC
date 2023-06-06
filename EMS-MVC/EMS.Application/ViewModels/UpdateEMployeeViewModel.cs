using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.ViewModels
{
    [DataContract]  
    public class UpdateEMployeeViewModel
    {
        [DataMember]
        public string FirstName { get; set; } = string.Empty;
        [DataMember]
        public string? LastName { get; set; } = string.Empty;
        [DataMember]
        public DateTime? DOB { get; set; }
        [DataMember]
        public byte Gender { get; set; }
        [DataMember]
        public bool Status { get; set; } = true;
        [DataMember]
        public bool IsLocked { get; set; }
    }
}
