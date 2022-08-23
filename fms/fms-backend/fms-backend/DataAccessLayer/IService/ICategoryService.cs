using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IService
{
    public interface ICategoryService
    {
        ApiResult<Category> GetAll();
        ApiResult<Category> GetList(CategorySearchModel model);
        ApiResult<Category> Save(Category category);
        ApiResult<Category> Delete(int id);
    }
}
