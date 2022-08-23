using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Model
{
    public class Paging
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int RowsCount { get; set; }
    }
}
