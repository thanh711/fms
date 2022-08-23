using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Checklist
{
    public class TemplateDetail
    {
        public Template Template { get; set; }
        public List<Component> ComponentList {get;set; }
    }
}
