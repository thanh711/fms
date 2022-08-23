using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Checklist
{
    public class ImportModel
    {
        public List<Item> ListItem { get; set; }
        public string ComponentName { get; set; }
        public int? ComponentId { get; set; }
        public int TypeUpdate { get; set; }
    }
}
