using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Checklist
{
    public class Checklist
    {
        public string ID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TemplateID { get; set; }
        public int ComponentID { get; set; }
        public int AreaID { get; set; }
        public int LocationID { get; set; }
        public string Technician { get; set; }
        public string Status { get; set; }
        public string Review { get; set; }
        public string Issues { get; set; }
        public string Solution { get; set; }
        public string Note { get; set; }
        public DateTime? CheckingDate { get; set; }
        public string ChecklistName { get; set; }
        public string ChecklistType { get; set; }
        public string LocationName { get; set; }
        public string LocationCode { get; set; }
        public string Overdue { get; set; }
        public string AreaName { get; set; }
        public int TypeID { get; set; }
        public List<String> IssuesList { get; set; }
        public string CampusName { get; set; }
    }
}
