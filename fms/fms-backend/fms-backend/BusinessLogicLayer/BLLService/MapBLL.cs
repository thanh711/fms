using BusinessLogicLayer.IBLLService;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Map;
using DataAccessLayer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLLService
{
    public class MapBLL : IMapBLL
    {
        private readonly IMapService _service;
        public MapBLL(IMapService service)
        {
            _service = service;
        }
        public ApiResult<AreaType> GetAllAreaType()
        {
            return _service.GetAllAreaType();
        }

        public ApiResult<Area> GetListAreaSelect(FilterModel filter)
        {
            return _service.GetListAreaSelect(filter);
        }

        public ApiResult<MapApiResult> GetMap(FilterModel filter)
        {
            return _service.GetMap(filter);
        }

        public ApiResult<MapApiResult> SaveMap(SaveMapModel saveModel)
        {
            return _service.SaveMap(saveModel);
        }
    }
}
