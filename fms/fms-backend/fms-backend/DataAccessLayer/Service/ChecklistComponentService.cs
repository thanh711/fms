using DataAccessLayer.Model.Checklist;
using DataAccessLayer.Model.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Service
{
    public class ChecklistComponentService
    {
        private readonly BaseService<Component> _baseService;

        public ChecklistComponentService(IConfiguration configuration)
        {
            _baseService = new BaseService<Component>(configuration);
        }

        public ApiResult<Component> GetAll()
        {
            string query = "SELECT * FROM [Checklists.Components] WHERE IsDelete = 0 OR IsDelete IS NULL";
            return _baseService.GetAll(query);
        }

        public Component Create(string name)
        {
            string query = "INSERT INTO [Checklists.Components]([Name],[Created],[CreatedBy]) VALUES (N'" + name + "', GETDATE(),'admin')";
            var res = _baseService.SaveByQuery(query);
            if (res.Status == 200)
            {
                string qu = "SELECT * FROM [Checklists.Components] WHERE [Name] = N'" + name + "'";
                var r = _baseService.GetBy(qu);
                if (r.Status == 200)
                {
                    return r.Data;
                }
            }
            return null;
        }

        public ApiResult<Component> GetById(int id)
        {
            string query = "SELECT * FROM [Checklists.Components] WHERE ID = " + id;
            return _baseService.GetBy(query);
        }

        public ApiResult<Component> SaveConfig(Component model)
        {
            string query = "UPDATE [Checklists.Components] SET [TemplateID] = " + model.TemplateID + "," +
                "[EffectArea] = " + (string.IsNullOrEmpty(model.EffectArea) ? "NULL" : "'" + model.EffectArea + "'") + " WHERE ID = " + model.ID;
            return _baseService.Save(query);
        }

        public ApiResult<Component> Delete(int id)
        {
            string query = "UPDATE [Checklists.Components] SET IsDelete = 1 WHERE ID = " + id;
            return _baseService.Delete(query);
        }

        public ApiResult<Component> GetListByTemplate(int tempid)
        {
            string query = "SELECT * FROM [Checklists.Components] WHERE (IsDelete = 0 OR IsDelete IS NULL) AND [TemplateID] = " + tempid;
            return _baseService.GetAll(query);
        }
    }
}
