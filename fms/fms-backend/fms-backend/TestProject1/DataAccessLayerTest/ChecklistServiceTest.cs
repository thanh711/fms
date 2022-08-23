using DataAccessLayer.IService;
using DataAccessLayer.Model.Checklist;
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
    internal class ChecklistServiceTest
    {
        private IChecklistService _service;
        private ChecklistData _data;
        [SetUp]
        public void SetUp()
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .AddEnvironmentVariables()
                                    .Build();
            _service = new ChecklistService(config);
            _data = new ChecklistData(config);
        }
        [Test]
        public void testGetList()
        {
            //create data
            _data.CreateTestData();
            //test
            try
            {
                Paging paging = CommonData.paging4;
                ChecklistSearchModel model = new ChecklistSearchModel
                {
                    CampusName = "FU-HL",
                    LocationCode = "DE",
                    ChecklistType = "Daily",
                    paging = paging
                };
                var actual = _service.GetList(model);
                List<Checklist> checklists = actual.ListData.Where(a => a.ID.Contains("2021")).ToList();
                ApiResult<Checklist> expected = new ApiResult<Checklist>
                {
                    Status = 200,
                    ListData = ChecklistData.checklists,
                    Paging = paging,
                };
                Assert.That(actual.Status, Is.EqualTo(expected.Status));
                Assert.That(checklists.Count, Is.EqualTo(expected.ListData.Count));
                
            }
            finally
            {//clear data
                _data.ClearTestData();
            }
        }

        [Test]
        public void testGetWeeklyChecklist()
        {
            //create data
            // _data.CreateTestData();
            //test
            try
            {
                {
                    WeeklyChecklistSearchModel model = ChecklistData.weekly1;
                    var actual = _service.GetWeeklyChecklist(model);
                    ApiResult<WeeklyChecklistModel> expected = new ApiResult<WeeklyChecklistModel>
                    {
                        Status = 200,
                        ListData = ChecklistData.weeklyChecklistModels1,
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.ListData.Count, Is.EqualTo(expected.ListData.Count));
                    Assert.That(actual.ListData[0].ChecklistType, Is.EqualTo(expected.ListData[0].ChecklistType));
                }
                {
                    WeeklyChecklistSearchModel model = ChecklistData.weekly2;
                    var actual = _service.GetWeeklyChecklist(model);
                    ApiResult<WeeklyChecklistModel> expected = new ApiResult<WeeklyChecklistModel>
                    {
                        Status = 200,
                        ListData = ChecklistData.weeklyChecklistModels2,
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.ListData.Count, Is.EqualTo(expected.ListData.Count));
                    Assert.That(actual.ListData[0].ChecklistType, Is.EqualTo(expected.ListData[0].ChecklistType));
                }
                {
                    WeeklyChecklistSearchModel model = ChecklistData.weekly3;
                    var actual = _service.GetWeeklyChecklist(model);
                    ApiResult<WeeklyChecklistModel> expected = new ApiResult<WeeklyChecklistModel>
                    {
                        Status = 200,
                        ListData = ChecklistData.weeklyChecklistModels3,
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.ListData.Count, Is.EqualTo(expected.ListData.Count));
                    Assert.That(actual.ListData[0].ChecklistType, Is.EqualTo(expected.ListData[0].ChecklistType));
                }
            }
            finally
            {//clear data
                _data.ClearTestData();
            }
        }
        [Test]
        public void testGetDetail()
        {
            //create data
             _data.CreateTestData();
            //test
            try
            {
                {
                    string checklistId = ChecklistData.checklistd1.ID;
                    var actual = _service.GetDetail(checklistId);
                    ApiResult<ChecklistDetailApiResult> expected = new ApiResult<ChecklistDetailApiResult>
                    {
                        Status = 200,
                        Data = ChecklistData.apiResultd1,
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Data.ChecklistInfo.ID, Is.EqualTo(expected.Data.ChecklistInfo.ID));
                }
                {
                    string checklistId = ChecklistData.checklistd2.ID;
                    var actual = _service.GetDetail(checklistId);
                    ApiResult<ChecklistDetailApiResult> expected = new ApiResult<ChecklistDetailApiResult>
                    {
                        Status = 200,
                        Data = ChecklistData.apiResultd2,
                    };
                   Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Data.ChecklistInfo.ID, Is.EqualTo(expected.Data.ChecklistInfo.ID));
                }
                {
                    string checklistId = ChecklistData.checklistd3.ID;
                    var actual = _service.GetDetail(checklistId);
                    ApiResult<ChecklistDetailApiResult> expected = new ApiResult<ChecklistDetailApiResult>
                    {
                        Status = 200,
                        Data = ChecklistData.apiResultd3,
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Data.ChecklistInfo.ID, Is.EqualTo(expected.Data.ChecklistInfo.ID));
                }
                {
                    string checklistId = ChecklistData.checklistl1.ID;
                    var actual = _service.GetDetail(checklistId);
                    ApiResult<ChecklistDetailApiResult> expected = new ApiResult<ChecklistDetailApiResult>
                    {
                        Status = 200,
                        Data = ChecklistData.apiResultd4,
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Data.ChecklistInfo.ID, Is.EqualTo(expected.Data.ChecklistInfo.ID));
                }
            }
            finally
            {
                //clear data
                _data.ClearTestData();
            }
        }
        [Test]
        public void testGetCustomizeDetail()
        {
            //create data
            // _data.CreateTestData();
            //test
            try
            {

                ApiResult<CustomizeModel> expected = new ApiResult<CustomizeModel>
                {
                    Status = 200,
                    Data = ChecklistData.customize
                };
                int tempId = expected.Data.Component.ID;
                var actual = _service.GetCustomizeDetail(tempId);
                Assert.That(actual.Status, Is.EqualTo(expected.Status));
                Assert.That(actual.Data.Items.Count, Is.EqualTo(expected.Data.Items.Count));
                Assert.That(actual.Data.Items[0].ID, Is.EqualTo(expected.Data.Items[0].ID));
                //clear data
            }
            finally
            {
                _data.ClearTestData();
            }
        }
        [Test]
        public void testSaveChecklist()
        {
            //create data
            //test
            try
            {
                {
                    var actual = _service.SaveChecklist(ChecklistData.apiResult1);
                    ApiResult<Checklist> expected = new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Message, Is.EqualTo(expected.Message));
                }
                {
                    var actual = _service.SaveChecklist(ChecklistData.apiResult2);
                    ApiResult<Checklist> expected = new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Message, Is.EqualTo(expected.Message));
                }
                {
                    var actual = _service.SaveChecklist(ChecklistData.apiResult3);
                    ApiResult<Checklist> expected = new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Message, Is.EqualTo(expected.Message));
                }
            }
            finally
            {//clear data
                _data.ClearTestData();
            }
        }
        [Test]
        public void testSaveImport()
        {
            //create data
            //_data.ClearTestData();
            //test
            try
            {
                {
                    var actual = _service.SaveImport(ChecklistData.importModel21);
                    ApiResult<Checklist> expected = new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Message, Is.EqualTo(expected.Message));
                }
                {
                    var actual = _service.SaveImport(ChecklistData.importModel22);
                    ApiResult<Checklist> expected = new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Message, Is.EqualTo(expected.Message));
                }
                {
                    var actual = _service.SaveImport(ChecklistData.importModel23);
                    ApiResult<Checklist> expected = new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Message, Is.EqualTo(expected.Message));
                }
                {
                    var actual = _service.SaveImport(ChecklistData.importModel24);
                    ApiResult<Checklist> expected = new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Message, Is.EqualTo(expected.Message));
                }
                {
                    var actual = _service.SaveImport(ChecklistData.importModel25);
                    ApiResult<Checklist> expected = new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    };
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
        public void testCreateNew()
        {
            //create data
            _data.ClearTestData();
            //test
            try
            {
                {
                    var actual = _service.CreateNew(ChecklistData.importModel41);
                    ApiResult<Checklist> expected = new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Message, Is.EqualTo(expected.Message));
                }
                {
                    var actual = _service.CreateNew(ChecklistData.importModel42);
                    ApiResult<Checklist> expected = new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Message, Is.EqualTo(expected.Message));
                }
                {
                    var actual = _service.CreateNew(ChecklistData.importModel43);
                    ApiResult<Checklist> expected = new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Message, Is.EqualTo(expected.Message));
                }
                {
                    var actual = _service.CreateNew(ChecklistData.importModel44);
                    ApiResult<Checklist> expected = new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    };
                    Assert.That(actual.Status, Is.EqualTo(expected.Status));
                    Assert.That(actual.Message, Is.EqualTo(expected.Message));
                }
                {
                    var actual = _service.CreateNew(ChecklistData.importModel45);
                    ApiResult<Checklist> expected = new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    };
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
    }
}
