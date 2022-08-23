using BusinessLogicLayer.BLLService;
using BusinessLogicLayer.IBLLService;
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

namespace TestProject1.BusinessLogicLayerTest
{
    internal class CampusBLLTest
    {
        private ICampusBLL _service;
        private CampusData _data;
        [SetUp]
        public void SetUp()
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .AddEnvironmentVariables()
                                    .Build();
            _service = new CampusBLL(new CampusService(config));
            _data = new CampusData(config);
        }
        [Test]
        public void testSave()
        {
            //create data
            //test
            try
            {
                Campus model = CampusData.model1;
                ApiResult<Campus> expected = new ApiResult<Campus>
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
            _data.CreateTestItem(CampusData.model1);
            //test
            try
            {
                ApiResult<Campus> expected = new ApiResult<Campus>
                {
                    Status = 200,
                    Message = "Delete successfully.",
                };
                List<Campus> data = _service.GetAll().ListData;
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
            Campus model = CampusData.model1;
            model.InService = false;
            _data.CreateTestData();
            //test
            try
            {
                var actual = _service.ChangeInService(model);
                ApiResult<Campus> expected = new ApiResult<Campus>
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
                    CampusSearchModel model = new CampusSearchModel
                    {
                        CampusName = "ThanhNC-Test",
                        paging = paging
                    };
                    var actual = _service.GetList(model);
                    ApiResult<Campus> expected = new ApiResult<Campus>
                    {
                        Status = 200,
                        ListData = CampusData.Lists,
                        Paging = paging,
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.ListData.Count, Is.EqualTo(expected.ListData.Count));
                }
                {
                    Paging paging = CommonData.paging2;
                    CampusSearchModel model = new CampusSearchModel
                    {
                        CampusName = "ThanhNC-Test",
                        paging = paging
                    };
                    var actual = _service.GetList(model);
                    ApiResult<Campus> expected = new ApiResult<Campus>
                    {
                        Status = 200,
                        ListData = CampusData.Lists.Take(4).ToList(),
                        Paging = paging,
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.ListData.Count, Is.EqualTo(expected.ListData.Count));
                }
                {
                    Paging paging = CommonData.paging3;
                    CampusSearchModel model = new CampusSearchModel
                    {
                        CampusName = "ThanhNC-Test",
                        paging = paging
                    };
                    var actual = _service.GetList(model);
                    ApiResult<Campus> expected = new ApiResult<Campus>
                    {
                        Status = 200,
                        ListData = CampusData.Lists.Skip(2).Take(2).ToList(),
                        Paging = paging,
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.ListData.Count, Is.EqualTo(expected.ListData.Count));

                }
            }
            finally
            {//clear data
                _data.ClearTestData();
            }
        }
    }
}
