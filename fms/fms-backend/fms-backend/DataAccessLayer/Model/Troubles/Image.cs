using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Troubles
{
    public class Image
    {
        public string ID { get; set; }
        public int ReportID { get; set; }
        public int TroubleshotingID { get; set; }
        public string Path { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
    }
}
