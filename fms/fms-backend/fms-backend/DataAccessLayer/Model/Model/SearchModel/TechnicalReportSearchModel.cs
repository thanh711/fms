using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Model.SearchModel
{
    public class TechnicalReportSearchModel : BasicSearchModel
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public DateTime? ExportDate { get; set; }
        public string LocationCode { get; set; }
        public string RoomCode { get; set; }
        public int RoleID { get; set; }
        public string Campus { get; set; }
        public string User { get; set; }
    }
}
