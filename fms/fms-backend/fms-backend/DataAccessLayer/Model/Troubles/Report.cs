using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Troubles
{
    public class Report
    {
        public int ID { get; set; }
        public int AreaID { get; set; }
        public string Summary { get; set; }
        public bool Emergency { get; set; }
        public string Description { get; set; }
        public DateTime? InAreaTime { get; set; }
        public int WorkflowID { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
        public string LocationCode { get; set; }
        public string AreaName { get; set; }
        public string CampusName { get; set; }
        public int Step { get; set; }
        public DateTime? Finished { get; set; }

        public int SLA { get; set; }
    }
}
