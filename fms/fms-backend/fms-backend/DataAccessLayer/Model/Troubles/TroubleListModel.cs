using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Troubles
{
    public class TroubleListModel
    {
        public int ReportID { get; set; }
        public string LocationCode { get; set; }
        public string AreaName { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Priority { get; set; }
        public string CategoryName { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Reporter { get; set; }
        public string Status { get; set; }
        public string Technician { get; set; }
        public string IssueReview { get; set; }
        public string Solution { get; set; }
        public string CreatedBy { get; set; }
        public int SLA { get; set; }

    }
}

