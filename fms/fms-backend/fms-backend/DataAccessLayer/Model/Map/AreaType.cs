using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Map
{
    public class AreaType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderLine { get; set; }
        public string BorderColor { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
    }
}
