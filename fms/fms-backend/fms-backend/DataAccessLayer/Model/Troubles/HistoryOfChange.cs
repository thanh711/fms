using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Troubles
{
    public class HistoryOfChange
    {
        public string ID { get; set; }
        public int ReportID { get; set; }
        public string ChangeContent { get; set; }
        public string ChangeDetail { get; set; }
        public DateTime Created { get; set; }
        public string CreateBy { get; set; }
    }
}
