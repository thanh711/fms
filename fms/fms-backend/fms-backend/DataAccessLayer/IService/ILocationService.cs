using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IService
{
    public interface ILocationService
    {
        //InService
        ApiResult<Location> GetAll();
        ApiResult<Location> Save(Location model);
        ApiResult<Location> GetList(LocationSearchModel model);
        ApiResult<Location> Delete(int id);
        ApiResult<Location> ChangeInService(Location model);
        ApiResult<Location> GetByListCampus(List<string> campus);
        ApiResult<Location> GetAllInService();
        ApiResult<Location> GetListInService(LocationSearchModel model);
    }
}
