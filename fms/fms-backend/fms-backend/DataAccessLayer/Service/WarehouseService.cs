using Dapper;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Checklist;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using DataAccessLayer.Model.Troubles;
using DataAccessLayer.Model.Warehouse;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Service
{
    public class WarehouseService : IWarehouseService
    {
        private readonly BaseService<WarehouseAsset> _assetService;
        private readonly BaseService<WarehouseImportItem> _importService;
        private readonly BaseService<WarehouseExportItem> _exportService;
        private readonly BaseService<WarehouseHistory> _historyService;
        private readonly CategoryService _categoryService;
        private readonly MeasureUnitService _measureUnitService;
        private readonly IConfiguration _configuration;
        public WarehouseService(IConfiguration configuration)
        {
            _assetService = new BaseService<WarehouseAsset>(configuration);
            _importService = new BaseService<WarehouseImportItem>(configuration);
            _categoryService = new CategoryService(configuration);
            _measureUnitService = new MeasureUnitService(configuration);
            _configuration = configuration;
            _exportService = new BaseService<WarehouseExportItem>(configuration);
            _historyService = new BaseService<WarehouseHistory>(configuration);
        }

        public ApiResult<WarehouseAsset> GetAsset(string assetCode)
        {
            string query = "SELECT * FROM [Warehouse.RemainingAssets] WHERE AssetCode = '" + assetCode + "'";
            return _assetService.GetBy(query);
        }

        public ApiResult<WarehouseAsset> GetAllAsset()
        {
            string query = "SELECT * FROM [Warehouse.RemainingAssets]";
            return _assetService.GetAll(query);
        }

        public WarehouseImportItem MapDataImport(WarehouseImportItem importModel)
        {
            importModel.IsExist = false;
            WarehouseAsset asset = null;
            if (!string.IsNullOrEmpty(importModel.AssetCode))
            {
                asset = GetAsset(importModel.AssetCode)?.Data;
                importModel.IsExist = asset != null;
                if (asset != null)
                {
                    importModel.CategoryID = asset.CategoryID;
                    importModel.MeasureID = asset.MeasureID;
                    importModel.AssetName = asset.Name;
                }
            }

            if (!string.IsNullOrEmpty(importModel.CategoryName))
            {
                var res = _categoryService.GetByName(importModel.CategoryName);
                importModel.CategoryID = res.Data?.ID;
                if (res.Data != null && asset != null)
                {
                    if (asset.CategoryID != res.Data.ID)
                    {
                        importModel.CategoryID = asset.CategoryID;
                        importModel.CategoryError = "Conflict category! Can't import as [" + importModel.CategoryName + "]";
                    }
                }
            }
            else
            {
                importModel.CategoryID = null;
            }
            if (!string.IsNullOrEmpty(importModel.MeasureName))
            {
                var meaRes = _measureUnitService.GetByName(importModel.MeasureName);
                importModel.MeasureID = meaRes.Data?.ID;
                if (asset != null)
                {
                    if (asset.MeasureID != meaRes.Data?.ID)
                    {
                        importModel.MeasureID = asset.MeasureID;
                        importModel.MeasureError = "Conflict measure unit! Can't import as [" + importModel.MeasureName + "]";
                    }
                }
            }
            else
            {
                importModel.MeasureName = null;
            }
            importModel.IsValid = !string.IsNullOrEmpty(importModel.Reason) && importModel.ImportDate < DateTime.Now
                && !string.IsNullOrEmpty(importModel.AssetName) && !string.IsNullOrEmpty(importModel.AssetCode) &&
                !string.IsNullOrEmpty(importModel.CategoryName) && !string.IsNullOrEmpty(importModel.MeasureName)
                && importModel.Quantity > 0;
            return importModel;
        }

        public ApiResult<WarehouseImportItem> SaveImportWarehouse(WarehouseImportModel model)
        {
            foreach (var item in model.ImportList)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AssetCode", item.AssetCode);
                parameters.Add("@AssetName", item.AssetName);
                parameters.Add("@CategoryID", item.CategoryID);
                parameters.Add("@MeasureID", item.MeasureID);
                parameters.Add("@Quantity", item.Quantity);
                parameters.Add("@CreateBy", model.CurrentUser);
                parameters.Add("@CreateAsset", item.IsExist ? 0 : 1);
                parameters.Add("@ImportDate", item.ImportDate);
                parameters.Add("@Reason", item.Reason);
                parameters.Add("@Ready", item.IsReady);
                var res = _importService.Save(StoredProcedure.Warehouse_Import_Save, parameters);
                if (res.Status != 200)
                {
                    return res;
                }
            }
            return new ApiResult<WarehouseImportItem>
            {
                Status = 200,
                Message = Constants.MESS_SAVE_SUS
            };
        }

        public WarehouseExportItem MapExportData(WarehouseExportItem item)
        {
            var assetRes = GetAsset(item.AssetCode);
            if (assetRes?.Data == null)
            {
                item.ErrorCode = "Invalid Code";
                item.AssetCode = null;
            }
            else
            {
                if (assetRes.Data.RemainingQuantity < item.Quantity)
                {
                    item.ErrorQuantity = "Remaining quantity [" + assetRes.Data.RemainingQuantity + "] < [" + item.Quantity + "]";
                    item.Quantity = assetRes.Data.RemainingQuantity;
                }
            }
            if (!string.IsNullOrEmpty(item.References))
            {
                var urlValidate = ValidateURL(item.References);
                if (urlValidate.Status != 200)
                {
                    item.ErrorRefer = urlValidate.Message;
                    item.References = null;
                }
            }
            return item;
        }

        public ApiResult<WarehouseAsset> ValidateURL(string referURL)
        {

            int status = 400;
            string mes = "Invalid URL";

            int startIndex = referURL.LastIndexOf("http://demofms.site:3000/detailTrouble/");
            if (startIndex != -1)
            {
                string query = "SELECT * FROM [Trouble.Reports] WHERE ID = " + referURL.Substring(("http://demofms.site:3000/detailTrouble/").Length);
                var res = new BaseService<Trouble>(_configuration).GetBy(query);
                if (res.Data != null)
                {
                    status = 200;
                    mes = "Valid URL";
                }
            }
            else
            {
                startIndex = referURL.LastIndexOf("http://demofms.site:3000/checklistDetail/");
                if (startIndex != -1)
                {
                    string query = "SELECT * FROM [Checklists.Checklists] WHERE ID = '" + referURL.Substring(("http://demofms.site:3000/checklistDetail/").Length) + "'";
                    var res = new BaseService<Checklist>(_configuration).GetBy(query);
                    if (res.Data != null)
                    {
                        status = 200;
                        mes = "Valid URL";
                    }
                }
            }


            return new ApiResult<WarehouseAsset>()
            {
                Status = status,
                Message = mes
            };
        }

        public ApiResult<WarehouseExportItem> SaveExportWarehouse(WarehouseExportModel model)
        {
            foreach (var item in model.ListData)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AssetCode", item.AssetCode);
                parameters.Add("@Receiver", item.Receiver);
                parameters.Add("@Quantity", item.Quantity);
                parameters.Add("@CreateBy", model.CurrentUser);
                parameters.Add("@Refer", item.References);
                parameters.Add("@ExportDate", item.ExportDate);
                parameters.Add("@Reason", item.Reason);
                var res = _exportService.Save(StoredProcedure.Warehouse_Export_Save, parameters);
                if (res.Status != 200)
                {
                    return res;
                }
            }
            return new ApiResult<WarehouseExportItem>
            {
                Status = 200,
                Message = Constants.MESS_SAVE_SUS
            };
        }

        public ApiResult<WarehouseHistory> GetListHistory(WarehouseSearchModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CateName", model.CategoryName);
            parameters.Add("@AssetName", model.AssetName);
            parameters.Add("@From", model.From);
            parameters.Add("@To", model.To);
            parameters.Add("@PageIndex", model.paging.CurrentPage);
            parameters.Add("@PageSize", model.paging.PageSize);
            var res = _historyService.GetList(model.paging, StoredProcedure.Warehouse_GetList_History, parameters);
            return res;
        }

        public ApiResult<WarehouseAsset> GetListRemaining(WarehouseSearchModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CateName", model.CategoryName);
            parameters.Add("@AssetName", model.AssetName);
            parameters.Add("@PageIndex", model.paging.CurrentPage);
            parameters.Add("@PageSize", model.paging.PageSize);
            var res = _assetService.GetList(model.paging, StoredProcedure.Warehouse_GetList_Remaining, parameters);
            return res;
        }

        public ApiResult<WarehouseAsset> GetListConfig(WarehouseSearchModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CateName", model.CategoryName);
            parameters.Add("@AssetName", model.AssetName);
            parameters.Add("@PageIndex", model.paging.CurrentPage);
            parameters.Add("@PageSize", model.paging.PageSize);
            var res = _assetService.GetList(model.paging, StoredProcedure.Warehouse_GetList_Config, parameters);
            return res;
        }

        public ApiResult<WarehouseAsset> ChangeReady(UpdateByID model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ID", model.ID);
            parameters.Add("@CurrentUser", model.CurrentUser);
            var res = _assetService.Save(StoredProcedure.Warehouse_Import_ChangeReady, parameters);
            return res;
        }

        public ApiResult<WarehouseAsset> UpdateStandard(WarehouseAsset model)
        {
            string query = "UPDATE [Warehouse.RemainingAssets] SET [MinQuantity] = " + model.MinQuantity +
                ", [Updated] = GETDATE(),[UpdatedBy] = '" + model.UpdatedBy + "' WHERE [AssetCode] = '" + model.AssetCode + "'";
            var res = _assetService.SaveByQuery(query);
            return res;
        }
    }
}

