using BusinessLogicLayer.IBLLService;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLLService
{
    public class UserBLLService : IUserBLLService
    {
        IUserService _userService;
        public UserBLLService(IUserService userService)
        {
            _userService = userService;
        }

        public ApiResult<User> ChangeInService(User model)
        {
            return _userService.ChangeInService(model);
        }

        public ApiResult<User> Delete(string username)
        {
            return _userService.Delete(username);
        }

        public ApiResult<User> GetAll()
        {
            return _userService.GetAll();
        }

        public ApiResult<User> GetAllByRole(string role)
        {
            return _userService.GetAllByRole(role);
        }

        public ApiResult<User> GetList(UserSearchModel model)
        {
            return _userService.GetList(model);
        }

        public ApiResult<User> GetUserByEmail(string email)
        {
            return _userService.GetUserByEmail(email);
        }

        public ApiResult<User> Save(User model)
        {
            return _userService.Save(model);
        }

    }
}
