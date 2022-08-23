using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Model
{
    public class Notify
    {
        public string Priority { get; set; }
        public string Overdue { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Refer { get; set; }
    }
}
