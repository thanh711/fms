using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Checklist
{
    public class ChecklistDetails
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public List<Requirement> Requirements { get; set; }
        public List<String> NOKList { get; set; }
        public string NotPass { get; set; }
    }
}
