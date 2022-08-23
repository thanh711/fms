using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Warehouse
{
    public class WarehouseExportModel
    {
        public string CurrentUser { get; set; }
        public List<WarehouseExportItem> ListData { get; set; }
    }
}
