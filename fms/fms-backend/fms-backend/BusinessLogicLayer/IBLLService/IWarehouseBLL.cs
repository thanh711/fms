using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using DataAccessLayer.Model.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IBLLService
{
    public interface IWarehouseBLL
    {
        WarehouseImportItem MapDataImport(WarehouseImportItem importModel);
        ApiResult<WarehouseImportItem> SaveImportWarehouse(WarehouseImportModel model);
        ApiResult<WarehouseAsset> GetAsset(string assetCode);
        WarehouseExportItem MapExportData(WarehouseExportItem item);
        ApiResult<WarehouseAsset> ValidateURL(string referURL);
        ApiResult<WarehouseAsset> GetAllAsset();
        ApiResult<WarehouseExportItem> SaveExportWarehouse(WarehouseExportModel model);
        ApiResult<WarehouseHistory> GetListHistory(WarehouseSearchModel model);
        ApiResult<WarehouseAsset> GetListRemaining(WarehouseSearchModel model);
        ApiResult<WarehouseAsset> GetListConfig(WarehouseSearchModel model);
        ApiResult<WarehouseAsset> ChangeReady(UpdateByID model);
        ApiResult<WarehouseAsset> UpdateStandard(WarehouseAsset model);
    }
}
