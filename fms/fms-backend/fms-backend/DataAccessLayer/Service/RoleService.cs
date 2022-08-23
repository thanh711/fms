using Dapper;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccessLayer.Service
{
    public class RoleService : IRoleService
    {
        private string _cnnString;
        private readonly IConfiguration _configuration;
        private BaseService<Role> _baseService;
        public RoleService(IConfiguration configuration)
        {
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DB");
            _baseService = new BaseService<Role>(configuration);
        }
        
        public ApiResult<Role> GetAll()
        {
            string query = "select * from [Configuration.Roles]";
            return _baseService.GetAll(query);
        }
    }
}
