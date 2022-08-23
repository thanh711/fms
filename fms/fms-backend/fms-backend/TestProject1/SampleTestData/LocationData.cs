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
    internal class LocationData
    {
        private string _cnnString;
        private readonly IConfiguration _configuration;
        private BaseService<Location> _baseService;
        public LocationData(IConfiguration configuration)
        {
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DB");
            _baseService = new BaseService<Location>(configuration);
        }
        public void ClearTestData()
        {
            string query = "delete from [Configuration.Locations] where [Name] like 'ThanhNC-Test%'";

            _baseService.Delete(query);
        }
        public void CreateTestData()
        {

            foreach (var item in Lists)
            {
                CreateItem(item);
            }
        }
        public ApiResult<Location> CreateItem(Location model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ID", model.ID);
            parameters.Add("@Name", model.Name);
            parameters.Add("@FullName", model.FullName);
            parameters.Add("@Code", model.Code);
            parameters.Add("@CampusID", model.CampusID);
            parameters.Add("@CreateBy", model.CreatedBy);

            return _baseService.Save(StoredProcedure.SaveLocation, parameters);
        }
        internal static Location model1 = new Location
        {
            Name = "ThanhNC-Test",
            FullName = "ThanhNC-Test-Alpha",
            Code = "ThanhNC-Test",
            CampusID = 1,
            CreatedBy = "thanhnche140350",
        };
        internal static Location model2 = new Location
        {
            Name = "ThanhNC-Test",
            FullName = "ThanhNC-Test-Beta",
            Code = "ThanhNC-Test",
            CampusID = 1,
            CreatedBy = "thanhnche140350",
        };
        internal static Location model3 = new Location
        {
            Name = "ThanhNC-Test",
            FullName = "ThanhNC-Test-Delta",
            Code = "ThanhNC-Test",
            CampusID = 1,
            CreatedBy = "thanhnche140350",
        };
        internal static Location model4 = new Location
        {
            Name = "ThanhNC-Test",
            FullName = "ThanhNC-Test-ktxA",
            Code = "ThanhNC-Test",
            CampusID = 1,
            CreatedBy = "thanhnche140350",
        };
        internal static Location model5 = new Location
        {
            Name = "ThanhNC-Test",
            FullName = "ThanhNC-Test-ktxB",
            Code = "ThanhNC-Test",
            CampusID = 1,
            CreatedBy = "thanhnche140350",
        };
        internal static List<Location> Lists = new List<Location> { model1, model2, model3, model4, model5 };

    }
}
