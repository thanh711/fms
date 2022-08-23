using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Checklist
{
    public class CustomizeModel
    {
        public Component Component { get; set; }
        public List<Item> Items { get; set; }
    }
}
