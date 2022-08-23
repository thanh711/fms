using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Model.SearchModel
{
    public class WarehouseSearchModel : BasicSearchModel
    {
        public string CategoryName { get; set; }
        public string AssetName { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
