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
    public class AreaData
    {

        private string _cnnString;
        private readonly IConfiguration _configuration;
        private BaseService<Area> _baseService;
        public AreaData(IConfiguration configuration)
        {
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DB");
            _baseService = new BaseService<Area>(configuration);
        }
        public ApiResult<Area> ClearTestData()
        {
            string query = "delete from [Configuration.Areas] where [Name] like 'ThanhNC-Test%'";

            return _baseService.Delete(query);
        }
        public void CreateTestData()
        {
            foreach (Area item in Lists)
            {
                CreateItem(item);
            }
        }
        public void CreateItem(Area model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ID", model.ID);
            parameters.Add("@Name", model.Name);
            parameters.Add("@FullName", model.FullName);
            parameters.Add("@LocationID", model.LocationID);
            parameters.Add("@CreateBy", model.CreatedBy);

             _baseService.Save(StoredProcedure.SaveArea, parameters);
        }
        internal static Area model1 = new Area
        {
            LocationID = 1,
            Name = "ThanhNC-Test",
            FullName = "Phòng AL-101L",
            CampusName = "FU-HL",
            InService = true,
            CreatedBy = "thanhnche140350",
        };
        internal static Area model2 = new Area
        {
            LocationID = 1,
            Name = "ThanhNC-Test",
            FullName = "Phòng AL-101R",
            CampusName = "FU-HL",
            InService = true,
            CreatedBy = "thanhnche140350",
        };
        internal static Area model3 = new Area
        {
            LocationID = 1,
            Name = "ThanhNC-Test",
            FullName = "Phòng AL-102L",
            CampusName = "FU-HL",
            InService = true,
            CreatedBy = "thanhnche140350",
        };
        internal static Area model4 = new Area
        {
            LocationID = 1,
            Name = "ThanhNC-Test",
            FullName = "Phòng AL-102R",
            CampusName = "FU-HL",
            InService = true,
            CreatedBy = "thanhnche140350",
        };
        internal static Area model5 = new Area
        {
            LocationID = 3,
            Name = "ThanhNC-Test",
            FullName = "Phòng BE-217",
            CampusName = "FU-HL",
            InService = true,
            CreatedBy = "thanhnche140350",
        };
        
        internal static List<Area> Lists =new List<Area> { model1, model2, model3, model4, model5 };
       
    }
}
