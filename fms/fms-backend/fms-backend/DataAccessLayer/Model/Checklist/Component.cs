using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Checklist
{
    public class Component
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int TemplateID { get; set; }
        public string EffectArea { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
        public List<Item> ItemList { get; set; }
        public string Status { get; set; }
    }
}
