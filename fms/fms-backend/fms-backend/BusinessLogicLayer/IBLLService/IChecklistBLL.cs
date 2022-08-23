using DataAccessLayer.Model.Checklist;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IBLLService
{
    public interface IChecklistBLL
    {
        ApiResult<Template> GetTemplateByCampus(string campus);
        ApiResult<Checklist> GetList(ChecklistSearchModel model);
        ApiResult<WeeklyChecklistModel> GetWeeklyChecklist(WeeklyChecklistSearchModel model);
        ApiResult<ChecklistDetailApiResult> GetDetail(string checklistId);
        ApiResult<Checklist> SaveChecklist(ChecklistDetailApiResult model);
        ApiResult<ImportModel> SaveImport(ImportModel model);
        ApiResult<CustomizeModel> GetCustomizeDetail(int tempId);
        ApiResult<Component> GetAllComponents();
        ApiResult<Template> DeleteChecklistTemplate(int tempId);
        ApiResult<ImportModel> CreateNew(ImportModel model);
        ApiResult<Item> UpdateItem(Item model);
        ApiResult<Item> DeleteItem(int itemId);
        ApiResult<Template> SaveTemplate(Template model);
        ApiResult<Component> SaveConfiguration(Component model);
        ApiResult<Component> DeleteComponent(int id);
        ApiResult<Template> GetTemplateDetail(int templateId);
        ApiResult<TemplateDetail> GetTemplateBasicInfomation(int tempid);
        ApiResult<Checklist> CreateChecklist(CreateChecklistModel model);
    }
}
