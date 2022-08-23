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
    public class LocationBLL : ILocationBLL
    {
        private readonly ILocationService _locationService;
        public LocationBLL(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public ApiResult<Location> ChangeInService(Location model)
        {
            return _locationService.ChangeInService(model);
        }

        public ApiResult<Location> Delete(int id)
        {
            return _locationService.Delete(id);
        }

        public ApiResult<Location> GetAll()
        {
            return _locationService.GetAll();
        }

        public ApiResult<Location> GetAllInService()
        {
            return _locationService.GetAllInService();
        }

        public ApiResult<Location> GetByListCampus(List<string> campus)
        {
            return _locationService.GetByListCampus(campus);
        }

        public ApiResult<Location> GetList(LocationSearchModel model)
        {
            return _locationService.GetList(model);
        }

        public ApiResult<Location> GetListInService(LocationSearchModel model)
        {
            return _locationService.GetListInService(model);
        }

        public ApiResult<Location> SaveLocation(Location model)
        {
            return _locationService.Save(model);
        }
    }
}
