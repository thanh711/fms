using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Model.SearchModel
{
    public class AssetSearchModel : BasicSearchModel
    {
        public string campusName { get; set; }
        public string locationCode { get; set; }
        public string roomName { get; set; }
        public string assetName { get; set; }
    }
}
