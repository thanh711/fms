using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IService
{
    public interface IAssetService
    {
        ApiResult<Asset> GetAll();
        ApiResult<Asset> GetList(AssetSearchModel model);
        ApiResult<Asset> Save(Asset model);
        ApiResult<Asset> Delete(int id);
        ApiResult<MeasureUnit> GetAllMeasureUnit();
        ApiResult<Asset> ChangeActive(Asset model);
    }
}
