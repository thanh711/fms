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
    public class ChecklistItemService
    {
        private readonly BaseService<Item> _baseService;

        public ChecklistItemService(IConfiguration configuration)
        {
            _baseService = new BaseService<Item>(configuration);
        }

        public bool DeleteAllItem(int? id)
        {
            if (id != null)
            {
                string query = "DELETE FROM [Checklists.Items] WHERE [ComponentID] = " + id;
                var res = _baseService.Delete(query);
                return res.Status == 200;
            }
            return true;
        }

        public bool Create(Item item)
        {
            string query = "INSERT INTO [Checklists.Items]([ComponentID],[Requirements],[DefaultValue],[Name]) " +
                "VALUES(" + item.ComponentID + ", N'" + item.Requirements + "'," + (item.DefaultValue ? 1 : 0) + ", N'" + item.Name + "')";
            return _baseService.SaveByQuery(query).Status == 200;
        }

        public List<Item> GetByID(int templateId)
        {
            string query = "SELECT * FROM [Checklists.Items] WHERE ComponentID = " + templateId;
            return _baseService.GetAll(query).ListData;
        }

        public ApiResult<Item> UpdateItem(Item model)
        {
            string query = "UPDATE [Checklists.Items] SET [Requirements] = N'" + model.Requirements + "'," +
                "[DefaultValue] = " + (model.DefaultValue ? 1 : 0) + ", [Name] = N'" + model.Name + "' WHERE ID = " + model.ID;
            return _baseService.SaveByQuery(query);
        }

        public ApiResult<Item> DeleteItem(int itemId)
        {
            string query = "DELETE FROM [Checklists.Items] WHERE ID = " + itemId;
            return _baseService.Delete(query);
        }

        public ApiResult<Item> GetListItem(int componentId, string checklistId)
        {
            string query = "SELECT I.ID AS ID, I.[Name] AS Name, I.Requirements, CASE WHEN R.[Status] = 1 THEN 'OK' " +
                "WHEN R.[Status] = 0 THEN 'NOK' ELSE 'OK' END AS Status, R.NOK, R.Note " +
                "FROM [Checklists.Checklists] CK " +
                "INNER JOIN [Checklists.Templates] T ON CK.TemplateID = T.ID " +
                "INNER JOIN [Checklists.Components] C ON T.ID = C.TemplateID " +
                "INNER JOIN [Checklists.Items] I ON C.ID = I.ComponentID " +
                "LEFT JOIN [Checklists.Results] R ON I.ID = R.ItemID AND CK.ID = R.ChecklistID " +
                "WHERE CK.ID = '" + checklistId + "' AND C.ID = " + componentId;
            return _baseService.GetAll(query);
        }
    }
}
