using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Troubles
{
    public class TroubleShooting
    {
        public int ID { get; set; }
        public int ReportID { get; set; }
        public string Technician { get; set; }
        public int CategoryID { get; set; }
        public bool Resolved { get; set; }
        public int Priority { get; set; }
        public string IssueReview { get; set; }
        public string Solution { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
    }
}
