using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IBLLService
{
    public interface ILocationBLL
    {
        ApiResult<Location> GetAll();
        ApiResult<Location> SaveLocation(Location model);
        ApiResult<Location> GetList(LocationSearchModel model);
        ApiResult<Location> Delete(int id);
        ApiResult<Location> ChangeInService(Location model);
        ApiResult<Location> GetByListCampus(List<string> campus);
        ApiResult<Location> GetAllInService();
        ApiResult<Location> GetListInService(LocationSearchModel model);

    }
}
