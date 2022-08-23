using BusinessLogicLayer.BLLService;
using BusinessLogicLayer.IBLLService;
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

namespace TestProject1.BusinessLogicLayerTest
{
    internal class CategoryBLLTest
    {
        private ICategoryBLL _service;
        private CategoryData _data;
        [SetUp]
        public void SetUp()
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .AddEnvironmentVariables()
                                    .Build();
            _service = new CategoryBLL(new CategoryService(config));
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
                ApiResult<Category> expected = new ApiResult<Category>
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
            _data.CreateItem(CategoryData.model1);
            //test
            try
            {
                ApiResult<Category> expected = new ApiResult<Category>
                {
                    Status = 200,
                    Message = "Delete successfully.",
                };
                List<Category> data = _service.GetAll().ListData;
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
                    CategorySearchModel model = new CategorySearchModel
                    {
                        CategoryL1Name = "ThanhNC-Test",
                        CategoryL2Name = null,
                        paging = paging
                    };
                    var actual = _service.GetList(model);
                    ApiResult<Category> expected = new ApiResult<Category>
                    {
                        Status = 200,
                        ListData = CategoryData.Lists,
                        Paging = paging,
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.ListData.Count, Is.EqualTo(expected.ListData.Count));

                }
                {
                    Paging paging = CommonData.paging2;
                    CategorySearchModel model = new CategorySearchModel
                    {
                        CategoryL1Name = "ThanhNC-Test",
                        CategoryL2Name = null,
                        paging = paging
                    };
                    var actual = _service.GetList(model);
                    ApiResult<Category> expected = new ApiResult<Category>
                    {
                        Status = 200,
                        ListData = CategoryData.Lists.Take(4).ToList(),
                        Paging = paging,
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.ListData.Count, Is.EqualTo(expected.ListData.Count));
                }
                {
                    Paging paging = CommonData.paging3;
                    CategorySearchModel model = new CategorySearchModel
                    {
                        CategoryL1Name = "ThanhNC-Test",
                        CategoryL2Name = null,
                        paging = paging
                    };
                    var actual = _service.GetList(model);
                    ApiResult<Category> expected = new ApiResult<Category>
                    {
                        Status = 200,
                        ListData = CategoryData.Lists.Skip(2).Take(2).ToList(),
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
