using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Warehouse
{
    public class WarehouseImportModel
    {
        public string CurrentUser { get; set; }
        public List<WarehouseImportItem> ImportList { get; set; }
    }
   
}
