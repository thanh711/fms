using Dapper;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Service
{
    public class UserService : IUserService
    {
        private string _cnnString;
        private readonly IConfiguration _configuration;
        private BaseService<User> _baseService;
        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DB");
            _baseService = new BaseService<User>(configuration);
        }

        public ApiResult<User> GetAll()
        {
            string query = "select * from [Configuration.Users]";
            return _baseService.GetAll(query);
        }

        public ApiResult<User> GetUserByEmail(string email)
        {
            string query = "select us.*, ro.[Name] as RoleName, ca.[Name] as CampusName " +
                "from[Configuration.Users] us " +
                "INNER JOIN[Configuration.Roles] ro ON us.RoleID = ro.ID " +
                "INNER JOIN[Configuration.Campuses] ca On us.CampusID = ca.ID where Email = '" + email + "'";
            return _baseService.GetBy(query);
        }

        public ApiResult<User> Save(User model)
        {
            var parameters = new DynamicParameters();
            string username = model.UserName;
            if (string.IsNullOrEmpty(username))
            {
                username = model.Email.Substring(0, model.Email.IndexOf("@"));
            }
            parameters.Add("@Username", username.ToLower());
            parameters.Add("@Fullname", model.FullName);
            parameters.Add("@CampusID", model.CampusID);
            parameters.Add("@Email", model.Email);
            parameters.Add("@RoleID", model.RoleID);
            parameters.Add("@CreateBy", model.CreatedBy);

            return _baseService.Save(StoredProcedure.SaveUser, parameters);
        }

        public ApiResult<User> GetList(UserSearchModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", model.paging.CurrentPage);
            parameters.Add("@PageSize", model.paging.PageSize);
            parameters.Add("@Username", model.Username);
            parameters.Add("@CampusName", model.Campus);
            parameters.Add("@Role", model.Role);

            return _baseService.GetList(model.paging, StoredProcedure.GetListUser, parameters);
        }

        public ApiResult<User> Delete(string username)
        {
            string query = "delete from [Configuration.Users] where Username = '" + username + "'";
            return _baseService.Delete(query);
        }

        public ApiResult<User> ChangeInService(User model)
        {
            string query = "UPDATE [Configuration.Users] SET IsActive = " + (model.IsActive ? 1 : 0) + " WHERE Username = '" + model.UserName + "'";
            return _baseService.ChangeActive(query);
        }

        public ApiResult<User> GetAllByRole(string role)
        {
            string query = "select us.* from [Configuration.Users] us " +
                "inner join[Configuration.Roles] ro on us.RoleID = ro.ID " +
                "where ro.[Name] = '" + role + "'";
            return _baseService.GetAll(query);
        }
    }
}
