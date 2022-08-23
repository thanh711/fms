using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Warehouse
{
	public class WarehouseAsset
	{
		public string AssetCode { get; set; }
		public int CategoryID { get; set; }
		public string Name { get; set; }
		public float RemainingQuantity { get; set; }
		public float MinQuantity { get; set; }
		public DateTime? Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public string UpdatedBy {get;set;}
		public int MeasureID { get; set; }
		public string CategoryName { get; set; }
		public string Note { get; set; }
	}
}
