using BusinessLogicLayer.IBLLService;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLLService
{
    public class AreaBLL : IAreaBLL
    {
        private readonly IAreaService _areaService;
        public AreaBLL(IAreaService areaService)
        {
            _areaService = areaService;
        }

        public ApiResult<Area> ChangeInService(Area model)
        {
            return _areaService.ChangeInService(model);
        }

        public ApiResult<Area> Delete(int id)
        {
            return _areaService.Delete(id);
        }

        public ApiResult<Area> GetAll()
        {
            return _areaService.GetAll();
        }

        public ApiResult<Area> GetAllInService()
        {
            return _areaService.GetAllInService();
        }

        public ApiResult<Area> GetByListLocation(List<string> locations)
        {
            return _areaService.GetByListLocation(locations);
        }

        public ApiResult<Area> GetList(AreaSearchModel model)
        {
            return _areaService.GetList(model);
        }

        public ApiResult<Area> GetListInService(AreaSearchModel model)
        {
            return _areaService.GetListInService(model);
        }

        public ApiResult<Area> Save(Area model)
        {
            return _areaService.Save(model);
        }
    }
}
