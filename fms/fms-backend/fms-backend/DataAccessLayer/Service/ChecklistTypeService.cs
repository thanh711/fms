using DataAccessLayer.IService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Service
{
    public class ChecklistTypeService : IChecklistTypeService
    {
        private string _cnnString;
        private readonly IConfiguration _configuration;
        private BaseService<ChecklistType> _baseService;

        public ChecklistTypeService(IConfiguration configuration)
        {
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DB");
            _baseService = new BaseService<ChecklistType>(configuration);
        }

        public ApiResult<ChecklistType> GetAllWeeky()
        {
            string query = "SELECT * FROM [Configuration.ChecklistTypes] t WHERE t.ParentType = 1 ";
            return _baseService.GetAll(query);
        }

        public ChecklistType SaveNewType(string name)
        {
            string query = "INSERT INTO [Configuration.ChecklistTypes] ([Name]) VALUES (N'" + name + "')";
            var res = _baseService.SaveByQuery(query);
            if (res.Status == 200)
            {
                string qu = "SELECT * FROM [Configuration.ChecklistTypes] WHERE [Name] = N'" + name + "'";
                var r = _baseService.GetBy(qu);
                if (r.Status == 200)
                {
                    return r.Data;
                }
            }
            return null;
        }

        public ApiResult<ChecklistType> GetAllType(string campus)
        {
            string query = "SELECT ct.*, t.[Name] AS TemplateName " +
                "FROM[Configuration.ChecklistTypes] ct " +
                "LEFT JOIN[Checklist.Templates] t ON t.ChecklistTypeID = ct.ID " +
                "WHERE t.EffectCampus = 'All' OR t.EffectCampus LIKE '%" + campus + "%'";
            return _baseService.GetAll(query);
        }

        public ApiResult<ChecklistType> GetAll()
        {
            string query = "SELECT * FROM [Configuration.ChecklistTypes] WHERE [Name] NOT IN ('Daily','Weekly')";
            return _baseService.GetAll(query);
        }

        public ApiResult<ChecklistType> GetAllByTemplateName(string tempName)
        {
            string query = "SELECT t.* FROM [Checklist.Templates] tem " +
                "INNER JOIN[Configuration.ChecklistTypes] t ON tem.ChecklistTypeID = T.ID " +
                "WHERE tem.[Name] = N'" + tempName + "'";
            return _baseService.GetAll(query);
        }

        public bool UpdateParentType(int type, int id)
        {
            string query = "UPDATE [Configuration.ChecklistTypes] SET [ParentType] = " + type + " WHERE ID = " + id;
            return _baseService.SaveByQuery(query).Status == 200;
        }


    }
}
