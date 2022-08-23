using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Warehouse
{
    public class WarehouseImportItem
    {
        public int ID { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryID { get; set; }
        public DateTime ImportDate { get; set; }
        public string Reason { get; set; }
        public int? MeasureID { get; set; }
        public string MeasureName { get; set; }
        public float Quantity { get; set; }
        public bool IsReady { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsExist { get; set; }
        public string MeasureError { get; set; }
        public string CategoryError { get; set; }
        public bool IsValid { get; set; }
    }

}
