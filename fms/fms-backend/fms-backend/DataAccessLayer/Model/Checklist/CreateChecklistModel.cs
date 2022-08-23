using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Checklist
{
    public class CreateChecklistModel
    {
        public Template Template { get; set; }
        public List<Component> ComponentList { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
