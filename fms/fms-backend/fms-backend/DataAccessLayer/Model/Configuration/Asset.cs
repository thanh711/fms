using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Configuration
{
    public class Asset 
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int AreaID { get; set; }
        public string AreaName { get; set; }
        public string LocationCode { get; set; }
        public string CampusName { get; set; }
        public int MeasureUnitID { get; set; }
        public string MeasureName { get; set; }
        public float Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool InService { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
        
    }
}
