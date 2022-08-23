using BusinessLogicLayer.BLLService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
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
    internal class AssetControllerTest
    {
        private AssetController _controller;
        private AssetData _data;
        [SetUp]
        public void SetUp()
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .AddEnvironmentVariables()
                                    .Build();
            _controller = new AssetController(new AssetBLL(new AssetService(config)));
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
                OkObjectResult expected = new OkObjectResult(new ApiResult<Asset>
                {
                    Status = 200,
                    Message = "Save successfully.",
                });
                var actual = _controller.Save(model);
                var okResult = actual as OkObjectResult;
                Assert.IsNotNull(okResult);
                Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                Assert.That(((ApiResult<Asset>)okResult.Value).Message, Is.EqualTo(((ApiResult<Asset>)expected.Value).Message));
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
                OkObjectResult expected = new OkObjectResult(new ApiResult<Asset>
                {
                    Status = 200,
                    Message = "Delete successfully.",
                });
                var allitem = _controller.GetAll();
                var listokResult = allitem as OkObjectResult;
                List<Asset> data = ((ApiResult<Asset>)listokResult.Value).ListData;
                foreach (var item in data)
                {
                    if (item.Name.Contains("ThanhNC-Test"))
                    {
                        var actual = _controller.Delete(item.ID);
                        var okResult = actual as OkObjectResult;
                        Assert.IsNotNull(okResult);
                        Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                        Assert.That(((ApiResult<Asset>)okResult.Value).Message, Is.EqualTo(((ApiResult<Asset>)expected.Value).Message));
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

                OkObjectResult expected = new OkObjectResult(new ApiResult<Asset>
                {
                    Status = 200,
                    ListData = AssetData.Lists,
                    Paging = paging,
                });
                var actual = _controller.GetList(model);
                var okResult = actual as OkObjectResult;

                Assert.IsNotNull(okResult);
                Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                Assert.That(((ApiResult<Asset>)okResult.Value).Message, Is.EqualTo(((ApiResult<Asset>)expected.Value).Message));
                Assert.That(((ApiResult<Asset>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<Asset>)expected.Value).ListData.Count));
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

                    OkObjectResult expected = new OkObjectResult(new ApiResult<Asset>
                    {
                        Status = 200,
                        ListData = AssetData.Lists.Skip(4).Take(1).ToList(),
                        Paging = paging,
                    });

                    var actual = _controller.GetList(model);
                    var okResult = actual as OkObjectResult;

                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Asset>)okResult.Value).Message, Is.EqualTo(((ApiResult<Asset>)expected.Value).Message));
                    Assert.That(((ApiResult<Asset>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<Asset>)expected.Value).ListData.Count));
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

                    OkObjectResult expected = new OkObjectResult(new ApiResult<Asset>
                    {
                        Status = 200,
                        ListData = AssetData.Lists.Skip(2).Take(2).ToList(),
                        Paging = paging,
                    });

                    var actual = _controller.GetList(model);
                    var okResult = actual as OkObjectResult;

                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Asset>)okResult.Value).Message, Is.EqualTo(((ApiResult<Asset>)expected.Value).Message));
                    Assert.That(((ApiResult<Asset>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<Asset>)expected.Value).ListData.Count));
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
