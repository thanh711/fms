using Dapper;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IConfiguration _configuration;
        private BaseService<Category> _baseService;
        
        public CategoryService(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseService = new BaseService<Category>(configuration);
           
        }
        public ApiResult<Category> Delete(int id)
        {
            string query = "DELETE FROM [Configuration.Categories] WHERE ID = " + id;
            return _baseService.Delete(query);
        }

        public ApiResult<Category> GetAll()
        {
            string query = "SELECT cate.*, parCate.[Name] as ParentName " +
                "FROM[Configuration.Categories] cate " +
                "LEFT JOIN[Configuration.Categories] parCate ON cate.ParentCategory = parCate.ID";
            return _baseService.GetAll(query);
        }

        public ApiResult<Category> GetList(CategorySearchModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", model.paging.CurrentPage);
            parameters.Add("@PageSize", model.paging.PageSize);
            parameters.Add("@CategoryL1Name", model.CategoryL1Name);
            parameters.Add("@CategoryL2Name", model.CategoryL2Name);

            return _baseService.GetList(model.paging, StoredProcedure.GetListCategories, parameters);
        }

        public ApiResult<Category> Save(Category category)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ID", category.ID);
            parameters.Add("@Name", category.Name);
            parameters.Add("@ParentCategory", category.ParentCategory);
            parameters.Add("@Note", category.Note);
            parameters.Add("@CreateBy", category.CreatedBy);

            return _baseService.Save(StoredProcedure.SaveCategory, parameters);
        }

        public ApiResult<Category> GetByName(string name)
        {
            string query = "SELECT cate.*, parCate.[Name] as ParentName " +
                "FROM[Configuration.Categories] cate " +
                "LEFT JOIN[Configuration.Categories] parCate ON cate.ParentCategory = parCate.ID " +
                "WHERE cate.[Name] = N'" + name + "'";
            return _baseService.GetBy(query);
        }

    }
}
