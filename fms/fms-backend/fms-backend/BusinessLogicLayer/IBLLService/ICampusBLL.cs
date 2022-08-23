using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IBLLService
{
    public interface ICampusBLL
    {
        ApiResult<Campus> GetAll();
        ApiResult<Campus> Save(Campus model);
        ApiResult<Campus> GetList(CampusSearchModel model);
        ApiResult<Campus> Delete(int id);
        ApiResult<Campus> ChangeInService(Campus model);
        ApiResult<Campus> GetAllInService();
        ApiResult<Campus> GetListInService(CampusSearchModel model);
    }
}
