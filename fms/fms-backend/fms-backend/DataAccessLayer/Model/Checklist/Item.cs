using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Checklist
{
    public class Item
    {
        public int ID { get; set; }
        public int? ComponentID { get; set; }
        public string Name { get; set; }
        public string Requirements { get; set; }
        public bool DefaultValue { get; set; }
        public List<String> ListReqs { get; set; }
        public List<Requirement> RequirementList { get; set; }
        public string Status { get; set; }
        public string NOK { get; set; }
        public string Note { get; set; }
        public List<String> ListNOK { get; set; }
        public string NotPass { get; set; }
    }
}
