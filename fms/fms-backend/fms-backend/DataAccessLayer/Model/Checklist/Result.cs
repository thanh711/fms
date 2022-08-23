using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Checklist
{
    public class Result
    {
        public string ID { get; set; }
        public string ChecklistID { get; set; }
        public int ItemID { get; set; }
        public bool Status { get; set; }
        public string Note { get; set; }
        public string NOK { get; set; }
    }
}
