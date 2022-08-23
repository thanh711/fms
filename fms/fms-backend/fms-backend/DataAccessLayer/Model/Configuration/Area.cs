using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Configuration
{
    public class Area 
    {
        public int ID { get; set; }
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string CampusName { get; set; }
        public string LocationCode { get; set; }
        public bool InService { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }

     
    }
}
