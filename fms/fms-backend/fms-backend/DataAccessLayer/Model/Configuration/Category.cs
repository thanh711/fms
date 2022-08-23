using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Configuration
{
    public class Category
    {
        public int ID { get; set; }
        public int ParentCategory { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
        public string ParentName { get; set; }
    }
}
