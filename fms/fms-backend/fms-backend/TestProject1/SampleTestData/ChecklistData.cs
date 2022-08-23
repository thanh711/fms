using Dapper;
using DataAccessLayer;
using DataAccessLayer.Model.Checklist;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using DataAccessLayer.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.SampleTestData
{
    internal class ChecklistData
    {
        private readonly BaseService<Checklist> _baseService;
        private readonly ChecklistComponentService _checklistComponentService;
        private readonly ChecklistItemService _checklistItemService;
        private readonly ChecklistTemplateService _checklistTemplateService;
        public ChecklistData(IConfiguration configuration)
        {
            _checklistTemplateService = new ChecklistTemplateService(configuration);
            _baseService = new BaseService<Checklist>(configuration);
            _checklistComponentService = new ChecklistComponentService(configuration);
            _checklistItemService = new ChecklistItemService(configuration);
        }

        public void ClearTestData()
        {
            string query = "delete from [Checklists.Items] where [Requirements] like 'ThanhNC-Test%'";
            _baseService.Delete(query);
            string query4 = " delete from [Checklists.Components] where [Name] like 'ThanhNC-Test%'";
            _baseService.Delete(query4);
            string query2 = "delete from [Checklist.Templates] where [Name] like 'ThanhNC-Test%'";
            _baseService.Delete(query2);
            string query3 = " delete from [Configuration.ChecklistTypes] where [Name] like 'ThanhNC-Test%'";
            _baseService.Delete(query3);
            string query5 = "delete from [Checklists.Results] where ChecklistID like'%2021%'";
            _baseService.Delete(query5);
            string query6 = "delete from [Checklists.Checklists] where id like'%2021%'";
            _baseService.Delete(query6);
        }
        public void CreateTestData()
        {
            _checklistComponentService.Create("ThanhNC-Test-Component");
            int Componentid = 0;
            foreach (var item in items1)
            {
                item.ComponentID = Componentid;
                _checklistItemService.Create(item);
            }
            CreateChecklist(create1); CreateChecklist(create2); CreateChecklist(create3);
        }
        public ApiResult<Checklist> CreateChecklist(CreateChecklistModel model)
        {
            if (model.Template?.TypeID == 2)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@tempId", model.Template.ID);
                parameters.Add("@EffectCampus", model.Template.EffectCampus);
                parameters.Add("@EffectLocation", model.Template.EffectLocation);
                parameters.Add("@From", model.From);
                parameters.Add("@To", model.To);

                return _baseService.Save(StoredProcedure.Checklist_CreateChecklist_Daily, parameters);
            }
            else
            {
                var parameters = new DynamicParameters();
                bool status = true;
                foreach (Component component in model.ComponentList)
                {
                    parameters.Add("@tempId", model.Template.ID);
                    parameters.Add("@compId", component.ID);
                    parameters.Add("@EffectCampus", model.Template.EffectCampus);
                    parameters.Add("@EffectLocation", model.Template.EffectLocation);
                    parameters.Add("@EffectArea", component.EffectArea);
                    parameters.Add("@From", model.From);
                    parameters.Add("@To", model.To);
                    var res = _baseService.Save(StoredProcedure.Checklist_CreateChecklist_Weekly, parameters);
                    status &= res.Status == 200;
                }
                if (status)
                {
                    return new ApiResult<Checklist>
                    {
                        Status = 200,
                        Message = "Create successfully."
                    };
                }
                else
                {
                    return new ApiResult<Checklist>
                    {
                        Status = 400,
                        Message = "Create failed."
                    };
                }
            }
        }
        //save import
        internal static Item item1 = new Item { Name = "ThanhNC-Test-item1", Requirements = "ThanhNC-Test-Requirements1", DefaultValue = true };
        internal static Item item2 = new Item { Name = "ThanhNC-Test-item2", Requirements = "ThanhNC-Test-Requirements2", DefaultValue = true };
        internal static Item item3 = new Item { Name = "ThanhNC-Test-item3", Requirements = "ThanhNC-Test-Requirements3", DefaultValue = false };
        internal static Item item4 = new Item { Name = "ThanhNC-Test-item4", Requirements = "ThanhNC-Test-Requirements4", DefaultValue = true };
        internal static List<Item> items1 = new List<Item> { item1, item2, item3, item4 };
        internal static ImportModel importModel = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel2 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel3 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel4 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel5 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        //create new
        internal static ImportModel importModel11 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel12 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel13 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel14 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel15 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        //create new2
        internal static ImportModel importModel41 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel42 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel43 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel44 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel45 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        //create new3
        internal static ImportModel importModel51 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel52 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel53 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel54 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel55 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        //SaveImport
        internal static ImportModel importModel21 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel22 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel23 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel24 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel25 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        //SaveImport2
        internal static ImportModel importModel31 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel32 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel33 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel34 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        internal static ImportModel importModel35 = new ImportModel
        {
            ListItem = items1,
            ComponentName = "ThanhNC-Test"
        };
        //save checklist
        internal static Item item11 = new Item { ID = 366, Status = "OK", Name = "ThanhNC1-Test-item1", Requirements = "ThanhNC1-Test-Requirements1", DefaultValue = true };
        internal static Item item12 = new Item { ID = 367,  Status = "OK", Name = "ThanhNC1-Test-item2", Requirements = "ThanhNC1-Test-Requirements2", DefaultValue = true };
        internal static Item item13 = new Item { ID = 368,  Status = "NOK", Name = "ThanhNC1-Test-item3", Requirements = "ThanhNC1-Test-Requirements3", DefaultValue = false };
        internal static Item item14 = new Item { ID = 369,  Status = "NOK", Name = "ThanhNC1-Test-item4", Requirements = "ThanhNC1-Test-Requirements4", DefaultValue = true };
        internal static List<Item> items2 = new List<Item> { item11, item12, item13, item14 };
        internal static Checklist checklist1 = new Checklist
        {
            ID = "checklist_w7_4_20210808_20220815_AL_9",
            Status = "OK",
            IssuesList = null,
            Technician = "thanhnche140350",
            Review = "có vết ố vàng",
            Solution = "Vệ sinh sạch sẽ",
            CheckingDate = DateTime.Parse("2022-07-24T22:05:37.757"),
        };

        internal static Component component1 = new Component
        {
            ID = 46,
            ItemList = items2,
        };
        internal static Component component2 = new Component
        {
            ItemList = items2,
        };
        internal static Component component3 = new Component
        {
            ItemList = items2,
        };
        internal static List<Component> components1 = new List<Component>
        {
            component1,component2,component3
        };
        internal static List<Component> components2 = new List<Component>
        {
            component1,component2
        };
        internal static List<Component> components3 = new List<Component>
        {
            component1
        };
        internal static ChecklistDetailApiResult apiResult1 = new ChecklistDetailApiResult
        {

            ChecklistInfo = checklist1,
            ComponentList = components1
        };
        internal static ChecklistDetailApiResult apiResult2 = new ChecklistDetailApiResult
        {
            ChecklistInfo = checklist1,
            ComponentList = components2
        };
        internal static ChecklistDetailApiResult apiResult3 = new ChecklistDetailApiResult
        {
            ChecklistInfo = checklist1,
            ComponentList = components3
        };
        //get list
        internal static CustomizeModel customize = new CustomizeModel
        {
            Component = component1,
            Items = items2,
        };

        //get detail
        internal static Checklist checklistd1 = new Checklist
        {
            ID = "checklist_6_20210811_20210811_DE",
            Status = "OK",
            IssuesList = null,
            Technician = "thanhnche140350",
            Review = "có vết ố vàng",
            Solution = "Vệ sinh sạch sẽ",
            CheckingDate = DateTime.Parse("2022-07-24T22:05:37.757"),
        };
        internal static Checklist checklistd2 = new Checklist
        {
            ID = "checklist_6_20210812_20210812_DE",
            Status = "OK",
            IssuesList = null,
            Technician = "thanhnche140350",
            Review = "có vết ố vàng",
            Solution = "Vệ sinh sạch sẽ",
            CheckingDate = DateTime.Parse("2022-07-24T22:05:37.757"),
        };
        internal static Checklist checklistd3 = new Checklist
        {
            ID = "checklist_6_20210813_20210813_DE",
            Status = "OK",
            IssuesList = null,
            Technician = "thanhnche140350",
            Review = "có vết ố vàng",
            Solution = "Vệ sinh sạch sẽ",
            CheckingDate = DateTime.Parse("2022-07-24T22:05:37.757"),
        };
        internal static Checklist checklistd4 = new Checklist
        {
            ID = "checklist_14_20220812_20220812BE",
            Status = "OK",
            IssuesList = null,
            Technician = "thanhnche140350",
            Review = "có vết ố vàng",
            Solution = "Vệ sinh sạch sẽ",
            CheckingDate = DateTime.Parse("2022-07-24T22:05:37.757"),
        };
        internal static Checklist checklistl1 = new Checklist
        {
            ID = "checklist_6_20210811_20210811_DE",
            Status = "OK",
        };
        internal static ChecklistDetailApiResult apiResultd1 = new ChecklistDetailApiResult
        {
            ChecklistInfo = checklistd1,
            ComponentList = components1
        };
        internal static ChecklistDetailApiResult apiResultd2 = new ChecklistDetailApiResult
        {

            ChecklistInfo = checklistd2,
            ComponentList = components1
        };
        internal static ChecklistDetailApiResult apiResultd3 = new ChecklistDetailApiResult
        {

            ChecklistInfo = checklistd3,
            ComponentList = components1
        };
        internal static ChecklistDetailApiResult apiResultd4 = new ChecklistDetailApiResult
        {

            ChecklistInfo = checklistl1,
            ComponentList = components1
        };

        //GetWeeklyChecklist
        internal static WeeklyChecklistSearchModel weekly1 = new WeeklyChecklistSearchModel
        {
            TemplateID = 17,
        };
        internal static WeeklyChecklistSearchModel weekly2 = new WeeklyChecklistSearchModel
        {
            TemplateID = 7,
        };
        internal static WeeklyChecklistSearchModel weekly3 = new WeeklyChecklistSearchModel
        {
            TemplateID = 15,
        };
        internal static WeeklyChecklistModel weeklyr1 = new WeeklyChecklistModel
        {
            ChecklistType = "demo",
            Status = "OK",
        };
        internal static WeeklyChecklistModel weeklyr2 = new WeeklyChecklistModel
        {
            ChecklistType = "ThanhNC1-Test",
            Status = "OK",
        };
        internal static WeeklyChecklistModel weeklyr3 = new WeeklyChecklistModel
        {
            ChecklistType = "CÁC PHÒNG BÊN TRONG TOÀ NHÀ",
            Status = "OK",
        };
        internal static WeeklyChecklistModel weeklyr4 = new WeeklyChecklistModel
        {
            ChecklistType = "CÁC PHÒNG WC",
            Status = "OK",
        };
        internal static WeeklyChecklistModel weeklyr5 = new WeeklyChecklistModel
        {
            ChecklistType = "KHU VỰC HÀNH LANG, CẦU THANG",
            Status = "OK",
        };
        internal static WeeklyChecklistModel weeklyr6 = new WeeklyChecklistModel
        {
            ChecklistType = "Admin test hệ thống 3",
            Status = "OK",
        };
        internal static List<WeeklyChecklistModel> weeklyChecklistModels1 = new List<WeeklyChecklistModel> { weeklyr1, weeklyr2 };
        internal static List<WeeklyChecklistModel> weeklyChecklistModels2 = new List<WeeklyChecklistModel> { weeklyr3, weeklyr4, weeklyr5 };
        internal static List<WeeklyChecklistModel> weeklyChecklistModels3 = new List<WeeklyChecklistModel> { weeklyr6 };

        //get list
        internal static Template template1 = new Template
        {
            ID = 6,
            TypeID = 2,
            EffectCampus = "FU-HL",
            EffectLocation = "DE"
        };
        internal static CreateChecklistModel create1 = new CreateChecklistModel
        {
            Template = template1,
            From = DateTime.Parse("2021-08-11 00:00:00.000"),
            To = DateTime.Parse("2021-08-11 00:00:00.000"),
        };
        internal static CreateChecklistModel create2 = new CreateChecklistModel
        {
            Template = template1,
            From = DateTime.Parse("2021-08-12 00:00:00.000"),
            To = DateTime.Parse("2021-08-12 00:00:00.000"),
        };
        internal static CreateChecklistModel create3 = new CreateChecklistModel
        {
            Template = template1,
            From = DateTime.Parse("2021-08-13 00:00:00.000"),
            To = DateTime.Parse("2021-08-13 00:00:00.000"),
        };
        
        internal static Checklist checklistl2 = new Checklist
        {
            ID = "checklist_6_20210812_20210812_DE",
            Status = "OK",
        };
        internal static Checklist checklistl3 = new Checklist
        {
            ID = "checklist_6_20210813_20210813_DE",
            Status = "OK",
        };
        internal static List<Checklist> checklists =new List<Checklist> { checklistl1 , checklistl2 , checklistl3 };
        //get template by campus
        internal static Template templatet1 = new Template
        {
            EffectCampus = "FU-HL",
            EffectLocation = "AL",
            Name= "Admin test hệ thống"

        };
        internal static Template templatet2 = new Template
        {
            EffectCampus = "FU-HL",
            EffectLocation = "BE",
            Name = "Phiếu kiểm tra hàng tuần các tòa nhà"
        };
        internal static Template templatet3= new Template
        {
            EffectCampus = "FU-HL",
            EffectLocation = "DE",
            Name = "Phiếu kiểm tra hàng ngày - Máy bơm nước"
        };
        internal static Template templatet4 = new Template
        {
            EffectCampus = "FU-HL",
            EffectLocation = "DomA",
            Name = "Phiếu kiểm tra hàng ngày - Máy phát điện"
        };
        internal static Template templatet5 = new Template
        {
            EffectCampus = "FU-HL",
            EffectLocation = "DomA",
            Name = "Phiếu kiểm tra hàng ngày - Trạm biến áp"
        };
        internal static Template templatet6 = new Template
        {
            EffectCampus = "FU-HL",
            EffectLocation = "DomA",
            Name = "Phiếu kiểm tra hàng ngày - Trục điện kỹ thuật"
        };
        internal static Template templatet7 = new Template
        {
            EffectCampus = "FU-HL",
            EffectLocation = "DomA",
            Name = "Phiếu kiểm tra hàng ngày - Tụ điện"
        };
        internal static Template templatet8 = new Template
        {
            EffectCampus = "FU-HL",
            EffectLocation = "DomA",
            Name = "Admin test hệ thống - Daily 1"
        };
        internal static Template templatet9 = new Template
        {
            EffectCampus = "FU-HL",
            EffectLocation = "DomA",
            Name = "Admin test hệ thống - Daily 2"
        };
        internal static Template templatet10 = new Template
        {
            EffectCampus = "FU-HL",
            EffectLocation = "DomA",
            Name = "Admin test hệ thống - Daily 3"
        };
        internal static List<Template> templates = new List<Template>
        {
            templatet1,templatet2,templatet3,templatet4,templatet5,
            templatet6,templatet7,templatet8,templatet9,templatet10,
        };
    }
}
