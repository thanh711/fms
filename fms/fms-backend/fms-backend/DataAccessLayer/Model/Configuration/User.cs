using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Configuration
{
    public class User
    {
        public string UserName { get; set; }
        public int CampusID { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int RoleID { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
        public string RoleName { get; set; }
        public string CampusName { get; set; }
    }
}
