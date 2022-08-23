using DataAccessLayer.Model.Checklist;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IService
{
    public interface IChecklistService
    {
        ApiResult<Checklist> GetList(ChecklistSearchModel model);
        ApiResult<WeeklyChecklistModel> GetWeeklyChecklist(WeeklyChecklistSearchModel model);
        ApiResult<ChecklistDetailApiResult> GetDetail(string checklistId);
        ApiResult<Checklist> SaveChecklist(ChecklistDetailApiResult model);
        ApiResult<ImportModel> SaveImport(ImportModel model);
        ApiResult<CustomizeModel> GetCustomizeDetail(int tempId);
        ApiResult<ImportModel> CreateNew(ImportModel model);
        ApiResult<Component> GetAllComponents();
        ApiResult<Component> SaveConfiguration(Component model);
        ApiResult<Component> DeleteComponent(int id);
        ApiResult<TemplateDetail> GetTemplateBasicInfomation(int tempid);
        ApiResult<Checklist> CreateChecklist(CreateChecklistModel model);
    }
}
