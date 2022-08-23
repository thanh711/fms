using Dapper;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Service
{
    public class LocationService : ILocationService
    {
        private readonly BaseService<Location> _baseService;
        public LocationService(IConfiguration configuration)
        {
            _baseService = new BaseService<Location>(configuration);
        }
        public ApiResult<Location> Delete(int id)
        {
            string query = "delete from [Configuration.Locations] where ID = " + id;
            return _baseService.Delete(query);
        }

        public ApiResult<Location> GetAll()
        {
            string query = "SELECT * FROM [Configuration.Locations]";
            return _baseService.GetAll(query);
        }

        public ApiResult<Location> GetList(LocationSearchModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", model.paging.CurrentPage);
            parameters.Add("@PageSize", model.paging.PageSize);
            parameters.Add("@CampusName", model.Campus);
            parameters.Add("@LocationCode", model.LocationCode);

            return _baseService.GetList(model.paging, StoredProcedure.GetListLocation, parameters);
        }

        public ApiResult<Location> Save(Location model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ID", model.ID);
            parameters.Add("@Name", model.Name);
            parameters.Add("@FullName", model.FullName);
            parameters.Add("@Code", model.Code);
            parameters.Add("@CampusID", model.CampusID);
            parameters.Add("@CreateBy", model.CreatedBy);

            return _baseService.Save(StoredProcedure.SaveLocation,parameters);
        }

        public ApiResult<Location> ChangeInService(Location model)
        {
            string query = "UPDATE [Configuration.Locations] SET InService = " + (model.InService ? 1 : 0) + " WHERE ID = " + model.ID + "";
            return _baseService.ChangeActive(query);
        }

        public ApiResult<Location> GetByListCampus(List<string> campus)
        {
            string condition = "(";
            foreach(string cam in campus)
            {
                condition += "N'" + cam + "',";
            }
            condition = condition.Substring(0, condition.Length - 1);
            condition += ")";
            string query = "SELECT LO.* FROM[Configuration.Locations] LO " +
                "INNER JOIN[Configuration.Campuses] CA ON LO.CampusID = CA.ID " +
                "WHERE LO.InService = 1 AND CA.[Name] IN " + condition;
            return _baseService.GetAll(query);
        }

        public ApiResult<Location> GetAllInService()
        {
            string query = "SELECT * FROM [Configuration.Locations] WHERE InService = 1";
            return _baseService.GetAll(query);
        }

        public ApiResult<Location> GetListInService(LocationSearchModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", model.paging.CurrentPage);
            parameters.Add("@PageSize", model.paging.PageSize);
            parameters.Add("@CampusName", model.Campus);
            parameters.Add("@LocationCode", model.LocationCode);

            return _baseService.GetList(model.paging, StoredProcedure.GetListLocationInService, parameters);
        }
    }
}
