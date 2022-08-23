using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Checklist
{
    public class Template
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int TypeID { get; set; }
        public string EffectCampus { get; set; }
        public string EffectLocation { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
        public List<String> EffectCampuses { get; set; }
        public List<String> EffectLocations { get; set; }
    }
}
