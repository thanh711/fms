using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Warehouse
{
    public class WarehouseHistory : WarehouseAsset
    {
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public string Quantity { get; set; }
        public bool? IsReady { get; set; }
        public string Type { get; set; }
        public string Receiver { get; set; }
        public int? ImportID { get; set; }
        public int? ExportID { get; set; }
    }
}
