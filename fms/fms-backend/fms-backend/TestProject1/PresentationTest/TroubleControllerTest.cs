using BusinessLogicLayer.BLLService;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using DataAccessLayer.Model.Troubles;
using DataAccessLayer.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.SampleTestData;

namespace TestProject1.PresentationTest
{
    internal class TroubleControllerTest
    {
        private TroubleController _service;
        private TroubleData _data;
        [SetUp]
        public void SetUp()
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .AddEnvironmentVariables()
                                    .Build();
            _service = new TroubleController(null, new TroubleBLL(new TroubleService(config), new ImageService(config)));
            _data = new TroubleData(config);
        }
        [Test]
        public void testCreateTrouble()
        {
            //create data
            //test
            try
            {
                {
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Trouble>
                    {
                        Status = 200,
                        Message = "Create trouble successfully."
                    });
                    var actual = _service.CreateTrouble(TroubleData.model1);
                    var okResult = actual as OkObjectResult;
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Trouble>)okResult.Value).Message, Is.EqualTo(((ApiResult<Trouble>)expected.Value).Message));
                }
                {
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Trouble>
                    {
                        Status = 200,
                        Message = "Create trouble successfully."
                    });
                    var actual = _service.CreateTrouble(TroubleData.model2);
                    var okResult = actual as OkObjectResult;
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Trouble>)okResult.Value).Message, Is.EqualTo(((ApiResult<Trouble>)expected.Value).Message));
                }
                {
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Trouble>
                    {
                        Status = 200,
                        Message = "Create trouble successfully."
                    });
                    var actual = _service.CreateTrouble(TroubleData.model3);
                    var okResult = actual as OkObjectResult;
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Trouble>)okResult.Value).Message, Is.EqualTo(((ApiResult<Trouble>)expected.Value).Message));
                }
            }
            finally
            {
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
                    var okResult = actual as OkObjectResult;
                    List<TroubleListModel> listdata = ((ApiResult<TroubleListModel>)okResult.Value).ListData.Where(a => a.Summary.Equals("ThanhNC-Test")).ToList();
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Trouble>
                    {
                        Status = 200,
                        ListData = TroubleData.Lists.Where(a => a.Report.CreatedBy.Equals("thanhnche140350")).ToList(),
                        Paging = paging,
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(listdata.Count, Is.EqualTo(((ApiResult<Trouble>)expected.Value).ListData.Count));
                    for (int i = 0; i < listdata.Count; i++)
                    {
                        Assert.That(listdata[i].Description, Is.EqualTo(((ApiResult<Trouble>)expected.Value).ListData[i].Report.Description));
                        Assert.That(listdata[i].Summary, Is.EqualTo(((ApiResult<Trouble>)expected.Value).ListData[i].Report.Summary));
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
                    var okResult = actual as OkObjectResult;
                    List<TroubleListModel> listdata = ((ApiResult<TroubleListModel>)okResult.Value).ListData.Where(a => a.Summary.Equals("ThanhNC-Test")).ToList();
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Trouble>
                    {
                        Status = 200,
                        ListData = TroubleData.Lists.Take(4).ToList(),
                        Paging = paging,
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(listdata.Count, Is.EqualTo(((ApiResult<Trouble>)expected.Value).ListData.Count));
                    for (int i = 0; i < listdata.Count; i++)
                    {
                        Assert.That(listdata[i].Description, Is.EqualTo(((ApiResult<Trouble>)expected.Value).ListData[i].Report.Description));
                        Assert.That(listdata[i].Summary, Is.EqualTo(((ApiResult<Trouble>)expected.Value).ListData[i].Report.Summary));
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
                    var okResult = actual as OkObjectResult;
                    List<TroubleListModel> listdata = ((ApiResult<TroubleListModel>)okResult.Value).ListData.Where(a => a.Summary.Equals("ThanhNC-Test")).ToList();
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Trouble>
                    {
                        Status = 200,
                        ListData = TroubleData.Lists.Skip(3).Take(1).ToList(),
                        Paging = paging,
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(listdata.Count, Is.EqualTo(((ApiResult<Trouble>)expected.Value).ListData.Count));
                    for (int i = 0; i < listdata.Count; i++)
                    {
                        Assert.That(listdata[i].Description, Is.EqualTo(((ApiResult<Trouble>)expected.Value).ListData[i].Report.Description));
                        Assert.That(listdata[i].Summary, Is.EqualTo(((ApiResult<Trouble>)expected.Value).ListData[i].Report.Summary));
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
                    var okResult = actual as OkObjectResult;
                    List<TroubleListModel> listdata = ((ApiResult<TroubleListModel>)okResult.Value).ListData.Where(a => a.Summary.Equals("ThanhNC-Test")).ToList();
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Trouble>
                    {
                        Status = 200,
                        ListData = TroubleData.Lists.Where(a => a.Report.CreatedBy.Equals("thanhnche140350")).ToList(),
                        Paging = paging,
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(listdata.Count, Is.EqualTo(((ApiResult<Trouble>)expected.Value).ListData.Count));
                    for (int i = 0; i < listdata.Count; i++)
                    {
                        Assert.That(listdata[i].Description, Is.EqualTo(((ApiResult<Trouble>)expected.Value).ListData[i].Report.Description));
                        Assert.That(listdata[i].Summary, Is.EqualTo(((ApiResult<Trouble>)expected.Value).ListData[i].Report.Summary));
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
                var okResult2 = searchactual as OkObjectResult;
                TroubleListModel data = ((ApiResult<TroubleListModel>)okResult2.Value).ListData.Where(a => a.Summary.Equals("ThanhNC-Test")).First();
                int reportID = data.ReportID;
                {
                    var actual = _service.GetByID(reportID);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Trouble>
                    {
                        Status = 200,
                        Data = TroubleData.model1
                    });
                    Assert.That(((ApiResult<Trouble>)okResult.Value).Status, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Trouble>)okResult.Value).Data.Report.CreatedBy, Is.EqualTo(((ApiResult<Trouble>)expected.Value).Data.Report.CreatedBy));
                    Assert.That(((ApiResult<Trouble>)okResult.Value).Data.Report.Description, Is.EqualTo(((ApiResult<Trouble>)expected.Value).Data.Report.Description));
                    Assert.That(((ApiResult<Trouble>)okResult.Value).Data.Report.Summary, Is.EqualTo(((ApiResult<Trouble>)expected.Value).Data.Report.Summary));
                }
            }
            finally
            {//clear data
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
                var okResult2 = searchactual as OkObjectResult;
                TroubleListModel data = ((ApiResult<TroubleListModel>)okResult2.Value).ListData.Where(a => a.Summary.Equals("ThanhNC-Test")).First();
                int reportID = data.ReportID;
                {
                    TroubleListModel model = new TroubleListModel
                    {
                        ReportID = reportID,
                        Technician = "Vuongpthe140353",
                        CreatedBy = "thanhnche140350",
                    };
                    var actual = _service.ChangeTechnician(model);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<TroubleListModel>
                    {
                        Status = 200,
                        Message = "Save successfully.",
                    });
                    Assert.That(((ApiResult<TroubleListModel>)okResult.Value).Status, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<TroubleListModel>)okResult.Value).Message, Is.EqualTo(((ApiResult<TroubleListModel>)expected.Value).Message));
                }
                {
                    TroubleListModel model = new TroubleListModel
                    {
                        ReportID = reportID,
                        Technician = "thanhnche140350",
                        CreatedBy = "thanhnche140350",
                    };
                    var actual = _service.ChangeTechnician(model);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<TroubleListModel>
                    {
                        Status = 200,
                        Message = "Save successfully.",
                    });
                    Assert.That(((ApiResult<TroubleListModel>)okResult.Value).Status, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<TroubleListModel>)okResult.Value).Message, Is.EqualTo(((ApiResult<TroubleListModel>)expected.Value).Message));
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
                var searchactual = _service.GetList(searchmodel); var okResult2 = searchactual as OkObjectResult;
                TroubleListModel data = ((ApiResult<TroubleListModel>)okResult2.Value).ListData.Where(a => a.Summary.Equals("ThanhNC-Test")).First();
                int reportID = data.ReportID;
                var actual = _service.CancelReport(reportID);
                var okResult = actual as OkObjectResult;
                OkObjectResult expected = new OkObjectResult(new ApiResult<Trouble>
                {
                    Status = 200,
                    Message = "Save successfully."
                });

                Assert.That(((ApiResult<Trouble>)okResult.Value).Status, Is.EqualTo(expected.StatusCode));
                Assert.That(((ApiResult<Trouble>)okResult.Value).Message, Is.EqualTo(((ApiResult<Trouble>)expected.Value).Message));
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
                var okResult2 = searchactual as OkObjectResult;
                TroubleListModel data = ((ApiResult<TroubleListModel>)okResult2.Value).ListData.Where(a => a.Summary.Equals("ThanhNC-Test")).First();

                int reportID = data.ReportID;
                var actual = _service.DeleteReport(reportID);
                var okResult = actual as OkObjectResult;
                OkObjectResult expected = new OkObjectResult(new ApiResult<Trouble>
                {
                    Status = 200,
                    Message = "Save successfully." //"Delete successfully."
                });

                Assert.That(((ApiResult<Trouble>)okResult.Value).Status, Is.EqualTo(expected.StatusCode));
                Assert.That(((ApiResult<Trouble>)okResult.Value).Message, Is.EqualTo(((ApiResult<Trouble>)expected.Value).Message));
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
                var okResult2 = searchactual as OkObjectResult;
                TroubleListModel data = ((ApiResult<TroubleListModel>)okResult2.Value).ListData.Where(a => a.Summary.Equals("ThanhNC-Test")).First();
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
                    trouble.ShootImage = new List<Image> { TroubleData.img3 };
                    var actual = _service.SaveTroubleshooting(trouble);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Trouble>
                    {
                        Status = 200,
                        Message = "Save troubleshooting successfully."
                    });

                    Assert.That(((ApiResult<Trouble>)okResult.Value).Status, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Trouble>)okResult.Value).Message, Is.EqualTo(((ApiResult<Trouble>)expected.Value).Message));
                }
                {
                    trouble.Shooting = TroubleData.troubleShooting;
                    trouble.Shooting.Priority = 1;
                    trouble.Shooting.ReportID = reportID;
                    trouble.ShootImage = new List<Image> { TroubleData.img3 };

                    var actual = _service.SaveTroubleshooting(trouble);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Trouble>
                    {
                        Status = 200,
                        Message = "Save troubleshooting successfully."
                    });

                    Assert.That(((ApiResult<Trouble>)okResult.Value).Status, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Trouble>)okResult.Value).Message, Is.EqualTo(((ApiResult<Trouble>)expected.Value).Message));
                }
                {
                    trouble.Shooting = TroubleData.troubleShooting;
                    trouble.Shooting.Solution = "buy new";
                    trouble.Shooting.ReportID = reportID;
                    trouble.ShootImage = new List<Image> { TroubleData.img3 };
                    var actual = _service.SaveTroubleshooting(trouble);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Trouble>
                    {
                        Status = 200,
                        Message = "Save troubleshooting successfully."
                    });

                    Assert.That(((ApiResult<Trouble>)okResult.Value).Status, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Trouble>)okResult.Value).Message, Is.EqualTo(((ApiResult<Trouble>)expected.Value).Message));
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
            //create data
            _data.CreateTestData();
            //test
            try
            {
                Paging paging = CommonData.paging1;
                TroubleSearchModel model = new TroubleSearchModel
                {
                    RoleID = 1,
                    Campus = "FU-HL",
                    User = "thanhnche140350",
                    paging = paging
                };
                var actual = _service.CountReports(model);
                var okResult = actual as OkObjectResult;
                OkObjectResult expected = new OkObjectResult(new ApiResult<CountModel>
                {
                    Status = 200,
                    ListData = new List<CountModel> { new CountModel { StepName = "Cancel", Quantity = 1 }, new CountModel { StepName = "Done", Quantity = 1 }, new CountModel { StepName = "Up Work", Quantity = 1 }, },
                    Paging = paging,
                });

                Assert.IsNotNull(okResult);
                Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                Assert.That(((ApiResult<CountModel>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<CountModel>)expected.Value).ListData.Count));
                
            }
            finally
            {//clear data
                _data.ClearTestData();
            }
        }
    }
}
