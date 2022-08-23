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
    public class CampusService : ICampusService
    {
        private readonly BaseService<Campus> _baseService;
        public CampusService(IConfiguration configuration)
        {
            _baseService = new BaseService<Campus>(configuration);
        }
        public ApiResult<Campus> GetAll()
        {
            string query = "select * from [Configuration.Campuses]";
            return _baseService.GetAll(query);
        }

        public ApiResult<Campus> Save(Campus model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ID", model.ID);
            parameters.Add("@Name", model.Name);
            parameters.Add("@FullName", model.FullName);
            parameters.Add("@Address", model.Address);
            parameters.Add("@Telephone", model.Telephone);
            parameters.Add("@CreateBy", model.CreatedBy);

            return _baseService.Save(StoredProcedure.SaveCampus,parameters);
        }

        public ApiResult<Campus> GetList(CampusSearchModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", model.paging.CurrentPage);
            parameters.Add("@PageSize", model.paging.PageSize);
            parameters.Add("@CampusName", model.CampusName);

            return _baseService.GetList(model.paging,StoredProcedure.GetListCampus,parameters);
        }

        public ApiResult<Campus> Delete(int id)
        {
            string query = "delete from [Configuration.Campuses] where ID = " + id;
            return _baseService.Delete(query);
        }

        public ApiResult<Campus> ChangeInService(Campus model)
        {
            string query = "UPDATE [Configuration.Campuses] SET InService = " + (model.InService ? 1 : 0) + " WHERE ID = " + model.ID + "";
            return _baseService.ChangeActive(query);
        }

        public ApiResult<Campus> GetAllInService()
        {
            string query = "select * from [Configuration.Campuses] WHERE InService = 1";
            return _baseService.GetAll(query);
        }

        public ApiResult<Campus> GetListInService(CampusSearchModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", model.paging.CurrentPage);
            parameters.Add("@PageSize", model.paging.PageSize);
            parameters.Add("@CampusName", model.CampusName);

            return _baseService.GetList(model.paging, StoredProcedure.GetListCampusInService, parameters);
        }
    }
}
