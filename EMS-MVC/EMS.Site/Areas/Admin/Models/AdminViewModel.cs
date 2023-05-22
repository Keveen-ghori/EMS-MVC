using EMS.Repositories.EF;
using EMS.Repository.EF;

namespace EMS.Site.Areas.Admin.Models
{
    public class AdminViewModel
    {
        public AdminViewModel()
        {
            Admin = new Admins();
        }

        public Admins Admin { get; set; }
    }
}
