using DataAccessLayer.IService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
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
    internal class LocationServiceTest
    {
        private ILocationService _service;
        private LocationData _data;
        [SetUp]
        public void SetUp()
        {
            Microsoft.Extensions.Configuration.IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .AddEnvironmentVariables()
                                    .Build();
            _service = new LocationService(config);
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
                ApiResult<Location> expected = new ApiResult<Location>
                {
                    Status = 200,
                    Message = "Save successfully.",
                };
                var actual = _service.Save(model);
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
        public void testDelete()
        {
            //create data
            _data.CreateItem(LocationData.model1);
            //test
            try
            {
                ApiResult<Location> expected = new ApiResult<Location>
                {
                    Status = 200,
                    Message = "Delete successfully.",
                };
                List<Location> data = _service.GetAll().ListData;
                foreach (var item in data)
                {
                    if (item.Name.Contains("ThanhNC-Test"))
                    {
                        var actual = _service.Delete(item.ID);
                        Assert.That(actual.Status, Is.EqualTo(expected.Status));
                        Assert.That(actual.Message, Is.EqualTo(expected.Message));
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
                ApiResult<Location> expected = new ApiResult<Location>
                {
                    Status = 200,
                    Message = "Save successfully.",
                };
                var actual = _service.ChangeInService(model);
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
                    var actual = _service.GetList(model);
                    ApiResult<Location> expected = new ApiResult<Location>
                    {
                        Status = 200,
                        ListData = LocationData.Lists,
                        Paging = paging,
                    };
                    Assert.That(actual.Paging, Is.EqualTo(expected.Paging));
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.ListData.Count, Is.EqualTo(expected.ListData.Count));
                }
                {
                    Paging paging = CommonData.paging2;
                    LocationSearchModel model = new LocationSearchModel
                    {
                        Campus = null,
                        LocationCode = "ThanhNC-Test",
                        paging = paging
                    };
                    var actual = _service.GetList(model);
                    ApiResult<Location> expected = new ApiResult<Location>
                    {
                        Status = 200,
                        ListData = LocationData.Lists.Take(4).ToList(),
                        Paging = paging,
                    };
                    Assert.That(actual.Paging, Is.EqualTo(expected.Paging));
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.ListData.Count, Is.EqualTo(expected.ListData.Count));
                }
                {
                    Paging paging = CommonData.paging3;
                    LocationSearchModel model = new LocationSearchModel
                    {
                        Campus = null,
                        LocationCode = "ThanhNC-Test",
                        paging = paging
                    };
                    var actual = _service.GetList(model);
                    ApiResult<Location> expected = new ApiResult<Location>
                    {
                        Status = 200,
                        ListData = LocationData.Lists.Skip(2).Take(2).ToList(),
                        Paging = paging,
                    };
                    Assert.That(actual.Paging, Is.EqualTo(expected.Paging));
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.ListData.Count, Is.EqualTo(expected.ListData.Count));
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
