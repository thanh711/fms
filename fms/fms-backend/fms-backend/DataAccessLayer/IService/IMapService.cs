using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Map;
using DataAccessLayer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IService
{
    public interface IMapService
    {
        ApiResult<AreaType> GetAllAreaType();
        ApiResult<MapApiResult> GetMap(FilterModel filter);
        ApiResult<Area> GetListAreaSelect(FilterModel filter);
        ApiResult<MapApiResult> SaveMap(SaveMapModel saveModel);
    }
}
