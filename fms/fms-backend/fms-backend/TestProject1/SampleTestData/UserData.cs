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
    internal class UserData
    {
        private string _cnnString;
        private readonly IConfiguration _configuration;
        private BaseService<User> _baseService;
        public UserData(IConfiguration configuration)
        {
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DB");
            _baseService = new BaseService<User>(configuration);
        }
        public ApiResult<User> ClearTestData()
        {
            string query = "delete from [Configuration.Users] where [Username] like 'ThanhNC-Test%'";

            return _baseService.Delete(query);
        }
        public void CreateTestData()
        {
            foreach (User item in Lists)
            {
                CreateItem(item);
            }
        }
        public ApiResult<User> CreateItem(User model)
        {
            var parameters = new DynamicParameters();
            string username = model.UserName;
            if (string.IsNullOrEmpty(username))
            {
                username = model.Email.Substring(0, model.Email.IndexOf("@"));
            }
            parameters.Add("@Username", username);
            parameters.Add("@CampusID", model.CampusID);
            parameters.Add("@Email", model.Email);
            parameters.Add("@RoleID", model.RoleID);
            parameters.Add("@CreateBy", model.CreatedBy);

            return _baseService.Save(StoredProcedure.SaveUser, parameters);
        }
        internal static User model1 = new User
        {
            CampusID = 1,
            UserName = "ThanhNC-Test-u1",
            Email = "ThanhNC-Test-u1@fpt.edu.vn",
            RoleID = 1,
            CreatedBy = "thanhnche140350",
        };
        internal static User model2 = new User
        {
            CampusID = 1,
            UserName = "ThanhNC-Test-u2",
            Email = "ThanhNC-Test-u1@fpt.edu.vn",
            RoleID = 2,
            CreatedBy = "thanhnche140350",
        };
        internal static User model3 = new User
        {
            CampusID = 1,
            UserName = "ThanhNC-Test-u3",
            Email = "ThanhNC-Test-u1@fpt.edu.vn",
            RoleID = 3,
            CreatedBy = "thanhnche140350",
        };
        internal static User model4 = new User
        {
            CampusID = 1,
            UserName = "ThanhNC-Test-u4",
            Email = "ThanhNC-Test-u1@fpt.edu.vn",
            RoleID = 4,
            CreatedBy = "thanhnche140350",
        };
        internal static User model5 = new User
        {
            CampusID = 1,
            UserName = "ThanhNC-Test-u5",
            Email = "ThanhNC-Test-u1@fpt.edu.vn",
            RoleID = 1,
            CreatedBy = "thanhnche140350",
        };
        internal static List<User> Lists = new List<User> { model1, model2, model3, model4, model5 };

    }
}
