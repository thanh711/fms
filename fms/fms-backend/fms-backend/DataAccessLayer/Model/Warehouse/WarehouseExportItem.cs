using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Warehouse
{
    public class WarehouseExportItem
    {
        public int ID { get; set; }
        public string AssetCode { get; set; }
        public DateTime ExportDate { get; set; }
        public string Receiver { get; set; }
        public string Reason { get; set; }
        public float Quantity { get; set; }
        public string References { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorRefer { get; set; }
        public string ErrorQuantity { get; set; }
    }
}
