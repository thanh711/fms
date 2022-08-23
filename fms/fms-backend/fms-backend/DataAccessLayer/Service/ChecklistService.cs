using Dapper;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Checklist;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Service
{
    public class ChecklistService : IChecklistService
    {
        private readonly BaseService<Checklist> _baseService;
        private readonly ChecklistComponentService _checklistComponentService;
        private readonly ChecklistItemService _checklistItemService;
        private readonly ChecklistTemplateService _checklistTemplateService;
        public ChecklistService(IConfiguration configuration)
        {
            _checklistTemplateService = new ChecklistTemplateService(configuration);
            _baseService = new BaseService<Checklist>(configuration);
            _checklistComponentService = new ChecklistComponentService(configuration);
            _checklistItemService = new ChecklistItemService(configuration);
        }

        public ApiResult<Checklist> GetList(ChecklistSearchModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", model.paging.CurrentPage);
            parameters.Add("@PageSize", model.paging.PageSize);
            parameters.Add("@CampusName", model.CampusName);
            parameters.Add("@LocationCode", model.LocationCode);
            parameters.Add("@ChecklistType", model.ChecklistType);
            parameters.Add("@Status", model.Status);
            parameters.Add("@FromDate", model.FromDate);
            parameters.Add("@ToDate", model.ToDate);

            return _baseService.GetList(model.paging, StoredProcedure.Checklist_GetList, parameters);
        }

        public ApiResult<WeeklyChecklistModel> GetWeeklyChecklist(WeeklyChecklistSearchModel model)
        {
            List<WeeklyChecklistModel> list = new List<WeeklyChecklistModel>();
            var componentRes = _checklistComponentService.GetListByTemplate(model.TemplateID);
            if(componentRes.Status == 200)
            {
                foreach(Component component in componentRes.ListData)
                {
                    WeeklyChecklistModel entity = new WeeklyChecklistModel
                    {
                        ChecklistType = component.Name,
                        Details = new List<FloorChecklistModel>()
                    };
                    bool entityStatus = true;
                    // get by each floor
                    int maxValue = component.EffectArea.Equals("Inside") ? Constants.FLOOR_MAX : 1;
                    for (int i = 1; i <= maxValue; i++)
                    {
                        FloorChecklistModel floorChecklist = new FloorChecklistModel
                        {
                            Floor = i
                        };

                        string regex = model.LocationCode + (component.EffectArea.Equals("Inside")? "-" + i + Constants.REGEX_ROOM : component.EffectArea.Equals("WC") ? Constants.REGEX_WC : Constants.REGEX_HLCT);
                        List<Checklist> checklists = GetDetailChecklists(model, component.ID, regex);

                        if (checklists != null && checklists.Count() > 0)
                        {
                            floorChecklist.Status = GetStatus(checklists);
                            if (floorChecklist.Status.Equals("NOK")) { entityStatus = false; }
                            floorChecklist.AreaDetails = checklists;
                            entity.Details.Add(floorChecklist);
                        }
                    }

                    // set entity
                    entity.Status = entityStatus ? "OK" : "NOK";
                    list.Add(entity);
                }

                return new ApiResult<WeeklyChecklistModel>
                {
                    Status = 200,
                    ListData = list
                };
            }
            return new ApiResult<WeeklyChecklistModel>
            {
                Status = 400,
                Message = componentRes.Message
            };

            
        }

        public List<Checklist> GetDetailChecklists(WeeklyChecklistSearchModel model, int type, string regex)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Code", model.LocationCode);
            parameters.Add("@FromDate", model.FromDate);
            parameters.Add("@EndDate", model.EndDate);
            parameters.Add("@Type", type);
            parameters.Add("@Regex", regex);
            return _baseService.GetListUnReturnTotal(StoredProcedure.Checklist_GetWeekly, parameters).ListData;
        }

        public string GetStatus(List<Checklist> list)
        {
            string status = "OK";
            foreach (Checklist checklist in list)
            {
                if (checklist.Status.Equals("NOK"))
                {
                    status = "NOK";
                    break;
                }
            }
            return status;
        }

        public ApiResult<ChecklistDetailApiResult> GetDetail(string checklistId)
        {

            var checklistData = GetChecklistBasicInfo(checklistId);
            if (checklistData.Data != null)
            {
                ChecklistDetailApiResult apiResult = new ChecklistDetailApiResult
                {
                    ChecklistInfo = checklistData.Data
                };

                if (checklistData.Data.TypeID == 2)
                {
                    List<Component> componentList = new List<Component>();

                    var components = _checklistComponentService.GetListByTemplate(checklistData.Data.TemplateID);
                    if (components.Status == 200)
                    {
                        foreach (Component com in components.ListData)
                        {
                            Component component = com;
                            bool comStatus = true;
                            var res = _checklistItemService.GetListItem(com.ID, checklistId);
                            component.ItemList = res.ListData;
                            foreach (Item i in component.ItemList)
                            {
                                var reqs = i.Requirements.Split(';').ToList();
                                var nok = string.IsNullOrEmpty(i.NOK) ? null : i.NOK.Split(';').ToList();
                                List<Requirement> requires = new List<Requirement>();
                                foreach (string req in reqs)
                                {
                                    bool value = true;
                                    if (nok != null)
                                    {

                                        foreach (string no in nok)
                                        {
                                            if (req.Trim().Equals(no.Trim()))
                                            {
                                                value = false;
                                            }
                                        }
                                    }
                                    comStatus &= value;
                                    requires.Add(new Requirement { Name = req.Trim(), Value = value });
                                }
                                i.RequirementList = requires;
                            }
                            component.Status = comStatus ? "OK" : "NOK";
                            componentList.Add(component);
                        }
                        apiResult.ComponentList = componentList;
                        apiResult.ChecklistInfo.IssuesList = !string.IsNullOrEmpty(apiResult.ChecklistInfo.Issues) ?
                        apiResult.ChecklistInfo.Issues.Split(';').ToList() : null;


                        return new ApiResult<ChecklistDetailApiResult>
                        {
                            Data = apiResult,
                            Status = 200
                        };
                    }

                    else
                    {
                        

                        return new ApiResult<ChecklistDetailApiResult>
                        {
                            Status = components.Status,
                            Message = components.Message
                        };
                    }
                }
                else
                {
                    List<Component> componentList = new List<Component>();
                    // xử lý cho phần weekly
                    var componentRes = _checklistComponentService.GetById(checklistData.Data.ComponentID);
                    if (componentRes.Status == 200)
                    {
                        Component component = componentRes.Data;
                        bool comStatus = true;
                        var res = _checklistItemService.GetListItem(component.ID, checklistId);
                        component.ItemList = res.ListData;
                        foreach (Item i in component.ItemList)
                        {
                            var reqs = i.Requirements.Split(';').ToList();
                            var nok = string.IsNullOrEmpty(i.NOK) ? null : i.NOK.Split(';').ToList();
                            List<Requirement> requires = new List<Requirement>();
                            foreach (string req in reqs)
                            {
                                bool value = true;
                                if (nok != null)
                                {

                                    foreach (string no in nok)
                                    {
                                        if (req.Trim().Equals(no.Trim()))
                                        {
                                            value = false;
                                        }
                                    }
                                }
                                comStatus &= value;
                                requires.Add(new Requirement { Name = req.Trim(), Value = value });
                            }
                            i.RequirementList = requires;
                        }
                        component.Status = comStatus ? "OK" : "NOK";
                        componentList.Add(component);
                    }
                    apiResult.ComponentList = componentList;
                    apiResult.ChecklistInfo.IssuesList = !string.IsNullOrEmpty(apiResult.ChecklistInfo.Issues) ?
                    apiResult.ChecklistInfo.Issues.Split(';').ToList() : null;
                    return new ApiResult<ChecklistDetailApiResult>
                    {
                        Data = apiResult,
                        Status = 200
                    };
                }
            }
            return new ApiResult<ChecklistDetailApiResult>
            {
                Status = 400,
                Message = "Not found detail"
            };
        }

        public ApiResult<Checklist> GetChecklistBasicInfo(string id)
        {
            string query = "SELECT CK.*, T.TypeID, L.[Name] AS LocationName, A.[Name] AS AreaName, C.[Name] AS CampusName " +
                "FROM [Checklists.Checklists] CK " +
                "INNER JOIN[Checklists.Templates] T ON CK.TemplateID = T.ID " +
                "INNER JOIN[Configuration.Locations] L ON CK.LocationID = L.ID " +
                "INNER JOIN[Configuration.Campuses] C ON L.CampusID = C.ID " +
                "LEFT JOIN[Configuration.Areas] A ON CK.AreaID = A.ID WHERE CK.ID = '" + id + "'";
            return _baseService.GetBy(query);
        }
        public ApiResult<Checklist> SaveChecklist(ChecklistDetailApiResult model)
        {
            // call Proc_Checklist_SaveSummary 
            var parameters = new DynamicParameters();
            parameters.Add("@ID", model.ChecklistInfo.ID);
            parameters.Add("@Status", string.IsNullOrEmpty(model.ChecklistInfo.Status) ? 1 : (model.ChecklistInfo.Status.Equals("OK") ? 1 : 0));
            parameters.Add("@Issues", model.ChecklistInfo.IssuesList != null ? string.Join(";", model.ChecklistInfo.IssuesList) : null);
            parameters.Add("@Technican", model.ChecklistInfo.Technician);
            parameters.Add("@Review", model.ChecklistInfo.Review);
            parameters.Add("@Solution", model.ChecklistInfo.Solution);
            var res = _baseService.Save(StoredProcedure.Checklist_SaveSummary, parameters);
            if (res.Status == 200)
            {
                // foreach detail call Proc_Checklist_SaveItem
                foreach (Component component in model.ComponentList)
                {
                    foreach (Item detail in component.ItemList)
                    {
                        var pars = new DynamicParameters();
                        pars.Add("@checklistId", model.ChecklistInfo.ID);
                        pars.Add("@status", detail.Status.Equals("OK") ? 1 : 0);
                        pars.Add("@nok", detail.NOK);
                        pars.Add("@itemId", detail.ID);
                        pars.Add("@note", detail.Note);
                        pars.Add("@typeUpdate", model.ChecklistInfo.CheckingDate != null ? 1 : 0);
                        var resl = _baseService.Save(StoredProcedure.Checklist_SaveDetail, pars);
                        if (resl.Status == 400)
                        {
                            return new ApiResult<Checklist>
                            {
                                Status = 400,
                                Message = resl.Message
                            };
                        }
                    }

                }
                return new ApiResult<Checklist>
                {
                    Status = 200,
                    Message = Constants.MESS_SAVE_SUS
                };
            }
            return new ApiResult<Checklist>
            {
                Status = 400,
                Message = ""
            };
        }

        public ApiResult<ImportModel> SaveImport(ImportModel model)
        {
            // 

            if (model.ComponentId == null && !string.IsNullOrEmpty(model.ComponentName))
            {
                Component component = _checklistComponentService.Create(model.ComponentName);
                if (component != null)
                {
                    model.ComponentId = component.ID;
                }
                else
                {
                    return new ApiResult<ImportModel>
                    {
                        Status = 400,
                        Message = "Something wrong at create new component."
                    };
                }
            }



            if (model.ComponentId > 0)
            {
                if (model.TypeUpdate == 1)
                {
                    bool res = _checklistItemService.DeleteAllItem(model.ComponentId);
                    if (!res)
                    {
                        return new ApiResult<ImportModel>
                        {
                            Status = 400,
                            Message = "Something wrong at clear item."
                        };
                    }
                }
                foreach (Item i in model.ListItem)
                {
                    i.ComponentID = model.ComponentId;
                    bool res = _checklistItemService.Create(i);
                    if (!res)
                    {
                        return new ApiResult<ImportModel>
                        {
                            Status = 400,
                            Message = "Something wrong at insert Item: " + i.ToString()
                        };
                    }
                }
            }
            return new ApiResult<ImportModel>
            {
                Status = 200,
                Message = Constants.MESS_SAVE_SUS,
                Data = model
            };
        }


        public ApiResult<CustomizeModel> GetCustomizeDetail(int tempId)
        {
            CustomizeModel model = new CustomizeModel
            {
                Component = _checklistComponentService.GetById(tempId).Data,
                Items = _checklistItemService.GetByID(tempId)
            };
            if (model.Items != null)
            {
                foreach (Item i in model.Items)
                {
                    i.ListReqs = i.Requirements.Split(';').ToList();
                }
            }
            return new ApiResult<CustomizeModel>
            {
                Status = 200,
                Data = model
            };
        }

        public ApiResult<ImportModel> CreateNew(ImportModel model)
        {
            if (model.ComponentId == null && !string.IsNullOrEmpty(model.ComponentName))
            {
                Component component = _checklistComponentService.Create(model.ComponentName);
                if (component != null)
                {
                    model.ComponentId = component.ID;
                }
                else
                {
                    return new ApiResult<ImportModel>
                    {
                        Status = 400,
                        Message = "Something wrong at create new component."
                    };
                }
            }



            if (model.ComponentId > 0)
            {
                if (model.TypeUpdate == 1)
                {
                    bool res = _checklistItemService.DeleteAllItem(model.ComponentId);
                    if (!res)
                    {
                        return new ApiResult<ImportModel>
                        {
                            Status = 400,
                            Message = "Something wrong at clear item."
                        };
                    }
                }
                foreach (Item i in model.ListItem)
                {
                    i.ComponentID = model.ComponentId;
                    bool res = _checklistItemService.Create(i);
                    if (!res)
                    {
                        return new ApiResult<ImportModel>
                        {
                            Status = 400,
                            Message = "Something wrong at insert Item: " + i.ToString()
                        };
                    }
                }
            }
            return new ApiResult<ImportModel>
            {
                Status = 200,
                Message = Constants.MESS_SAVE_SUS,
                Data = model
            };
        }

        public ApiResult<Component> GetAllComponents()
        {
            return _checklistComponentService.GetAll();
        }

        public ApiResult<Component> SaveConfiguration(Component model)
        {
            return _checklistComponentService.SaveConfig(model);
        }
        public ApiResult<Component> DeleteComponent(int id)
        {
            return _checklistComponentService.Delete(id);

        }

        public ApiResult<TemplateDetail> GetTemplateBasicInfomation(int tempid)
        {
            return new ApiResult<TemplateDetail>
            {
                Status = 200,
                Data = new TemplateDetail
                {
                    Template = _checklistTemplateService.GetById(tempid).Data,
                    ComponentList = _checklistComponentService.GetListByTemplate(tempid).ListData
                }
            };
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
                    parameters.Add("@EffectArea",component.EffectArea);
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

        public ApiResult<Checklist> GetListByArea(TechnicalReportSearchModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", model.paging.CurrentPage);
            parameters.Add("@PageSize", model.paging.PageSize);
            parameters.Add("@CampusName", model.Campus);
            parameters.Add("@LocationCode", model.LocationCode);
            parameters.Add("@RoomCode", model.RoomCode);
            parameters.Add("@FromDate", model.FromDate);
            parameters.Add("@ToDate", model.ToDate);

            return _baseService.GetList(model.paging, StoredProcedure.TechReport_Checklist_GetList, parameters);
        }

    }
}
