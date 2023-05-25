using EMS.Repository.EF;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace EMS.Site.Areas.Admin.Models
{
    public class SpEmployeeListsViewModel
    {
        public List<Employees> EmpList { get; set; } = new List<Employees>();
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
