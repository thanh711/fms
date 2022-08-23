using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Map
{
   public  class MapApiResult
    {
        public List<Square> Squares { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
