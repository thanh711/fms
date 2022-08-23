using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Model.SearchModel
{
    public class WeeklyChecklistSearchModel
    {
        public string LocationCode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TemplateID { get; set; }
    }
}
