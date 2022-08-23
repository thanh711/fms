using DataAccessLayer;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Troubles;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.SampleTestData
{
    internal class ImageData
    {
        private string _cnnString;
        private readonly IConfiguration _configuration;
        private BaseService<Image> _baseService;
        public ImageData(IConfiguration configuration)
        {
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DB");
            _baseService = new BaseService<Image>(configuration);
        }
        public ApiResult<Image> ClearTestData()
        {
            string query = "delete from [Trouble.Images] where [ID] like '%ThanhNC-Test%'";

            return _baseService.Delete(query);
        }
        public void CreateTestData()
        {
            foreach (Image item in Lists)
            {
                CreateItem(item);
            }
        }
        public ApiResult<Image> CreateItem(Image model)
        {
            string query = "INSERT INTO [Trouble.Images] ([ID],[ReportID],[Path]) " +
                "VALUES ('" + model.ReportID + "_" + model.ID + "'," + model.ReportID + ",'" + model.Path + "')";
            return _baseService.Save(query);
        }
        internal static Image model1 = new Image
        {
            ReportID = 1,
            ID = "ThanhNC-Test-AL-101L",
            Path = "ThanhNC-Test-img",
        };
        internal static Image model2 = new Image
        {
            ReportID = 1,
            ID = "ThanhNC-Test-AL-101L1",
            Path = "ThanhNC-Test-img",
        };
        internal static Image model3 = new Image
        {
            ReportID = 1,
            ID = "ThanhNC-Test-AL-101L3",
            Path = "ThanhNC-Test-img",
        };
        internal static Image model4 = new Image
        {
            ReportID = 1,
            ID = "ThanhNC-Test-AL-1014",
            Path = "ThanhNC-Test-img",
        };
        internal static Image model5 = new Image
        {
            ReportID = 1,
            ID = "ThanhNC-Test-AL-101L5",
            Path = "ThanhNC-Test-img",
        };

        internal static List<Image> Lists = new List<Image> { model1, model2, model3, model4, model5 };

    }
}
