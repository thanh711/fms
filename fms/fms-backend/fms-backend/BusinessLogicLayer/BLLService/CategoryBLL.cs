using BusinessLogicLayer.IBLLService;
using DataAccessLayer.IService;
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
    public class CategoryBLL : ICategoryBLL
    {
        ICategoryService _categoryService;
        public CategoryBLL(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public ApiResult<Category> Delete(int id)
        {
            return _categoryService.Delete(id);
        }

        public ApiResult<Category> GetAll()
        {
            return _categoryService.GetAll();
        }

        public ApiResult<Category> GetList(CategorySearchModel model)
        {
            return _categoryService.GetList(model);
        }

        public ApiResult<Category> Save(Category category)
        {
            return _categoryService.Save(category);
        }
    }
}
