using DataAccessLayer.IService;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using DataAccessLayer.Model.Troubles;
using DataAccessLayer.Service;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.SampleTestData;

namespace TestProject1.DataAccessLayerTest
{
    internal class TroubleServiceTest
    {
        private ITroubleService _service;
        private TroubleData _data;
        [SetUp]
        public void SetUp()
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .AddEnvironmentVariables()
                                    .Build();
            _service = new TroubleService(config);
            _data = new TroubleData(config);
        }
        [Test]
        public void testCreateTrouble()
        {
            try
            {//create data
             //test
                {
                    ApiResult<Trouble> expected = new ApiResult<Trouble>
                    {
                        Status = 200,
                        Data = TroubleData.model1,
                        Message = "Create trouble successfully."
                    };
                    var actual = _service.CreateTrouble(TroubleData.model1);
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Message, Is.EqualTo(expected.Message));
                    Assert.That(actual.Data.Report.Summary, Is.EqualTo(expected.Data.Report.Summary));
                }
                {
                    ApiResult<Trouble> expected = new ApiResult<Trouble>
                    {
                        Status = 200,
                        Data = TroubleData.model2,
                        Message = "Create trouble successfully."
                    };
                    var actual = _service.CreateTrouble(TroubleData.model2);
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Message, Is.EqualTo(expected.Message));
                    Assert.That(actual.Data.Report.Summary, Is.EqualTo(expected.Data.Report.Summary));
                }
                {
                    ApiResult<Trouble> expected = new ApiResult<Trouble>
                    {
                        Status = 200,
                        Data = TroubleData.model3,
                        Message = "Create trouble successfully."
                    };
                    var actual = _service.CreateTrouble(TroubleData.model3);
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Message, Is.EqualTo(expected.Message));
                    Assert.That(actual.Data.Report.Summary, Is.EqualTo(expected.Data.Report.Summary));
                }
            }
            finally
            { //clear data
                _data.ClearTestData();
            }
        }
        [Test]
        public void testGetList()
        {
            try
            {//create data
                _data.CreateTestData();
                //test
                {
                    Paging paging = CommonData.paging1;
                    TroubleSearchModel model = new TroubleSearchModel
                    {
                        RoleID = 1,
                        Campus = "FU-HL",
                        User = "thanhnche140350",
                        paging = paging
                    };
                    var actual = _service.GetList(model);
                    List<TroubleListModel> listdata = actual.ListData.Where(a => a.Summary.Equals("ThanhNC-Test")).ToList();
                    ApiResult<Trouble> expected = new ApiResult<Trouble>
                    {
                        Status = 200,
                        ListData = TroubleData.Lists.Where(a => a.Report.CreatedBy.Equals("thanhnche140350")).ToList(),
                        Paging = paging,
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(listdata.Count, Is.EqualTo(expected.ListData.Count));
                    for (int i = 0; i < listdata.Count; i++)
                    {
                        Assert.That(listdata[i].Description, Is.EqualTo(expected.ListData[i].Report.Description));
                        Assert.That(listdata[i].Summary, Is.EqualTo(expected.ListData[i].Report.Summary));
                    }
                }
                {
                    Paging paging = CommonData.paging2;
                    TroubleSearchModel model = new TroubleSearchModel
                    {
                        RoleID = 1,
                        Campus = "FU-HL",
                        User = "thanhnche140350",
                        paging = paging
                    };
                    var actual = _service.GetList(model);
                    List<TroubleListModel> listdata = actual.ListData.Where(a => a.Summary.Equals("ThanhNC-Test")).ToList();
                    ApiResult<Trouble> expected = new ApiResult<Trouble>
                    {
                        Status = 200,
                        ListData = TroubleData.Lists.Take(4).ToList(),
                        Paging = paging,
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(listdata.Count, Is.EqualTo(expected.ListData.Count));
                    for (int i = 0; i < listdata.Count; i++)
                    {
                        Assert.That(listdata[i].Description, Is.EqualTo(expected.ListData[i].Report.Description));
                        Assert.That(listdata[i].Summary, Is.EqualTo(expected.ListData[i].Report.Summary));
                    }
                }
                {
                    Paging paging = CommonData.paging4;
                    TroubleSearchModel model = new TroubleSearchModel
                    {
                        RoleID = 1,
                        Campus = "FU-HL",
                        User = "Vuongpthe140353",
                        paging = paging
                    };
                    var actual = _service.GetList(model);
                    List<TroubleListModel> listdata = actual.ListData.Where(a => a.Summary.Equals("ThanhNC-Test")).ToList();
                    ApiResult<Trouble> expected = new ApiResult<Trouble>
                    {
                        Status = 200,
                        ListData = TroubleData.Lists.Skip(3).Take(1).ToList(),
                        Paging = paging,
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(listdata.Count, Is.EqualTo(expected.ListData.Count));
                    for (int i = 0; i < listdata.Count; i++)
                    {
                        Assert.That(listdata[i].Description, Is.EqualTo(expected.ListData[i].Report.Description));
                        Assert.That(listdata[i].Summary, Is.EqualTo(expected.ListData[i].Report.Summary));
                    }
                }
                {
                    Paging paging = CommonData.paging4;
                    TroubleSearchModel model = new TroubleSearchModel
                    {
                        RoleID = 4,
                        Campus = "FU-HL",
                        User = "thanhnche140350",
                        paging = paging
                    };
                    var actual = _service.GetList(model);
                    List<TroubleListModel> listdata = actual.ListData.Where(a => a.Summary.Equals("ThanhNC-Test")).ToList();
                    ApiResult<Trouble> expected = new ApiResult<Trouble>
                    {
                        Status = 200,
                        ListData = TroubleData.Lists.Where(a => a.Report.CreatedBy.Equals("thanhnche140350")).ToList(),
                        Paging = paging,
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(listdata.Count, Is.EqualTo(expected.ListData.Count));
                    for (int i = 0; i < listdata.Count; i++)
                    {
                        Assert.That(listdata[i].Description, Is.EqualTo(expected.ListData[i].Report.Description));
                        Assert.That(listdata[i].Summary, Is.EqualTo(expected.ListData[i].Report.Summary));
                    }
                }
            }
            finally
            {//clear data
                _data.ClearTestData();
            }
        }
        [Test]
        public void testGetByID()
        {
            try
            {//create data
                _data.CreateItem(TroubleData.model1);
                //test
                Paging paging = CommonData.paging1;
                TroubleSearchModel searchmodel = new TroubleSearchModel
                {
                    RoleID = 1,
                    Campus = "FU-HL",
                    User = "thanhnche140350",
                    paging = paging
                };
                var searchactual = _service.GetList(searchmodel);
                TroubleListModel data = searchactual.ListData.Where(a => a.Summary.Equals("ThanhNC-Test")).First();
                int reportID = data.ReportID;
                {

                    var actual = _service.GetByID(reportID);
                    ApiResult<Trouble> expected = new ApiResult<Trouble>
                    {
                        Status = 200,
                        Data = TroubleData.model1
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Data.Report.CreatedBy, Is.EqualTo(expected.Data.Report.CreatedBy));
                    Assert.That(actual.Data.Report.Description, Is.EqualTo(expected.Data.Report.Description));
                    Assert.That(actual.Data.Report.Summary, Is.EqualTo(expected.Data.Report.Summary));
                }
            }
            finally
            {
                _data.ClearTestData();
            }
        }
        [Test]
        public void ChangeTechnician()
        {
            try
            {
                //create data
                _data.CreateItem(TroubleData.model4);
                //test
                Paging paging = CommonData.paging1;
                TroubleSearchModel searchmodel = new TroubleSearchModel
                {
                    RoleID = 1,
                    Campus = "FU-HL",
                    User = "Vuongpthe140353",
                    paging = paging
                };
                var searchactual = _service.GetList(searchmodel);
                TroubleListModel data = searchactual.ListData.Where(a => a.Summary.Equals("ThanhNC-Test")).First();
                int reportID = data.ReportID;
                {
                    TroubleListModel model = new TroubleListModel
                    {
                        ReportID = reportID,
                        Technician = "Vuongpthe140353",
                        CreatedBy = "thanhnche140350",
                    };
                    var actual = _service.ChangeTechnician(model);
                    ApiResult<TroubleListModel> expected = new ApiResult<TroubleListModel>
                    {
                        Status = 200,
                        Message = "Save successfully.",
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Message, Is.EqualTo(expected.Message));
                }
                {
                    TroubleListModel model = new TroubleListModel
                    {
                        ReportID = reportID,
                        Technician = "thanhnche140350",
                        CreatedBy = "thanhnche140350",
                    };
                    var actual = _service.ChangeTechnician(model);
                    ApiResult<TroubleListModel> expected = new ApiResult<TroubleListModel>
                    {
                        Status = 200,
                        Message = "Save successfully.",
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Message, Is.EqualTo(expected.Message));
                }
            }
            finally
            {
                //clear data
                _data.ClearTestData();
            }
        }
        [Test]
        public void testCancelReport()
        {

            try
            {
                //create data
                _data.CreateItem(TroubleData.model1);
                //test
                Paging paging = CommonData.paging1;
                TroubleSearchModel searchmodel = new TroubleSearchModel
                {
                    RoleID = 1,
                    Campus = "FU-HL",
                    User = "thanhnche140350",
                    paging = paging
                };
                var searchactual = _service.GetList(searchmodel);
                TroubleListModel data = searchactual.ListData.Where(a => a.Summary.Equals("ThanhNC-Test")).First();
                int reportID = data.ReportID;
                ApiResult<Trouble> expected = new ApiResult<Trouble>
                {
                    Status = 200,
                    Message = "Save successfully."
                };
                var actual = _service.CancelReport(reportID);
                Assert.That(actual.Status, Is.EqualTo(expected.Status));
                Assert.That(actual.Message, Is.EqualTo(expected.Message));

            }
            finally
            {
                //clear data
                _data.ClearTestData();
            }
        }
        [Test]
        public void testDeleteReport()
        {
            try
            {//create data
                _data.CreateItem(TroubleData.model1);
                //test
                Paging paging = CommonData.paging1;
                TroubleSearchModel searchmodel = new TroubleSearchModel
                {
                    RoleID = 1,
                    Campus = "FU-HL",
                    User = "thanhnche140350",
                    paging = paging
                };
                var searchactual = _service.GetList(searchmodel);
                TroubleListModel data = searchactual.ListData.Where(a => a.Summary.Equals("ThanhNC-Test")).First();
                int reportID = data.ReportID;

                ApiResult<Trouble> expected = new ApiResult<Trouble>
                {
                    Status = 200,
                    Message ="Save successfully." //"Delete successfully."
                };
                var actual = _service.DeleteReport(reportID);
                Assert.That(actual.Status, Is.EqualTo(expected.Status));
                Assert.That(actual.Message, Is.EqualTo(expected.Message));
            }
            finally
            {
                //clear data
                _data.ClearTestData();
            }
        }
        [Test]
        public void testUpdateTrouble()
        {

            try
            {//create data
                Trouble trouble = TroubleData.model1;
                _data.CreateItem(trouble);
                //test
                Paging paging = CommonData.paging1;
                TroubleSearchModel searchmodel = new TroubleSearchModel
                {
                    RoleID = 1,
                    Campus = "FU-HL",
                    User = "thanhnche140350",
                    paging = paging
                };
                var searchactual = _service.GetList(searchmodel);
                TroubleListModel data = searchactual.ListData.Where(a => a.Summary.Equals("ThanhNC-Test")).First();
                int reportID = data.ReportID;
                TroubleListModel model = new TroubleListModel
                {
                    ReportID = reportID,
                    Technician = "thanhnche140350",
                    CreatedBy = "thanhnche140350",
                };
                _service.ChangeTechnician(model);
                {
                    trouble.Shooting = TroubleData.troubleShooting;
                    trouble.Shooting.ReportID = reportID;
                    trouble.ShootImage = new List<Image> { TroubleData.img2 };
                    ApiResult<Trouble> expected = new ApiResult<Trouble>
                    {
                        Status = 200,
                        Message = "Save troubleshooting successfully."
                    };
                    var actual = _service.UpdateTrouble(trouble);
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Message, Is.EqualTo(expected.Message));
                }
                {
                    trouble.Shooting = TroubleData.troubleShooting;
                    trouble.Shooting.Priority = 1;
                    trouble.Shooting.ReportID = reportID;
                    trouble.ShootImage = new List<Image> { TroubleData.img2 };
                    ApiResult<Trouble> expected = new ApiResult<Trouble>
                    {
                        Status = 200,
                        Message = "Save troubleshooting successfully."
                    };
                    var actual = _service.UpdateTrouble(trouble);
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Message, Is.EqualTo(expected.Message));
                }
                {
                    trouble.Shooting = TroubleData.troubleShooting;
                    trouble.Shooting.Solution = "buy new";
                    trouble.Shooting.ReportID = reportID;
                    trouble.ShootImage = new List<Image> { TroubleData.img2 };
                    ApiResult<Trouble> expected = new ApiResult<Trouble>
                    {
                        Status = 200,
                        Message = "Save troubleshooting successfully."
                    };
                    var actual = _service.UpdateTrouble(trouble);
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Message, Is.EqualTo(expected.Message));
                }

            }
            finally
            {
                //clear data
                _data.ClearTestData();
            }
        }
        [Test]
        public void testCountReports()
        {

            try
            {//create data
                _data.CreateTestData();
                //test
                Paging paging = CommonData.paging1;
                TroubleSearchModel model = new TroubleSearchModel
                {
                    RoleID = 1,
                    Campus = "FU-HL",
                    User = "thanhnche140350",
                    paging = paging
                };
                var actual = _service.CountReports(model);
                ApiResult<CountModel> expected = new ApiResult<CountModel>
                {
                    Status = 200,
                    ListData = new List<CountModel> { new CountModel { StepName = "Cancel", Quantity = 1 }, new CountModel { StepName = "Done", Quantity = 1 }, new CountModel { StepName = "Up Work", Quantity = 1 }, },
                    Paging = paging,
                };
                Assert.That(actual.Status, Is.EqualTo(expected.Status));
                Assert.That(actual.ListData.Count, Is.EqualTo(expected.ListData.Count));

            }
            finally
            {//clear data
                _data.ClearTestData();
            }
        }
    }
}
