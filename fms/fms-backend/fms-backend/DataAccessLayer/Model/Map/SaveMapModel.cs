using DataAccessLayer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Map
{
    public class SaveMapModel : BaseImportModel<MapApiResult>
    {
        public FilterModel Filter { get; set; }
        public List<int> DeleteList { get; set; }
    }
}
