using BusinessLogicLayer.BLLService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
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
    internal class CampusControllerTest
    {
        private CampusController _service;
        private CampusData _data;
        [SetUp]
        public void SetUp()
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .AddEnvironmentVariables()
                                    .Build();
            _service = new CampusController(new CampusBLL(new CampusService(config)));
            _data = new CampusData(config);
        }
        [Test]
        public void testSave()
        {
            //create data
            //test
            try
            {
                {
                    Campus model = CampusData.model1;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Campus>
                    {
                        Status = 200,
                        Message = "Save successfully.",
                    });
                    var actual = _service.Save(model);
                    var okResult = actual as OkObjectResult;
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Campus>)okResult.Value).Message, Is.EqualTo(((ApiResult<Campus>)expected.Value).Message));
                }
                {
                    Campus model = CampusData.model2;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Campus>
                    {
                        Status = 200,
                        Message = "Save successfully.",
                    });
                    var actual = _service.Save(model);
                    var okResult = actual as OkObjectResult;
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Campus>)okResult.Value).Message, Is.EqualTo(((ApiResult<Campus>)expected.Value).Message));
                }
            }
            finally
            {
                //clear data
                _data.ClearTestData();
            }
        }
       
        [Test]
        public void testDelete()
        {
            //create data
            _data.CreateTestItem(CampusData.model1);
            //test
            try
            {
                OkObjectResult expected = new OkObjectResult(new ApiResult<Campus>
                {
                    Status = 200,
                    Message = "Delete successfully.",
                });
                var allitem = _service.GetAll();
                var listokResult = allitem as OkObjectResult;
                List<Campus> data = ((ApiResult<Campus>)listokResult.Value).ListData;
                foreach (var item in data)
                {
                    if (item.Name.Contains("ThanhNC-Test"))
                    {
                        var actual = _service.Delete(item.ID);
                        var okResult = actual as OkObjectResult;
                        Assert.IsNotNull(okResult);
                        Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                        Assert.That(((ApiResult<Campus>)okResult.Value).Message, Is.EqualTo(((ApiResult<Campus>)expected.Value).Message));
                    }
                }
            }
            finally
            {
                //clear data
                _data.ClearTestData();
            }
        }
        [Test]
        public void testChangeInService()
        {
            //create data
            Campus model = CampusData.model1;
            model.InService = false;
            _data.CreateTestData();
            //test
            try
            {
                OkObjectResult expected = new OkObjectResult(new ApiResult<Campus>
                {
                    Status = 200,
                    Message = "Save successfully.",
                });
                var actual = _service.ChangeActive(model);
                var okResult = actual as OkObjectResult;
                Assert.IsNotNull(okResult);
                Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                Assert.That(((ApiResult<Campus>)okResult.Value).Message, Is.EqualTo(((ApiResult<Campus>)expected.Value).Message));
            }
            finally
            {
                //clear data
                _data.ClearTestData();
            }
        }
        [Test]
        public void testGetList()
        {
            //create data
            _data.CreateTestData();
            //test
            try
            {
                {
                    Paging paging = CommonData.paging1;
                    CampusSearchModel model = new CampusSearchModel
                    {
                        CampusName = "ThanhNC-Test",
                        paging = paging
                    };
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Campus>
                    {
                        Status = 200,
                        ListData = CampusData.Lists,
                        Paging = paging,
                    });
                    var actual = _service.GetListNoCondition(model);
                    var okResult = actual as OkObjectResult;

                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Campus>)okResult.Value).Message, Is.EqualTo(((ApiResult<Campus>)expected.Value).Message));
                    Assert.That(((ApiResult<Campus>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<Campus>)expected.Value).ListData.Count));
                }
                {
                    Paging paging = CommonData.paging2;
                    CampusSearchModel model = new CampusSearchModel
                    {
                        CampusName = "ThanhNC-Test",
                        paging = paging
                    };
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Campus>
                    {
                        Status = 200,
                        ListData = CampusData.Lists.Take(4).ToList(),
                        Paging = paging,
                    });

                    var actual = _service.GetListNoCondition(model);
                    var okResult = actual as OkObjectResult;

                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Campus>)okResult.Value).Message, Is.EqualTo(((ApiResult<Campus>)expected.Value).Message));
                    Assert.That(((ApiResult<Campus>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<Campus>)expected.Value).ListData.Count));
                }
                {
                    Paging paging = CommonData.paging3;
                    CampusSearchModel model = new CampusSearchModel
                    {
                        CampusName = "ThanhNC-Test",
                        paging = paging
                    };
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Campus>
                    {
                        Status = 200,
                        ListData = CampusData.Lists.Skip(2).Take(2).ToList(),
                        Paging = paging,
                    });
                    var actual = _service.GetListNoCondition(model);
                    var okResult = actual as OkObjectResult;

                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Campus>)okResult.Value).Message, Is.EqualTo(((ApiResult<Campus>)expected.Value).Message));
                    Assert.That(((ApiResult<Campus>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<Campus>)expected.Value).ListData.Count));
                }
            }
            finally
            {
                //clear data
                _data.ClearTestData();
            }
        }
    }
}
