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
    public class AreaService : IAreaService
    {
        private readonly BaseService<Area> _baseService;
        public AreaService(IConfiguration configuration)
        {
            _baseService = new BaseService<Area>(configuration);
        }
        public ApiResult<Area> Delete(int id)
        {
            string query = "delete from [Configuration.Areas] where ID = " + id;

            return _baseService.Delete(query);
        }

        public ApiResult<Area> GetAll()
        {
            string query = "SELECT area.*, loc.Code as locationCode, cam.[Name] as campusName " +
                "FROM [Configuration.Areas] area " +
                "INNER JOIN[Configuration.Locations] loc ON area.LocationID = loc.ID " +
                "INNER JOIN[Configuration.Campuses] cam ON loc.CampusID = cam.ID ";
            return _baseService.GetAll(query);
        }

        public ApiResult<Area> GetList(AreaSearchModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", model.paging.CurrentPage);
            parameters.Add("@PageSize", model.paging.PageSize);
            parameters.Add("@CampusName", model.Campus);
            parameters.Add("@LocationCode", model.LocationCode);
            parameters.Add("@RoomCode", model.RoomCode);

            return _baseService.GetList(model.paging,StoredProcedure.GetListArea,parameters);
        }

        public ApiResult<Area> GetAllInService()
        {
            string query = "SELECT area.*, loc.Code as locationCode, cam.[Name] as campusName " +
                "FROM [Configuration.Areas] area " +
                "INNER JOIN[Configuration.Locations] loc ON area.LocationID = loc.ID " +
                "INNER JOIN[Configuration.Campuses] cam ON loc.CampusID = cam.ID WHERE area.InService = 1";
            return _baseService.GetAll(query);
        }

        public ApiResult<Area> GetListInService(AreaSearchModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", model.paging.CurrentPage);
            parameters.Add("@PageSize", model.paging.PageSize);
            parameters.Add("@CampusName", model.Campus);
            parameters.Add("@LocationCode", model.LocationCode);
            parameters.Add("@RoomCode", model.RoomCode);

            return _baseService.GetList(model.paging, StoredProcedure.GetListAreaInService, parameters);
        }

        public ApiResult<Area> Save(Area model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ID", model.ID);
            parameters.Add("@Name", model.Name);
            parameters.Add("@FullName", model.FullName);
            parameters.Add("@LocationID", model.LocationID);
            parameters.Add("@CreateBy", model.CreatedBy);

            return _baseService.Save(StoredProcedure.SaveArea, parameters);
        }

        public ApiResult<Area> ChangeInService(Area model)
        {
            string query = "UPDATE [Configuration.Areas] SET InService = " + (model.InService ? 1 : 0) + " WHERE ID = " + model.ID + "";
            return _baseService.ChangeActive(query);
        }

        public ApiResult<Area> GetByListLocation(List<string> locations)
        {
            string condition = "(";
            foreach (string loc in locations)
            {
                condition += "'" + loc + "',";
            }
            condition = condition.Substring(0, condition.Length - 1);
            condition += ")";
            string query = "SELECT A.* FROM [Configuration.Areas] A " +
                "INNER JOIN [Configuration.Locations] L ON L.ID = A.LocationID " +
                "WHERE A.InService = 1 AND L.Code IN " + condition;
            return _baseService.GetAll(query);
        }
    }
}
