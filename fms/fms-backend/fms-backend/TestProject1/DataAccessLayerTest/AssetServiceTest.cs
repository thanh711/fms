using DataAccessLayer.IService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
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
    internal class AssetServiceTest
    {
        private IAssetService _service;
        private AssetData _data;
        [SetUp]
        public void SetUp()
        {
            Microsoft.Extensions.Configuration.IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .AddEnvironmentVariables()
                                    .Build();
            _service = new AssetService(config);
            _data = new AssetData(config);
        }
        [Test]
        public void testSave()
        {
            //create data
            //test
            try
            {
                Asset model = AssetData.model1;
                ApiResult<Asset> expected = new ApiResult<Asset>
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
            Asset model = AssetData.model1;
            _data.CreateAssetData(model);
            //test
            try
            {
                
                ApiResult<Asset> expected = new ApiResult<Asset>
                {
                    Status = 200,
                    Message = "Delete successfully.",
                };
                List<Asset> data = _service.GetAll().ListData;
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
        public void testGetList()
        {
            //create data
            _data.CreateTestData();
            //test
            try
            {
                {
                    Paging paging = CommonData.paging1;
                    AssetSearchModel model = new AssetSearchModel
                    {
                        campusName = null,
                        locationCode = null,
                        roomName = null,
                        assetName = "ThanhNC-Test",
                        paging = paging
                    };
                    var actual = _service.GetList(model);
                    ApiResult<Asset> expected = new ApiResult<Asset>
                    {
                        Status = 200,
                        ListData = AssetData.Lists,
                        Paging = paging,
                    };
                    Assert.That(actual.Paging, Is.EqualTo(expected.Paging));
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.ListData.Count, Is.EqualTo(expected.ListData.Count));

                }
                {
                    Paging paging = CommonData.paging2;
                    AssetSearchModel model = new AssetSearchModel
                    {
                        campusName = null,
                        locationCode = "DE",
                        roomName = null,
                        assetName = "ThanhNC-Test",
                        paging = paging
                    };
                    var actual = _service.GetList(model);
                    ApiResult<Asset> expected = new ApiResult<Asset>
                    {
                        Status = 200,
                        ListData = AssetData.Lists.Skip(4).Take(1).ToList(),
                        Paging = paging,
                    };
                    Assert.That(actual.Paging, Is.EqualTo(expected.Paging));
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.ListData.Count, Is.EqualTo(expected.ListData.Count));
                }
                {
                    Paging paging = CommonData.paging3;
                    AssetSearchModel model = new AssetSearchModel
                    {
                        campusName = "FU-HL",
                        locationCode = null,
                        roomName = null,
                        assetName = "ThanhNC-Test",
                        paging = paging
                    };
                    var actual = _service.GetList(model);
                    ApiResult<Asset> expected = new ApiResult<Asset>
                    {
                        Status = 200,
                        ListData = AssetData.Lists.Skip(2).Take(2).ToList(),
                        Paging = paging,
                    };
                    Assert.That(actual.Paging, Is.EqualTo(expected.Paging));
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
