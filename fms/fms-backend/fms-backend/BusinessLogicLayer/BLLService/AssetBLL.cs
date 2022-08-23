using DataAccessLayer.IService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLLService
{
    public class AssetBLL : IAssetBLL
    {
        IAssetService _assetService;
        public AssetBLL(IAssetService assetService)
        {
            _assetService = assetService;
        }

        public ApiResult<Asset> ChangeActive(Asset model)
        {
            return _assetService.ChangeActive(model);
        }

        public ApiResult<Asset> Delete(int id)
        {
            return _assetService.Delete(id);
        }

        public ApiResult<Asset> GetAll()
        {
            return _assetService.GetAll();
        }

        public ApiResult<MeasureUnit> GetAllMeasureUnit()
        {
            return _assetService.GetAllMeasureUnit();
        }

        public ApiResult<Asset> GetList(AssetSearchModel model)
        {
            return _assetService.GetList(model);
        }

        public ApiResult<Asset> Save(Asset model)
        {
            return _assetService.Save(model);
        }
    }
}
