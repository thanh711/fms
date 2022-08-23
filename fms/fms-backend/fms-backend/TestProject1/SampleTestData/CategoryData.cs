using Dapper;
using DataAccessLayer;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.SampleTestData
{
    internal class CategoryData
    {
        private string _cnnString;
        private readonly IConfiguration _configuration;
        private BaseService<Category> _baseService;
        public CategoryData(IConfiguration configuration)
        {
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DB");
            _baseService = new BaseService<Category>(configuration);
        }
        public ApiResult<Category> ClearTestData()
        {
            string query = "delete from [Configuration.Categories] where [Name] like 'ThanhNC-Test%'";

            return _baseService.Delete(query);
        }
        public void CreateTestData()
        {
            foreach (Category item in Lists)
            {
                CreateItem(item);
            }
        }
        public ApiResult<Category> CreateItem(Category model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ID", model.ID);
            parameters.Add("@Name", model.Name);
            parameters.Add("@ParentCategory", model.ParentCategory);
            parameters.Add("@CreateBy", model.CreatedBy);
            parameters.Add("@Note", model.Note);
            return _baseService.Save(StoredProcedure.SaveCategory, parameters);
        }
        internal static Category model1 = new Category
        {
            Name = "ThanhNC-Test",
            CreatedBy = "thanhnche140350",
            Note= "ThanhNC-Test-note1",
        };
        internal static Category model2 = new Category
        {
            Name = "ThanhNC-Test",
            CreatedBy = "thanhnche140350",
            Note = "ThanhNC-Test-note2",
        };
        internal static Category model3 = new Category
        {
            Name = "ThanhNC-Test",
            CreatedBy = "thanhnche140350",
            Note = "ThanhNC-Test-note3",
        };
        internal static Category model4 = new Category
        {
            Name = "ThanhNC-Test",
            CreatedBy = "thanhnche140350",
            Note = "ThanhNC-Test-note4",
        };
        internal static Category model5 = new Category
        {
            Name = "ThanhNC-Test",
            CreatedBy = "thanhnche140350",
            Note = "ThanhNC-Test-note5",
        };

        internal static List<Category> Lists = new List<Category> { model1, model2, model3, model4, model5 };

    }
}
