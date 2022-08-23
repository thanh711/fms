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
    internal class AreaRoomControllerTest
    {
        private AreaRoomController _service;
        private AreaData _data;
        [SetUp]
        public void SetUp()
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .AddEnvironmentVariables()
                                    .Build();
            _service = new AreaRoomController(new AreaBLL(new AreaService(config)),null);
            _data = new AreaData(config);
        }
        [Test]
        public void testSave()
        {
            //create data
            //test
            try
            {
                Area model = AreaData.model1;
                OkObjectResult expected = new OkObjectResult(new ApiResult<Area>
                {
                    Status = 200,
                    Message = "Save successfully.",
                });
                var actual = _service.Save(model);
                var okResult = actual as OkObjectResult;
                Assert.IsNotNull(okResult);
                Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                Assert.That(((ApiResult<Area>)okResult.Value).Message, Is.EqualTo(((ApiResult<Area>)expected.Value).Message));
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
            Area model = AreaData.model1;
            _data.CreateItem(model);
            //test
            try
            {
                OkObjectResult expected = new OkObjectResult(new ApiResult<Area>
                {
                    Status = 200,
                    Message = "Delete successfully.",
                });
                var allitem = _service.GetAll();
                var listokResult = allitem as OkObjectResult;
                List<Area> data = ((ApiResult<Area>)listokResult.Value).ListData;
                foreach (var item in data)
                {
                    if (item.Name.Contains("ThanhNC-Test"))
                    {
                        var actual = _service.Delete(item.ID);
                        var okResult = actual as OkObjectResult;
                        Assert.IsNotNull(okResult);
                        Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                        Assert.That(((ApiResult<Area>)okResult.Value).Message, Is.EqualTo(((ApiResult<Area>)expected.Value).Message));
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
            Area model = AreaData.model1;
            _data.CreateItem(model);
            model.InService = false;
            //test
            try
            {
                OkObjectResult expected = new OkObjectResult(new ApiResult<Area>
                {
                    Status = 200,
                    Message = "Save successfully.",
                });
                var actual = _service.ChangeActive(model);
                var okResult = actual as OkObjectResult;
                Assert.IsNotNull(okResult);
                Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                Assert.That(((ApiResult<Area>)okResult.Value).Message, Is.EqualTo(((ApiResult<Area>)expected.Value).Message));
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
                AreaSearchModel model = new AreaSearchModel
                {
                    Campus = null,
                    LocationCode = null,
                    RoomCode = "ThanhNC-Test",
                    paging = paging
                };

                OkObjectResult expected = new OkObjectResult(new ApiResult<Area>
                {
                    Status = 200,
                    ListData = AreaData.Lists,
                    Paging = paging,
                });
                var actual = _service.GetListNoCondition(model);
                var okResult = actual as OkObjectResult;

                Assert.IsNotNull(okResult);
                Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                Assert.That(((ApiResult<Area>)okResult.Value).Message, Is.EqualTo(((ApiResult<Area>)expected.Value).Message));
                Assert.That(((ApiResult<Area>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<Area>)expected.Value).ListData.Count));
            }
                {
                    Paging paging = CommonData.paging2;
                    AreaSearchModel model = new AreaSearchModel
                    {
                        Campus = null,
                        LocationCode = "BE",
                        RoomCode = "ThanhNC-Test",
                        paging = paging
                    };

                    OkObjectResult expected = new OkObjectResult(new ApiResult<Area>
                    {
                        Status = 200,
                        ListData = AreaData.Lists.Skip(4).Take(1).ToList(),
                        Paging = paging,
                    });

                    var actual = _service.GetListNoCondition(model);
                    var okResult = actual as OkObjectResult;

                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Area>)okResult.Value).Message, Is.EqualTo(((ApiResult<Area>)expected.Value).Message));
                    Assert.That(((ApiResult<Area>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<Area>)expected.Value).ListData.Count));
                }
                {
                    Paging paging = CommonData.paging3;
                    AreaSearchModel model = new AreaSearchModel
                    {
                        Campus = "FU-HL",
                        LocationCode = null,
                        RoomCode = "ThanhNC-Test",
                        paging = paging
                    };

                    OkObjectResult expected = new OkObjectResult(new ApiResult<Area>
                    {
                        Status = 200,
                        ListData = AreaData.Lists.Skip(2).Take(2).ToList(),
                        Paging = paging,
                    });

                    var actual = _service.GetListNoCondition(model);
                    var okResult = actual as OkObjectResult;

                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Area>)okResult.Value).Message, Is.EqualTo(((ApiResult<Area>)expected.Value).Message));
                    Assert.That(((ApiResult<Area>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<Area>)expected.Value).ListData.Count));
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
