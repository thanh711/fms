using DataAccessLayer;
using DataAccessLayer.Model.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.SampleTestData
{
    internal class RoleData
    {
        private string _cnnString;
        private readonly IConfiguration _configuration;
        private BaseService<Role> _baseService;
        public RoleData(IConfiguration configuration)
        {
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DB");
            _baseService = new BaseService<Role>(configuration);
        }
        
        internal static Role model1 = new Role
        {
            ID = 1,
            Name = "User",
        };
        internal static Role model2 = new Role
        {
            ID = 2,
            Name = "Admin",
        };
        internal static Role model3 = new Role
        {
            ID = 3,
            Name = "Manager",
        };
        internal static Role model4 = new Role
        {
            ID = 4,
            Name = "Technician",
        };
        internal static List<Role> Lists = new List<Role> { model1, model2, model3, model4 };

    }
}
