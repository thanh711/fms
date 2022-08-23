using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Model.SearchModel
{
    public class CategorySearchModel : BasicSearchModel
    {
        public string CategoryL1Name { get; set; }
        public string CategoryL2Name { get; set; }
    }
}
