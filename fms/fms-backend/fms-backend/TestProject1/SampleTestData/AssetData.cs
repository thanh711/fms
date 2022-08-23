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
    internal class AssetData
    {
        private string _cnnString;
        private readonly IConfiguration _configuration;
        private BaseService<Asset> _baseService;
        public AssetData(IConfiguration configuration)
        {
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DB");
            _baseService = new BaseService<Asset>(configuration);
        }
        public void ClearTestData()
        {
            string query = "delete from [Configuration.Assets] where [Name] like 'ThanhNC-Test%'";

            _baseService.Delete(query);
        }
        public void CreateTestData()
        {

            foreach (var item in Lists)
            {
                CreateAssetData(item);
            }
        }
        public void CreateAssetData(Asset model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ID", model.ID);
            parameters.Add("@Code", model.Code);
            parameters.Add("@Name", model.Name);
            parameters.Add("@CategoryID", model.CategoryID);
            parameters.Add("@AreaID", model.AreaID);
            parameters.Add("@MeasureID", model.MeasureUnitID);
            parameters.Add("@Quantity", model.Quantity);
            parameters.Add("@StartDate", model.StartDate);
            parameters.Add("@EndDate", model.EndDate);
            parameters.Add("@CreateBy", model.CreatedBy);

            _baseService.Save(StoredProcedure.SaveAsset, parameters);
        }
        internal static Asset model1 = new Asset
        {
            Code = "TBD001",
            CategoryID = 1,
            AreaID = 1,
            MeasureUnitID = 1,
            Name = "ThanhNC-Test",
            Quantity = 8,
            StartDate = DateTime.Parse("2022-07-24T22:05:37.757"),
            EndDate = DateTime.Parse("2022-07-24T22:05:37.757"),
            InService = true,
            CreatedBy = "thanhnche140350",
        };
        internal static Asset model2 = new Asset
        {
            Code = "TBD002",
            CategoryID = 1,
            AreaID = 1,
            MeasureUnitID = 1,
            Name = "ThanhNC-Test",
            Quantity = 8,
            StartDate = DateTime.Parse("2022-07-24T22:05:37.757"),
            EndDate = DateTime.Parse("2022-07-24T22:05:37.757"),
            InService = true,
            CreatedBy = "thanhnche140350",
        };
        internal static Asset model3 = new Asset
        {
            Code = "TBD003",
            CategoryID = 1,
            AreaID = 1,
            MeasureUnitID = 1,
            Name = "ThanhNC-Test",
            Quantity = 8,
            StartDate = DateTime.Parse("2022-07-24T22:05:37.757"),
            EndDate = DateTime.Parse("2022-07-24T22:05:37.757"),
            InService = true,
            CreatedBy = "thanhnche140350",
        };
        internal static Asset model4 = new Asset
        {
            Code = "TBD004",
            CategoryID = 1,
            AreaID = 1,
            MeasureUnitID = 1,
            Name = "ThanhNC-Test",
            Quantity = 8,
            StartDate = DateTime.Parse("2022-07-24T22:05:37.757"),
            EndDate = DateTime.Parse("2022-07-24T22:05:37.757"),
            InService = true,
            CreatedBy = "thanhnche140350",
        };
        internal static Asset model5 = new Asset
        {
            Code = "TBD005",
            CategoryID = 1,
            AreaID = 5,
            MeasureUnitID = 1,
            Name = "ThanhNC-Test",
            Quantity = 8,
            StartDate = DateTime.Parse("2022-07-24T22:05:37.757"),
            EndDate = DateTime.Parse("2022-07-24T22:05:37.757"),
            InService = true,
            CreatedBy = "thanhnche140350",
        };
        internal static List<Asset> Lists = new List<Asset> { model1, model2, model3, model4, model5 };

    }
}
