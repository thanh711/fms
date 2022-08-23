using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Map;
using DataAccessLayer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IBLLService
{
    public interface IMapBLL
    {
        ApiResult<AreaType> GetAllAreaType();
        ApiResult<MapApiResult> GetMap(FilterModel filter);
        ApiResult<Area> GetListAreaSelect(FilterModel filter);
        ApiResult<MapApiResult> SaveMap(SaveMapModel saveModel);
    }
}
