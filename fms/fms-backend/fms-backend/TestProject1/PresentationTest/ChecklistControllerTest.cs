using BusinessLogicLayer.BLLService;
using DataAccessLayer.Model.Checklist;
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
    internal class ChecklistControllerTest
    {
        private ChecklistController _service;
        private ChecklistData _data;
        [SetUp]
        public void SetUp()
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .AddEnvironmentVariables()
                                    .Build();
            _service = new ChecklistController(null, new ChecklistBLL(new ChecklistTemplateService(config), new ChecklistService(config)));
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
                var okResult = actual as OkObjectResult;
                List<Checklist> checklists = ((ApiResult<Checklist>)okResult.Value).ListData.Where(a => a.ID.Contains("2021")).ToList();
                OkObjectResult expected = new OkObjectResult(new ApiResult<Checklist>
                {
                    Status = 200,
                    ListData = ChecklistData.checklists,
                    Paging = paging,
                });
                Assert.IsNotNull(okResult);
                Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                Assert.That(checklists.Count, Is.EqualTo(((ApiResult<Checklist>)expected.Value).ListData.Count));

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
            _data.CreateTestData();
            //test
            try
            {
                {
                    WeeklyChecklistSearchModel model = ChecklistData.weekly2;
                    var actual = _service.GetWeeklyList(model);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<WeeklyChecklistModel>
                    {
                        Status = 200,
                        ListData = ChecklistData.weeklyChecklistModels2,
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<WeeklyChecklistModel>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<WeeklyChecklistModel>)expected.Value).ListData.Count));
                    Assert.That(((ApiResult<WeeklyChecklistModel>)okResult.Value).ListData[0].ChecklistType, Is.EqualTo(((ApiResult<WeeklyChecklistModel>)expected.Value).ListData[0].ChecklistType));
                }
                {
                    WeeklyChecklistSearchModel model = ChecklistData.weekly3;
                    var actual = _service.GetWeeklyList(model);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<WeeklyChecklistModel>
                    {
                        Status = 200,
                        ListData = ChecklistData.weeklyChecklistModels3,
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<WeeklyChecklistModel>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<WeeklyChecklistModel>)expected.Value).ListData.Count));
                    Assert.That(((ApiResult<WeeklyChecklistModel>)okResult.Value).ListData[0].ChecklistType, Is.EqualTo(((ApiResult<WeeklyChecklistModel>)expected.Value).ListData[0].ChecklistType));
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
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<ChecklistDetailApiResult>
                    {
                        Status = 200,
                        Data = ChecklistData.apiResultd1,
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<ChecklistDetailApiResult>)okResult.Value).Data.ChecklistInfo.ID, Is.EqualTo(((ApiResult<ChecklistDetailApiResult>)expected.Value).Data.ChecklistInfo.ID));
                }
                {
                    string checklistId = ChecklistData.checklistd2.ID;
                    var actual = _service.GetDetail(checklistId);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<ChecklistDetailApiResult>
                    {
                        Status = 200,
                        Data = ChecklistData.apiResultd2,
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<ChecklistDetailApiResult>)okResult.Value).Data.ChecklistInfo.ID, Is.EqualTo(((ApiResult<ChecklistDetailApiResult>)expected.Value).Data.ChecklistInfo.ID));
                }
                {
                    string checklistId = ChecklistData.checklistd3.ID;
                    var actual = _service.GetDetail(checklistId);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<ChecklistDetailApiResult>
                    {
                        Status = 200,
                        Data = ChecklistData.apiResultd3,
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<ChecklistDetailApiResult>)okResult.Value).Data.ChecklistInfo.ID, Is.EqualTo(((ApiResult<ChecklistDetailApiResult>)expected.Value).Data.ChecklistInfo.ID));
                }
                {
                    string checklistId = ChecklistData.checklistl1.ID;
                    var actual = _service.GetDetail(checklistId);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<ChecklistDetailApiResult>
                    {
                        Status = 200,
                        Data = ChecklistData.apiResultd4,
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<ChecklistDetailApiResult>)okResult.Value).Data.ChecklistInfo.ID, Is.EqualTo(((ApiResult<ChecklistDetailApiResult>)expected.Value).Data.ChecklistInfo.ID));
                }
            }
            finally
            {//clear data
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
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Checklist>)okResult.Value).Message, Is.EqualTo(((ApiResult<Checklist>)expected.Value).Message));
                }
                {
                    var actual = _service.SaveChecklist(ChecklistData.apiResult2);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Checklist>)okResult.Value).Message, Is.EqualTo(((ApiResult<Checklist>)expected.Value).Message));
                }
                {
                    var actual = _service.SaveChecklist(ChecklistData.apiResult3);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<Checklist>)okResult.Value).Message, Is.EqualTo(((ApiResult<Checklist>)expected.Value).Message));
                }
            }
            finally
            { //clear data
                _data.ClearTestData();
            }
        }
        [Test]
        public void testSaveImport()
        {
            //create data
            //test
            try
            {
                { 
                var actual = _service.SaveImport(ChecklistData.importModel31);
                var okResult = actual as OkObjectResult;
                OkObjectResult expected = new OkObjectResult(new ApiResult<Checklist>
                {
                    Status = 200,
                    Message = "Save successfully."
                });
                Assert.IsNotNull(okResult);
                Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                Assert.That(((ApiResult<ImportModel>)okResult.Value).Message, Is.EqualTo(((ApiResult<Checklist>)expected.Value).Message));
            }
                {
                    var actual = _service.SaveImport(ChecklistData.importModel32);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<ImportModel>)okResult.Value).Message, Is.EqualTo(((ApiResult<Checklist>)expected.Value).Message));
                }
                {
                    var actual = _service.SaveImport(ChecklistData.importModel33);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<ImportModel>)okResult.Value).Message, Is.EqualTo(((ApiResult<Checklist>)expected.Value).Message));
                }
                {
                    var actual = _service.SaveImport(ChecklistData.importModel34);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<ImportModel>)okResult.Value).Message, Is.EqualTo(((ApiResult<Checklist>)expected.Value).Message));
                }
                {
                    var actual = _service.SaveImport(ChecklistData.importModel35);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<ImportModel>)okResult.Value).Message, Is.EqualTo(((ApiResult<Checklist>)expected.Value).Message));
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
             _data.CreateTestData();
            //test
            try
            {
                OkObjectResult expected = new OkObjectResult(new ApiResult<CustomizeModel>
                {
                    Status = 200,
                    Data = ChecklistData.customize
                });
                int tempId = ((ApiResult<CustomizeModel>)expected.Value).Data.Component.ID;
                var actual = _service.GetCustomizeDetail(tempId);
                var okResult = actual as OkObjectResult;
                Assert.IsNotNull(okResult);
                Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                Assert.That(((ApiResult<CustomizeModel>)okResult.Value).Data.Items.Count, Is.EqualTo(((ApiResult<CustomizeModel>)expected.Value).Data.Items.Count));
                //clear data
            }
            finally
            {
                _data.ClearTestData();
            }
        }
        [Test]
        public void testGetAllType()
        {
            //create data
            //test
            try
            {
                string campus = "FU-HL";
                 var actual = _service.GetTemplates(campus);
                  var okResult = actual as OkObjectResult;
                OkObjectResult expected = new OkObjectResult(new ApiResult<Template>
                {
                    Status = 200,
                      ListData = ChecklistData.templates
                });
                  Assert.IsNotNull(okResult);
                 Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                  Assert.That(((ApiResult<Template>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<Template>)expected.Value).ListData.Count));
            }
            finally
            { //clear data
                _data.ClearTestData();
            }
        }
        [Test]
        public void testDeleteChecklistTemplate()
        {
            //create data
            //test
            try
            {
                int tempId = 20;
                var actual = _service.DeleteTemplate(tempId);
                var okResult = actual as OkObjectResult;
                OkObjectResult expected = new OkObjectResult(new ApiResult<ChecklistType>
                {
                    Status = 200,
                    Message = "Save successfully."
                });
                Assert.IsNotNull(okResult);
                Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                Assert.That(((ApiResult<Template>)okResult.Value).Message, Is.EqualTo(((ApiResult<ChecklistType>)expected.Value).Message));
                //clear data
            }
            finally
            {
                _data.ClearTestData();
            }
        }
       
        [Test]
        public void testGetTemplateByCampus()
        {
            //create data
            //test
            try
            {
                 string campus = "FU-HL";
                  var actual = _service.GetTemplates(campus);
                  var okResult = actual as OkObjectResult;
                  OkObjectResult expected = new OkObjectResult(new ApiResult<Template>
                  {
                      Status = 200,
                      ListData = ChecklistData.templates
                  });
                  Assert.IsNotNull(okResult);
                  Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                  Assert.That(((ApiResult<Template>)okResult.Value).ListData.Count, Is.EqualTo(((ApiResult<Template>)expected.Value).ListData.Count));
                  
            }
            finally
            {//clear data
                _data.ClearTestData();
            }
        }
        [Test]
        public void testUpdateItem()
        {
            //create data
            //test
            try
            {
                Item UpdateItem = ChecklistData.item1;
                var actual = _service.UpdateItem(UpdateItem);
                var okResult = actual as OkObjectResult;
                OkObjectResult expected = new OkObjectResult(new ApiResult<ChecklistType>
                {
                    Status = 200,
                    Message = "Save successfully."
                });
                Assert.IsNotNull(okResult);
                Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                Assert.That(((ApiResult<Item>)okResult.Value).Message, Is.EqualTo(((ApiResult<ChecklistType>)expected.Value).Message));
               
            }
            finally
            { //clear data
                _data.ClearTestData();
            }
        }
        [Test]
        public void testCreateNew()
        {
            //create data
            //test
            try
            {
                {
                    var actual = _service.CreateNewItem(ChecklistData.importModel51);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<ImportModel>)okResult.Value).Message, Is.EqualTo(((ApiResult<Checklist>)expected.Value).Message));
                }
                {
                    var actual = _service.CreateNewItem(ChecklistData.importModel52);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<ImportModel>)okResult.Value).Message, Is.EqualTo(((ApiResult<Checklist>)expected.Value).Message));
                }
                {
                    var actual = _service.CreateNewItem(ChecklistData.importModel53);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<ImportModel>)okResult.Value).Message, Is.EqualTo(((ApiResult<Checklist>)expected.Value).Message));
                }
                {
                    var actual = _service.CreateNewItem(ChecklistData.importModel54);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<ImportModel>)okResult.Value).Message, Is.EqualTo(((ApiResult<Checklist>)expected.Value).Message));
                }
                {
                    var actual = _service.CreateNewItem(ChecklistData.importModel55);
                    var okResult = actual as OkObjectResult;
                    OkObjectResult expected = new OkObjectResult(new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Save successfully."
                    });
                    Assert.IsNotNull(okResult);
                    Assert.That(okResult.StatusCode, Is.EqualTo(expected.StatusCode));
                    Assert.That(((ApiResult<ImportModel>)okResult.Value).Message, Is.EqualTo(((ApiResult<Checklist>)expected.Value).Message));
                }
            }
            finally
            {//clear data
                _data.ClearTestData();
            }
        }
    }
}
