using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Model
{
    public class LocationSearchModel : BasicSearchModel
    {
        public string Campus { get; set; }
        public string LocationCode { get; set; }
    }
}
