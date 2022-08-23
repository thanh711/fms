using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Troubles;
using DataAccessLayer.Model.Checklist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.TechnicalReport
{
    public class TechnicalReport
    {
        public DateTime ExportDate { get; set; }
        public string SystemRoom { get; set; }
        public string Location { get; set; }
        public List<Asset> Equipment { get; set; }
        public List<TroubleListModel> TroubleshootingReport { get; set; }
        public int? TotalTroubles { get; set; }
        public List<Checklist.Checklist> ChecklistReport { get; set; }
        public int? TotalChecklists { get; set; }

        public List<Cause> Causes { get; set; }
        public List<string> Solutions { get; set; }
        public List<string> Plans { get; set; }

        public string Review { get; set; }
        public string Prediction { get; set; }
        public string CurrentUser { get; set; }
    }

    public class Cause
    {
        public string Priority { get; set; }
        public string Value { get; set; }
    }
}
