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
    internal class CategoryControllerTest
    {
        private CategoryController _service;
        private CategoryData _data;
        [SetUp]
        public void SetUp()
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .AddEnvironmentVariables()
                                    .Build();
            _service = new CategoryController(new CategoryBLL(new CategoryService(config)));
            _data = new CategoryData(config);
        }
        [Test]
        public void testSave()
        {
            //create data
            //test
            try
            {
                Category model = CategoryData.model1;
                OkObjectResult expected = new OkObjectResult(new ApiResult<Category>
                {
                    Status = 200,
                    Message = "Save successfully.",
                });
                var actual = _service.Save(model);
                var okResult = actual as OkObjectResult;
                Assert.IsNotNull(okResult);
                Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                Assert.That(((ApiResult<Category>)okResult.Value).Message, Is.EqualTo(((ApiResult<Category>)expected.Value).Message));
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
            _data.CreateItem(CategoryData.model1);
            //test
            try
            {
                OkObjectResult expected = new OkObjectResult(new ApiResult<Category>
                {
                    Status = 200,
                    Message = "Delete successfully.",
                });
                var allitem = _service.GetAll();
                var listokResult = allitem as OkObjectResult;
                List<Category> data = ((ApiResult<Category>)listokResult.Value).ListData;
                foreach (var item in data)
                {
                    if (item.Name.Contains("ThanhNC-Test"))
                    {
                        var actual = _service.Delete(item.ID);
                        var okResult = actual as OkObjectResult;
                        Assert.IsNotNull(okResult);
                        Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                        Assert.That(((ApiResult<Category>)okResult.Value).Message, Is.EqualTo(((ApiResult<Category>)expected.Value).Message));
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
                    CategorySearchModel model = new CategorySearchModel
                    {
                        CategoryL1Name = "ThanhNC-Test",
                        CategoryL2Name = null,
                        paging = paging
                    };
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Category>
                    {
                        Status = 200,
                        ListData = CategoryData.Lists,
                        Paging = paging,
                    });
                    var actual = _service.GetList(model);
                    var okResult = actual as OkObjectResult;

                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Category>)okResult.Value).Message, Is.EqualTo(((ApiResult<Category>)expected.Value).Message));
                    Assert.That(((ApiResult<Category>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<Category>)expected.Value).ListData.Count));
                }
                {
                    Paging paging = CommonData.paging2;
                    CategorySearchModel model = new CategorySearchModel
                    {
                        CategoryL1Name = "ThanhNC-Test",
                        CategoryL2Name = null,
                        paging = paging
                    };
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Category>
                    {
                        Status = 200,
                        ListData = CategoryData.Lists.Take(4).ToList(),
                        Paging = paging,
                    });
                    var actual = _service.GetList(model);
                    var okResult = actual as OkObjectResult;

                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Category>)okResult.Value).Message, Is.EqualTo(((ApiResult<Category>)expected.Value).Message));
                    Assert.That(((ApiResult<Category>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<Category>)expected.Value).ListData.Count));
                }
                {
                    Paging paging = CommonData.paging3;
                    CategorySearchModel model = new CategorySearchModel
                    {
                        CategoryL1Name = "ThanhNC-Test",
                        CategoryL2Name = null,
                        paging = paging
                    };
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Category>
                    {
                        Status = 200,
                        ListData = CategoryData.Lists.Skip(2).Take(2).ToList(),
                        Paging = paging,
                    });
                    var actual = _service.GetList(model);
                    var okResult = actual as OkObjectResult;

                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Category>)okResult.Value).Message, Is.EqualTo(((ApiResult<Category>)expected.Value).Message));
                    Assert.That(((ApiResult<Category>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<Category>)expected.Value).ListData.Count));
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
