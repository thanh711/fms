using BusinessLogicLayer.BLLService;
using BusinessLogicLayer.IBLLService;
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
    internal class LocationControllerTest
    {
        private LocationController _service;
        private LocationData _data;
        [SetUp]
        public void SetUp()
        {
            Microsoft.Extensions.Configuration.IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .AddEnvironmentVariables()
                                    .Build();
            _service =new LocationController( new LocationBLL(new LocationService(config)),null);
            _data = new LocationData(config);
        }
        [Test]
        public void testSave()
        {
            //create data
            //test
            try
            {
                Location model = LocationData.model1;
                OkObjectResult expected = new OkObjectResult(new ApiResult<Location>
                {
                    Status = 200,
                    Message = "Save successfully.",
                });
                var actual = _service.Save(model);
                var okResult = actual as OkObjectResult;
                Assert.IsNotNull(okResult);
                Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                Assert.That(((ApiResult<Location>)okResult.Value).Message, Is.EqualTo(((ApiResult<Location>)expected.Value).Message));
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
            _data.CreateItem(LocationData.model1);
            //test
            try
            {
                OkObjectResult expected = new OkObjectResult(new ApiResult<Location>
                {
                    Status = 200,
                    Message = "Delete successfully.",
                });
                var allitem = _service.GetAll();
                var listokResult = allitem as OkObjectResult;
                List<Location> data = ((ApiResult<Location>)listokResult.Value).ListData;
                foreach (var item in data)
                {
                    if (item.Name.Contains("ThanhNC-Test"))
                    {
                        var actual = _service.Delete(item.ID);
                        var okResult = actual as OkObjectResult;
                        Assert.IsNotNull(okResult);
                        Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                        Assert.That(((ApiResult<Location>)okResult.Value).Message, Is.EqualTo(((ApiResult<Location>)expected.Value).Message));
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
            Location model = LocationData.model1;
            model.InService = false;
            _data.CreateTestData();
            //test
            try
            {
                OkObjectResult expected = new OkObjectResult(new ApiResult<Location>
                {
                    Status = 200,
                    Message = "Save successfully.",
                });
                var actual = _service.ChangeActive(model);
                var okResult = actual as OkObjectResult;
                Assert.IsNotNull(okResult);
                Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                Assert.That(((ApiResult<Location>)okResult.Value).Message, Is.EqualTo(((ApiResult<Location>)expected.Value).Message));
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
                    LocationSearchModel model = new LocationSearchModel
                    {
                        Campus = null,
                        LocationCode = "ThanhNC-Test",
                        paging = paging
                    };

                    OkObjectResult expected = new OkObjectResult(new ApiResult<Location>
                    {
                        Status = 200,
                        ListData = LocationData.Lists,
                        Paging = paging,
                    });
                    var actual = _service.GetListNoCondition(model);
                    var okResult = actual as OkObjectResult;

                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Location>)okResult.Value).Message, Is.EqualTo(((ApiResult<Location>)expected.Value).Message));
                    Assert.That(((ApiResult<Location>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<Location>)expected.Value).ListData.Count));
                }
                {
                    Paging paging = CommonData.paging2;
                    LocationSearchModel model = new LocationSearchModel
                    {
                        Campus = null,
                        LocationCode = "ThanhNC-Test",
                        paging = paging
                    };

                    OkObjectResult expected = new OkObjectResult(new ApiResult<Location>
                    {
                        Status = 200,
                        ListData = LocationData.Lists.Take(4).ToList(),
                        Paging = paging,
                    });

                    var actual = _service.GetListNoCondition(model);
                    var okResult = actual as OkObjectResult;

                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Location>)okResult.Value).Message, Is.EqualTo(((ApiResult<Location>)expected.Value).Message));
                    Assert.That(((ApiResult<Location>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<Location>)expected.Value).ListData.Count));
                }
                {
                    Paging paging = CommonData.paging3;
                    LocationSearchModel model = new LocationSearchModel
                    {
                        Campus = null,
                        LocationCode = "ThanhNC-Test",
                        paging = paging
                    };
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Location>
                    {
                        Status = 200,
                        ListData = LocationData.Lists.Skip(2).Take(2).ToList(),
                        Paging = paging,
                    });

                    var actual = _service.GetListNoCondition(model);
                    var okResult = actual as OkObjectResult;

                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Location>)okResult.Value).Message, Is.EqualTo(((ApiResult<Location>)expected.Value).Message));
                    Assert.That(((ApiResult<Location>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<Location>)expected.Value).ListData.Count));
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
