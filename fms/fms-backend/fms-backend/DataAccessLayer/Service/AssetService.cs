using Dapper;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using Microsoft.Extensions.Configuration;
using System;

namespace DataAccessLayer.Service
{
    public class AssetService : IAssetService
    {
        private readonly IConfiguration _configuration;
        private BaseService<Asset> _baseService;
        private MeasureUnitService _measureUnitService;
        public AssetService(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseService = new BaseService<Asset>(configuration);
            _measureUnitService = new MeasureUnitService(configuration);
        }

        public ApiResult<Asset> GetAll()
        {
            string query = "SELECT asset.*, area.[Name] as AreaName , cate.[Name] as CategoryName, " +
                "loc.Code as LocationCode, cam.[Name] as CampusName, unit.[Name] as UnitName " +
                "FROM [Configuration.Assets] asset " +
                "INNER JOIN [Configuration.Areas] area on asset.AreaID = area.ID " +
                "INNER JOIN [Configuration.Locations] loc on area.LocationID = loc.ID " +
                "INNER JOIN [Configuration.Campuses] cam on loc.CampusID = cam.ID " +
                "INNER JOIN [Configuration.Categories] cate on asset.CategoryID = cate.ID " +
                "INNER JOIN [Configuration.MeasureUnits] unit on asset.MeasureUnitID = unit.ID";
            return _baseService.GetAll(query);
        }

        public ApiResult<Asset> GetList(AssetSearchModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", model.paging.CurrentPage);
            parameters.Add("@PageSize", model.paging.PageSize);
            parameters.Add("@CampusName", model.campusName);
            parameters.Add("@LocationCode", model.locationCode);
            parameters.Add("@RoomCode", model.roomName);
            parameters.Add("@AssetName", model.assetName);

            return _baseService.GetList(model.paging, StoredProcedure.GetListAsset, parameters);
        }

        public ApiResult<Asset> Save(Asset model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ID", model.ID);
            parameters.Add("@Code", model.Code);
            parameters.Add("@Name", model.Name);
            parameters.Add("@CategoryID", model.CategoryID);
            parameters.Add("@AreaID", model.AreaID);
            parameters.Add("@MeasureID", model.MeasureUnitID);
            parameters.Add("@Quantity", model.Quantity);
            parameters.Add("@StartDate", model.StartDate);
            parameters.Add("@EndDate", model.EndDate);
            parameters.Add("@CreateBy", model.CreatedBy);

            return _baseService.Save(StoredProcedure.SaveAsset, parameters);
        }

        public ApiResult<Asset> Delete(int id)
        {
            string query = "delete from [Configuration.Assets] where ID = " + id;
            return _baseService.Delete(query);
        }


        public ApiResult<MeasureUnit> GetAllMeasureUnit()
        {
            return _measureUnitService.GetAll();
        }

        public ApiResult<Asset> ChangeActive(Asset model)
        {
            string query = "UPDATE [Configuration.Assets] SET [InService] = "+(model.InService? 1 : 0)+" WHERE ID = " + model.ID;
            return _baseService.ChangeActive(query);
        }

        public ApiResult<Asset> GetByArea(string areaName)
        {
            string query = "SELECT asset.*, area.[Name] as AreaName , cate.[Name] as CategoryName, " +
                "loc.Code as LocationCode, cam.[Name] as CampusName, unit.[Name] as UnitName " +
                "FROM [Configuration.Assets] asset " +
                "INNER JOIN [Configuration.Areas] area on asset.AreaID = area.ID " +
                "INNER JOIN [Configuration.Locations] loc on area.LocationID = loc.ID " +
                "INNER JOIN [Configuration.Campuses] cam on loc.CampusID = cam.ID " +
                "INNER JOIN [Configuration.Categories] cate on asset.CategoryID = cate.ID " +
                "INNER JOIN [Configuration.MeasureUnits] unit on asset.MeasureUnitID = unit.ID " +
                "WHERE area.[Name] = '" + areaName + "'";
            return _baseService.GetAll(query);
        }
    }
}
