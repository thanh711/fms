using BusinessLogicLayer.IBLLService;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Checklist;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLLService
{
    public class ChecklistBLL : IChecklistBLL
    {
        private readonly IChecklistTemplateService _tempService;
        private readonly IChecklistService _checklistService;

        public ChecklistBLL(IChecklistTemplateService checklistTemplateService, IChecklistService checklistService)
        {
            _tempService = checklistTemplateService;
            _checklistService = checklistService;
        }

        public ApiResult<Template> GetTemplateByCampus(string campus)
        {
            return _tempService.GetTemplateByCampus(campus);
        }

        public ApiResult<Checklist> GetList(ChecklistSearchModel model)
        {
            return _checklistService.GetList(model);
        }

        public ApiResult<WeeklyChecklistModel> GetWeeklyChecklist(WeeklyChecklistSearchModel model)
        {
            return _checklistService.GetWeeklyChecklist(model);
        }

        public ApiResult<ChecklistDetailApiResult> GetDetail(string checklistId)
        {
            return _checklistService.GetDetail(checklistId);
        }

        public ApiResult<Checklist> SaveChecklist(ChecklistDetailApiResult model)
        {
            return _checklistService.SaveChecklist(model);
        }

        public ApiResult<ImportModel> SaveImport(ImportModel model)
        {
            return _checklistService.SaveImport(model);
        }

        public ApiResult<CustomizeModel> GetCustomizeDetail(int tempId)
        {
            return _checklistService.GetCustomizeDetail(tempId);
        }

        public ApiResult<Component> GetAllComponents()
        {
            return _checklistService.GetAllComponents();
        }

        public ApiResult<Template> DeleteChecklistTemplate(int tempId)
        {
            return _tempService.DeleteChecklistTemplate(tempId);
        }

        public ApiResult<ImportModel> CreateNew(ImportModel model)
        {
            return _checklistService.CreateNew(model);
        }

        public ApiResult<Item> UpdateItem(Item model)
        {
            return _tempService.UpdateItem(model);
        }

        public ApiResult<Item> DeleteItem(int itemId)
        {
            return _tempService.DeleteItem(itemId);
        }

        public ApiResult<Template> SaveTemplate(Template model)
        {
            return _tempService.Save(model);
        }

        public ApiResult<Component> SaveConfiguration(Component model)
        {
            return _checklistService.SaveConfiguration(model);
        }

        public ApiResult<Component> DeleteComponent(int id)
        {
            return _checklistService.DeleteComponent(id);
        }

        public ApiResult<Template> GetTemplateDetail(int templateId)
        {
            return _tempService.GetById(templateId);
        }

        public ApiResult<TemplateDetail> GetTemplateBasicInfomation(int tempid)
        {
            return _checklistService.GetTemplateBasicInfomation(tempid);
        }

        public ApiResult<Checklist> CreateChecklist(CreateChecklistModel model)
        {
            return _checklistService.CreateChecklist(model);
        }
    }
}
