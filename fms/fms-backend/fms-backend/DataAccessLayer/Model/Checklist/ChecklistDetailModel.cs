using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Checklist
{
    public class ChecklistDetailModel
    {
        public string ID { get; set; }
        public string ChecklistType { get; set; }
        public string CampusName { get; set; }
        public string LocationName { get; set; }
        public string AreaName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? CheckingDate { get; set; }
        public string Status { get; set; }
        public string Issues { get; set; }
        public string Technician { get; set; }
        public string Review { get; set; }
        public string Solution { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string Requirements { get; set; }
        public string ItemStatus { get; set; }
        public string NOK { get; set; }
        public string Note { get; set; }
    }

}
