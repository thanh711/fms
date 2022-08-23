using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Map
{
    public class MapFloor
    {
        public int ID { get; set; }
        public int SizeRow { get; set; }
		public int SizeColumn { get; set; }
		public int CampusID { get; set; }
		public int LocationID { get; set; }
		public int Floor { get; set; }
    }
}
