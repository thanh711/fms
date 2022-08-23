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
    internal class AreaServiceTest
    {
        private IAreaService _service;
        private AreaData _data;
        [SetUp]
        public void SetUp()
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .AddEnvironmentVariables()
                                    .Build();
            _service = new AreaService(config);
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
                ApiResult<Area> expected = new ApiResult<Area>
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
            Area model = AreaData.model1;
            _data.CreateItem(model);
            //test
            try
            {
                ApiResult<Area> expected = new ApiResult<Area>
                {
                    Status = 200,
                    Message = "Delete successfully.",
                };
                Area data = _service.GetAll().ListData.Last();
                    if (data.Name.Contains("ThanhNC-Test"))
                    {
                        var actual = _service.Delete(data.ID);
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
        public void testChangeInService()
        {
            //create data
            Area model = AreaData.model1;
            _data.CreateItem(model);
            model.InService = false;
            //test
            try
            {
                var actual = _service.ChangeInService(model);
                ApiResult<Area> expected = new ApiResult<Area>
                {
                    Status = 200,
                    Message = "Save successfully.",
                };
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
                    AreaSearchModel model = new AreaSearchModel
                    {
                        Campus = null,
                        LocationCode = null,
                        RoomCode = "ThanhNC-Test",
                        paging = paging
                    };
                    var actual = _service.GetList(model);
                    ApiResult<Area> expected = new ApiResult<Area>
                    {
                        Status = 200,
                        ListData = AreaData.Lists,
                        Paging = paging,
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.ListData.Count, Is.EqualTo(expected.ListData.Count));
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
                    var actual = _service.GetList(model);
                    ApiResult<Area> expected = new ApiResult<Area>
                    {
                        Status = 200,
                        ListData = AreaData.Lists.Skip(4).Take(1).ToList(),
                        Paging = paging,
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.ListData.Count, Is.EqualTo(expected.ListData.Count));
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
                    var actual = _service.GetList(model);
                    ApiResult<Area> expected = new ApiResult<Area>
                    {
                        Status = 200,
                        ListData = AreaData.Lists.Skip(2).Take(2).ToList(),
                        Paging = paging,
                    };
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

        [Test]
        public void testGetAll()
        {
            var actual = _service.GetAll();
            ApiResult<Area> expected = new ApiResult<Area>
            {
                Status = 200,
                // ListData = AreaData.Lists,
            };
            Assert.That(actual.Status, Is.EqualTo(expected.Status));
            //  CollectionAssert.AreEqual(expected.ListData, actual.ListData);
        }
    }
}
