using DataAccessLayer.Model.Checklist;
using DataAccessLayer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IService
{
    public interface IChecklistTemplateService
    {
        ApiResult<Template> GetTemplateByCampus(string campus);
        ApiResult<Template> DeleteChecklistTemplate(int tempId);
        ApiResult<Item> UpdateItem(Item model);
        ApiResult<Item> DeleteItem(int itemId);
        ApiResult<Template> Save(Template model);
        ApiResult<Template> GetById(int templateId);

    }
}
