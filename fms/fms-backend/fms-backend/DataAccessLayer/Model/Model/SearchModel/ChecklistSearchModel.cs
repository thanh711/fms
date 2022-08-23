using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Model.SearchModel
{
    public class ChecklistSearchModel : BasicSearchModel
    {
        public string LocationCode { get; set; }
        public string RoomCode { get; set; }
        public string CampusName { get; set; }
        public string ChecklistType { get; set; }
        public string Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }
}
