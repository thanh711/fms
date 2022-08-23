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
    public class CampusBLL : ICampusBLL
    {
        private readonly ICampusService _campusService;
        public CampusBLL(ICampusService campusService)
        {
            _campusService = campusService;
        }

        public ApiResult<Campus> ChangeInService(Campus model)
        {
            return _campusService.ChangeInService(model);
        }

        public ApiResult<Campus> Delete(int id)
        {
            return _campusService.Delete(id);
        }

        public ApiResult<Campus> GetAll()
        {
            return _campusService.GetAll();
        }

        public ApiResult<Campus> GetAllInService()
        {
            return _campusService.GetAllInService();
        }

        public ApiResult<Campus> GetList(CampusSearchModel model)
        {
            return _campusService.GetList(model);
        }

        public ApiResult<Campus> GetListInService(CampusSearchModel model)
        {
            return _campusService.GetListInService(model);
        }

        public ApiResult<Campus> Save(Campus model)
        {
            return _campusService.Save(model);
        }
    }
}
