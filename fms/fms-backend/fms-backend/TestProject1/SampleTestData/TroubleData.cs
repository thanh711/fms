using DataAccessLayer;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Troubles;
using DataAccessLayer.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.SampleTestData
{
    internal class TroubleData
    {
        private string _cnnString;
        private readonly IConfiguration _configuration;
        private BaseService<Trouble> _baseService;
        private BaseService<TroubleListModel> _baseListService;
        private ImageService _imageService;
        private ReportService _reportService;
        private TroubleshotingService _troubleshotingService;
        public TroubleData(IConfiguration configuration)
        {
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DB");
            _baseService = new BaseService<Trouble>(configuration);
            _imageService = new ImageService(configuration);
            _reportService = new ReportService(configuration);
            _baseListService = new BaseService<TroubleListModel>(configuration);
            _troubleshotingService = new TroubleshotingService(configuration);
        }
        public ApiResult<Trouble> ClearTestData()
        {
            string query3 = "delete from [Trouble.Troubleshootings] where ReportID in(SELECT ID as ReportID from [Trouble.Reports] where [Summary] like 'ThanhNC-Test%')";

            _baseService.Delete(query3);
            string query = "delete from [Trouble.Reports] where [Summary] like 'ThanhNC-Test%'";
            string query2 = "delete from [Trouble.Images] where [Path] like 'ThanhNC-Test%'";

            _baseService.Delete(query2);

            return _baseService.Delete(query);
        }
        public void CreateTestData()
        {
            foreach (var item in Lists)
            {
                CreateItem(item);
            }
        }
        public void CreateItem(Trouble trouble)
        {
            try
            {
                Report report = _reportService.Save(trouble.Report).Data;
                if (report != null)
                {
                    foreach (Image image in trouble.ReportImage)
                    {
                        image.ReportID = report.ID;
                        if (!string.IsNullOrEmpty(image.ID))
                        {
                            if (!_imageService.CheckExist(image.ID))
                            {
                                var res = _imageService.Save(image);
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                
            }


        }
        internal static Report rp1 = new Report
        {
            AreaID = 1,
            Summary = "ThanhNC-Test",
            Description = "ThanhNC-Test-Report Description",
            Emergency = false,
            InAreaTime = DateTime.Parse("2022-07-24T22:05:37.757"),
            WorkflowID = 2,
            CreatedBy = "thanhnche140350",
        };
        internal static Report rp2 = new Report
        {
            AreaID = 1,
            Summary = "ThanhNC-Test",
            Description = "ThanhNC-Test-Report Description",
            Emergency = true,
            WorkflowID = 2,
            InAreaTime = DateTime.Parse("2022-07-24T22:05:37.757"),
            CreatedBy = "thanhnche140350",
        };
        internal static Report rp3 = new Report
        {
            AreaID = 1,
            Summary = "ThanhNC-Test",
            Description = "ThanhNC-Test-Report Description",
            Emergency = false,
            InAreaTime = DateTime.Parse("2022-07-24T22:05:37.757"),
            WorkflowID = 2,
            CreatedBy = "thanhnche140350",
        };
        internal static Report rp4 = new Report
        {
            AreaID = 1,
            Summary = "ThanhNC-Test",
            Description = "ThanhNC-Test-Report Description",
            Emergency = true,
            InAreaTime = DateTime.Parse("2022-07-24T22:05:37.757"),
            WorkflowID = 2,
            CreatedBy = "Vuongpthe140353",
        };
        internal static Report rp5 = new Report
        {
            AreaID = 5,
            Summary = "ThanhNC-Test",
            Description = "ThanhNC-Test-Report Description",
            Emergency = false,
            InAreaTime = DateTime.Parse("2022-07-24T22:05:37.757"),
            WorkflowID = 2,
            CreatedBy = "thanhnche140350",
        };
        internal static Image img1 = new Image
        {
            Path = "https://res.cloudinary.com/dqds2j3jr/image/upload/s--4q7pdf0z--/v1659986906/dzq3tj94hwzp8bad2mfn.png",
            CreatedBy = "thanhnche140350",
        };
        internal static Image img2 = new Image
        {
            Path = "https://res.cloudinary.com/dqds2j3jr/image/upload/s--947T4uIw--/v1660161132/flj9qjfgcvc8sf5hcf87.jpg",
            CreatedBy = "thanhnche140350",
        }; internal static Image img3 = new Image
        {
            Path = "https://res.cloudinary.com/dqds2j3jr/image/upload/s--nWq-mhI0--/v1660105644/xl7vv27jfkouvzn7oe1o.jpg",
            CreatedBy = "thanhnche140350",
        }; 
        internal static TroubleShooting troubleShooting = new TroubleShooting
        {
            CategoryID = 1,
        };
        internal static Trouble model1 = new Trouble
        {
            Report = rp1,
            
            ReportImage = new List<Image> { img1 }

        };
        internal static Trouble model2 = new Trouble
        {
            Report = rp2,
            ReportImage = new List<Image>{ img1 }
        };
        internal static Trouble model3 = new Trouble
        {
            Report = rp3,
            ReportImage = new List<Image> { img2 }
        };
        internal static Trouble model4 = new Trouble
        {
            Report = rp4,
            ReportImage = new List<Image> { img1, img2 }
        };
        internal static Trouble model5 = new Trouble
        {
            Report = rp5,
            ReportImage = new List<Image> { img1, img2 }
        };
        internal static List<Trouble> Lists = new List<Trouble> { model1, model2, model3, model4, model5 };

    }
}
