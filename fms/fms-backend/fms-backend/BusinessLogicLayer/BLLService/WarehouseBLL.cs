using BusinessLogicLayer.IBLLService;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using DataAccessLayer.Model.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLLService
{
    public class WarehouseBLL : IWarehouseBLL
    {
        private readonly IWarehouseService _service;
        public WarehouseBLL(IWarehouseService Service)
        {
            _service = Service;
        }

        public ApiResult<WarehouseAsset> ChangeReady(UpdateByID model)
        {
            return _service.ChangeReady(model);
        }

        public ApiResult<WarehouseAsset> GetAllAsset()
        {
            return _service.GetAllAsset();
        }

        public ApiResult<WarehouseAsset> GetAsset(string assetCode)
        {
            return _service.GetAsset(assetCode);
        }

        public ApiResult<WarehouseAsset> GetListConfig(WarehouseSearchModel model)
        {
            return _service.GetListConfig(model);
        }

        public ApiResult<WarehouseHistory> GetListHistory(WarehouseSearchModel model)
        {
            return _service.GetListHistory(model);
        }

        public ApiResult<WarehouseAsset> GetListRemaining(WarehouseSearchModel model)
        {
            return _service.GetListRemaining(model);
        }

        public WarehouseImportItem MapDataImport(WarehouseImportItem importModel)
        {
            return _service.MapDataImport(importModel);
        }

        public WarehouseExportItem MapExportData(WarehouseExportItem item)
        {
            return _service.MapExportData(item);
        }

        public ApiResult<WarehouseExportItem> SaveExportWarehouse(WarehouseExportModel model)
        {
            return _service.SaveExportWarehouse(model);
        }

        public ApiResult<WarehouseImportItem> SaveImportWarehouse(WarehouseImportModel model)
        {
            return _service.SaveImportWarehouse(model);
        }

        public ApiResult<WarehouseAsset> UpdateStandard(WarehouseAsset model)
        {
            return _service.UpdateStandard(model);
        }

        public ApiResult<WarehouseAsset> ValidateURL(string referURL)
        {
            return _service.ValidateURL(referURL);
        }
    }
}
