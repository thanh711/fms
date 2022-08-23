using Dapper;
using DataAccessLayer.IService;
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
    public class ChecklistTemplateService : IChecklistTemplateService
    {
        private readonly BaseService<Template> _baseService;
        private readonly BaseService<Item> _itemBaseService;
        private readonly ChecklistItemService _checklistItemService;

        public ChecklistTemplateService(IConfiguration configuration)
        {
            _baseService = new BaseService<Template>(configuration);
            _itemBaseService = new BaseService<Item>(configuration);
            _checklistItemService = new ChecklistItemService(configuration);
        }

        public ApiResult<Template> GetTemplateByCampus(string campus)
        {
            string query = "SELECT t.* FROM [Checklists.Templates] t " +
                "WHERE t.EffectCampus = 'All' OR t.EffectCampus LIKE '%" + campus + "%' AND IsDelete = 0";
            var res = _baseService.GetAll(query);
            return res;
        }
        public List<Item> GetAllByID(int templateId)
        {
            string query = "SELECT * FROM [Checklist.Items] WHERE TemplateID = " + templateId;
            return _itemBaseService.GetAll(query).ListData;
        }

        public ApiResult<Template> GetById(int templateId)
        {
            string query = "SELECT t.*  FROM [Checklists.Templates] t " +
                "WHERE t.ID = " + templateId;
            var template = _baseService.GetBy(query);

            if (template.Data != null)
            {
                if (!string.IsNullOrEmpty(template.Data.EffectCampus))
                {
                    template.Data.EffectCampuses = template.Data.EffectCampus.Split(';').ToList();
                }
                if (!string.IsNullOrEmpty(template.Data.EffectLocation))
                {
                    template.Data.EffectLocations = template.Data.EffectLocation.Split(';').ToList();
                }
            }
            return template;
        }

        public bool IsExisted(string name, int? typeId)
        {
            string qu = "SELECT * FROM [Checklist.Templates] WHERE [Name] = N'" + name + "' AND ChecklistTypeID=" + typeId;
            var r = _baseService.GetBy(qu);
            if (r.Status == 200)
            {
                return r.Data != null;
            }
            return false;
        }

        public ApiResult<Template> DeleteChecklistTemplate(int tempId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ID", tempId);
            return _baseService.Save(StoredProcedure.Checklist_DeleteTemplate, parameters);
        }

        public ApiResult<Item> UpdateItem(Item model)
        {
            return _checklistItemService.UpdateItem(model);
        }

        public ApiResult<Item> DeleteItem(int itemId)
        {
            return _checklistItemService.DeleteItem(itemId);
        }

        public ApiResult<Template> Save(Template model)
        {
            var parameters = new DynamicParameters();
            string campus = (model.EffectCampuses != null && model.EffectCampuses.Count > 0 )? string.Join(";", model.EffectCampuses) : "All";
            string location = (model.EffectLocations != null && model.EffectLocations.Count > 0)? string.Join(";", model.EffectLocations) : "All";
            parameters.Add("@ID", model.ID);
            parameters.Add("@Name", model.Name);
            parameters.Add("@TypeID", model.TypeID);
            parameters.Add("@EffectCampus", campus);
            parameters.Add("@EffectLocation", location);
            parameters.Add("@CreateBy",string.IsNullOrEmpty(model.CreatedBy) ? "Admin" : model.CreatedBy);

            return _baseService.SaveReturnEntity(StoredProcedure.Checklist_SaveTemplate, parameters);
        }
    }
}
