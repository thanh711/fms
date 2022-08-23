using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Checklist
{
    public class WeeklyChecklistModel
    {
        public string ChecklistType { get; set; }
        public string Status { get; set; }
        public List<FloorChecklistModel> Details { get; set; }
    }

    public class FloorChecklistModel
    {
        public int Floor { get; set; }
        public string Status { get; set; }
        public List<Checklist> AreaDetails { get; set; }
    }
}
