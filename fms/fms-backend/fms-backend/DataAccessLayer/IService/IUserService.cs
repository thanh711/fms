using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IService
{
    public interface IUserService
    {
        ApiResult<User> GetUserByEmail(string email);
        ApiResult<User> GetAll();
        ApiResult<User> Save(User model);
        ApiResult<User> GetList(UserSearchModel model);
        ApiResult<User> Delete(string username);
        ApiResult<User> ChangeInService(User model);
        ApiResult<User> GetAllByRole(string role);
    }
}
