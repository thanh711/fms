using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Model
{
    public class UserSearchModel : BasicSearchModel
    {
        public string Username { get; set; }
        public string Campus { get; set; }
        public string Role { get; set; }
    }
}
