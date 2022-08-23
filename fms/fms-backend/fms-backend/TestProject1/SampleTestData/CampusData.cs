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
    internal class CampusData
    {
        private string _cnnString;
        private readonly IConfiguration _configuration;
        private BaseService<Campus> _baseService;
        public CampusData(IConfiguration configuration)
        {
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DB");
            _baseService = new BaseService<Campus>(configuration);
        }
        public void ClearTestData()
        {
            string query = "delete from [Configuration.Campuses] where [Name] like 'ThanhNC-Test%'";

            _baseService.Delete(query);
        }
        public void CreateTestData()
        {

            foreach (var item in Lists)
            {
                CreateTestItem(item);
            }
        }
        public void CreateTestItem(Campus model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ID", model.ID);
            parameters.Add("@Name", model.Name);
            parameters.Add("@FullName", model.FullName);
            parameters.Add("@Address", model.Address);
            parameters.Add("@Telephone", model.Telephone);
            parameters.Add("@CreateBy", model.CreatedBy);

             _baseService.Save(StoredProcedure.SaveCampus, parameters);
        }
        internal static Campus model1 = new Campus
        {
            Name = "ThanhNC-Test",
            FullName = "ThanhNC-Test-FU-Hòa Lạc",
            Address = "ThanhNC-Test-khu cnc",
            Telephone = "0961592144",
            CreatedBy = "thanhnche140350",
        };
        internal static Campus model2 = new Campus
        {
            Name = "ThanhNC-Test",
            FullName = "ThanhNC-Test-FU-Hô chí minh",
            Address = "ThanhNC-Test-q9",
            Telephone = "0961592144",
            CreatedBy = "thanhnche140350",
        };
        internal static Campus model3 = new Campus
        {
            Name = "ThanhNC-Test",
            FullName = "ThanhNC-Test-FU-Đà Nẵng",
            Address = "ThanhNC-Test-khu dn",
            Telephone = "0961592144",
            CreatedBy = "thanhnche140350",
        };
        internal static Campus model4 = new Campus
        {
            Name = "ThanhNC-Test",
            FullName = "ThanhNC-Test-FU-Cần Thơ",
            Address = "ThanhNC-Test-khu ct",
            Telephone = "0961592144",
            CreatedBy = "thanhnche140350",
        };
        internal static Campus model5 = new Campus
        {
            Name = "ThanhNC-Test",
            FullName = "ThanhNC-Test-FU-Quy nhơm",
            Address = "ThanhNC-Test-khu qn",
            Telephone = "0961592144",
            CreatedBy = "thanhnche140350",
        };
        internal static List<Campus> Lists = new List<Campus> { model1, model2, model3, model4, model5 };

    }
}
