using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IBLLService
{
    public interface IAreaBLL
    {
        ApiResult<Area> GetAll();
        ApiResult<Area> Save(Area model);
        ApiResult<Area> GetList(AreaSearchModel model);
        ApiResult<Area> Delete(int id);
        ApiResult<Area> ChangeInService(Area model);
        ApiResult<Area> GetByListLocation(List<string> locations);
        ApiResult<Area> GetAllInService();
        ApiResult<Area> GetListInService(AreaSearchModel model);
    }
}
