using Dapper;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using DataAccessLayer.Model.Troubles;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Service
{
    public class TroubleService : ITroubleService
    {
       
        private readonly BaseService<Trouble> _baseService;
        private readonly BaseService<TroubleListModel> _baseListService;
        private readonly ImageService _imageService;
        private readonly ReportService _reportService;
        private readonly TroubleshotingService _troubleshotingService;
        private readonly HistoryService _historyService;

        public TroubleService(IConfiguration configuration)
        {
           
            _baseService = new BaseService<Trouble>(configuration);
            _imageService = new ImageService(configuration);
            _reportService = new ReportService(configuration);
            _baseListService = new BaseService<TroubleListModel>(configuration);
            _troubleshotingService = new TroubleshotingService(configuration);
            _historyService = new HistoryService(configuration);
        }

        public ApiResult<Trouble> CreateTrouble(Trouble trouble)
        {
            Report report = null;
            try
            {
                bool isUpdate = true;
                string changeValues = "";
                if (trouble.Report.ID > 0)
                {
                    Report model = _reportService.GetById(trouble.Report.ID).Data;
                    if (model.WorkflowID > trouble.Report.WorkflowID)
                    {
                        isUpdate = false;
                        return new ApiResult<Trouble>
                        {
                            Message = "Report has changed. Press F5 to view changes",
                            Status = 400
                        };
                    }
                    changeValues = GetHistoryChangesOfReport(model, trouble.Report);

                }

                if (isUpdate)
                {
                    report = _reportService.Save(trouble.Report).Data;
                    if (report != null)
                    {
                        #region Save image changes of report
                        bool isImageChange = false;
                        foreach (Image image in trouble.ReportImage)
                        {
                            image.ReportID = report.ID;
                            if (!string.IsNullOrEmpty(image.ID))
                            {
                                if (!_imageService.CheckExist(image.ID))
                                {
                                    var res = _imageService.Save(image);
                                    isImageChange = true;
                                }
                            }

                        }
                        changeValues += isImageChange ? "Change [Image]; " : "";
                        #endregion
                    }
                    #region Save history change 
                    if (trouble.Report.WorkflowID > 0)
                    {
                        if (trouble.Report.ID != 0)
                        {
                            if (!string.IsNullOrEmpty(changeValues))
                            {
                                var saveHistory = _historyService.InsertHistory(new HistoryOfChange
                                {
                                    ReportID = report.ID,
                                    ChangeContent = trouble.Report.CreatedBy + " made changes",
                                    ChangeDetail = changeValues.Substring(0, changeValues.Length - 2),
                                    CreateBy = trouble.Report.CreatedBy
                                });
                            }

                        }
                        else
                        {
                            var saveHistory = _historyService.InsertHistory(new HistoryOfChange
                            {
                                ReportID = report.ID,
                                ChangeContent = trouble.Report.WorkflowID == 1 ? trouble.Report.CreatedBy + " create trouble at [Draft]" : trouble.Report.CreatedBy + " create trouble",
                                ChangeDetail = trouble.Report.ID == 0 ? "" : !string.IsNullOrEmpty(changeValues) ? changeValues.Substring(0, changeValues.Length - 2) : changeValues,
                                CreateBy = trouble.Report.CreatedBy
                            });
                        }
                    }
                    #endregion
                }

                return new ApiResult<Trouble>
                {
                    Status = 200,
                    Data = new Trouble { Report = report },
                    Message = "Create trouble successfully."
                };
            }
            catch (Exception e)
            {
                return new ApiResult<Trouble>
                {
                    Status = 400,
                    Message = e.Message
                };
            }


        }

        public ApiResult<TroubleListModel> GetList(TroubleSearchModel model)
        {
            var parameters = new DynamicParameters();

            #region Add Params
            parameters.Add("@PageIndex", model.paging.CurrentPage);
            parameters.Add("@PageSize", model.paging.PageSize);
            parameters.Add("@WorkflowId", model.WorkflowID);
            parameters.Add("@LocationCode", model.LocationCode);
            parameters.Add("@RoomCode", model.RoomCode);
            parameters.Add("@Role", model.RoleID);
            parameters.Add("@Campus", model.Campus);
            parameters.Add("@Role", model.RoleID);
            parameters.Add("@FromDate", model.FromDate);
            parameters.Add("@ToDate", model.ToDate);
            parameters.Add("@User", model.User);
            #endregion

            return _baseListService.GetList(model.paging, StoredProcedure.GetListTrouble, parameters);
        }

        public ApiResult<Trouble> GetByID(int reportId)
        {
            var report = _reportService.GetById(reportId);
            if (report.Status == 200)
            {
                #region Get list image of report
                var reportImages = _imageService.GetListImageReport(reportId);
                if (reportImages.Status == 200)
                {
                    #region Get troubleshot information of report
                    var troubleShot = _troubleshotingService.GetByReportID(reportId);
                    if (troubleShot.Status == 200)
                    {
                        #region Get list troubleshot images of report
                        var troubleImages = _imageService.GetListImageTroubleShoting(troubleShot.Data != null ? troubleShot.Data.ID : 0);
                        if (troubleImages.Status == 200)
                        {
                            return new ApiResult<Trouble>
                            {
                                Status = 200,
                                Data = new Trouble
                                {
                                    Report = report.Data,
                                    ReportImage = reportImages.ListData,
                                    Shooting = troubleShot.Data,
                                    ShootImage = troubleImages.ListData
                                }
                            };
                        }
                        else
                        {
                            return new ApiResult<Trouble>
                            {
                                Status = 400,
                                Message = troubleImages.Message
                            };
                        }
                        #endregion
                    }
                    return new ApiResult<Trouble>
                    {
                        Status = 400,
                        Message = troubleShot.Message
                    };
                    #endregion
                }
                else
                {
                    return new ApiResult<Trouble>
                    {
                        Status = 400,
                        Message = reportImages.Message
                    };
                }
                #endregion
            }
            else
            {
                return new ApiResult<Trouble>
                {
                    Status = 400,
                    Message = report.Message
                };
            }

        }

        public ApiResult<TroubleListModel> ChangeTechnician(TroubleListModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ReportID", model.ReportID);
            parameters.Add("@Technician", model.Technician);
            parameters.Add("@CreateBy", model.CreatedBy);

            var res = _baseListService.Save(StoredProcedure.Trouble_ChangeTechnician, parameters);
            if (res.Status == 200)
            {
                #region Save history changes
                HistoryOfChange history = new HistoryOfChange
                {
                    ReportID = model.ReportID,
                    CreateBy = model.CreatedBy,
                    ChangeContent = "Change technician of report",
                    ChangeDetail = "Technician assign to " + model.Technician + " by [" + model.CreatedBy + "]."
                };
                bool saveHis = _historyService.InsertHistory(history);
                if (saveHis)
                {
                    return new ApiResult<TroubleListModel>
                    {
                        Status = 200,
                        Message = Constants.MESS_SAVE_SUS
                    };
                }
                #endregion
            }
            return res;
        }

        public ApiResult<Trouble> CancelReport(int reportId)
        {
            string query = "UPDATE [Trouble.Reports] SET[WorkflowID] = 6 WHERE ID = " + reportId;
            var res = _baseService.ChangeActive(query);
            if (res.Status == 200)
            {
                #region Save history changes
                HistoryOfChange history = new HistoryOfChange
                {
                    ReportID = reportId,
                    ChangeContent = "Report was cancel",
                    ChangeDetail = "",
                    CreateBy = ""
                };
                bool saveHis = _historyService.InsertHistory(history);
                if (saveHis)
                {
                    return new ApiResult<Trouble>
                    {
                        Status = 200,
                        Message = Constants.MESS_SAVE_SUS
                    };
                }
                #endregion
            }
            return res;
        }

        public ApiResult<Trouble> DeleteReport(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ID", id);
            return _baseService.Save(StoredProcedure.Trouble_Delete, parameters);
        }

        public ApiResult<Trouble> UpdateTrouble(Trouble model)
        {
            try
            {
                string changeValues = "";
                var troubleShot = _troubleshotingService.GetByReportID(model.Shooting.ReportID).Data;
                var resl = _troubleshotingService.Update(model.Shooting);
                #region Add History Of Changes
                changeValues = GetHistoryChangesOfTroubleShoot(troubleShot, model.Shooting);
                #endregion
                if (resl.Status == 200)
                {
                    #region Save images
                    bool isImageChange = false;
                    foreach (Image image in model.ShootImage)
                    {
                        image.TroubleshotingID = model.Shooting.ID;
                        if (!string.IsNullOrEmpty(image.ID))
                        {
                            if (!_imageService.CheckExist(image.ID))
                            {
                                var res = _imageService.SaveTroubleshootingImage(image);
                                isImageChange = true;
                            }
                        }

                    }
                    changeValues += isImageChange ? "[Image changes]; " : "";
                    #endregion

                    #region Save history change 
                    if (!string.IsNullOrEmpty(changeValues))
                    {
                        HistoryOfChange history = new HistoryOfChange
                        {
                            ReportID = troubleShot.ReportID,
                            ChangeContent = model.Shooting.CreatedBy + " made changes",
                            ChangeDetail = changeValues.Substring(0, changeValues.Length - 2),
                            CreateBy = model.Shooting.CreatedBy
                        };
                        var saveHistory = _historyService.InsertHistory(history);
                        #endregion
                    }


                    var report = _reportService.GetById(model.Shooting.ReportID).Data;
                    if (report.WorkflowID != model.Report.WorkflowID)
                    {
                        var re = _reportService.ChangeWorkflow(model.Report.ID, model.Report.WorkflowID);
                        if (re.Status == 200)
                        {
                            HistoryOfChange his = new HistoryOfChange
                            {
                                ReportID = troubleShot.ReportID,
                                ChangeContent = model.Shooting.CreatedBy + " made change workflow",
                                ChangeDetail = "Status had changed from [" + Constants.STATUS[report.WorkflowID == 0 ? 0 : report.WorkflowID - 1] +
                                "] to [" + Constants.STATUS[model.Report.WorkflowID == 0 ? 0 : model.Report.WorkflowID - 1] + "]",
                                CreateBy = model.Shooting.CreatedBy
                            };
                            var saveHis = _historyService.InsertHistory(his);
                            if (saveHis)
                            {
                                return new ApiResult<Trouble>
                                {
                                    Status = 200,
                                    Message = "Save troubleshooting successfully."
                                };
                            }
                            else
                            {
                                return new ApiResult<Trouble>
                                {
                                    Status = 400,
                                    Message = "Error at save history."
                                };
                            }
                        }
                    }
                    return new ApiResult<Trouble>
                    {
                        Status = 200,
                        Message = "Save troubleshooting successfully."
                    };
                }
                return new ApiResult<Trouble>
                {
                    Status = resl.Status,
                    Message = resl.Message
                };


            }
            catch (Exception e)
            {
                return new ApiResult<Trouble>
                {
                    Status = 400,
                    Message = e.Message
                };
            }
        }

        public ApiResult<CountModel> CountReports(TroubleSearchModel model)
        {
            return _reportService.CountReports(model);
        }

        public ApiResult<HistoryOfChange> GetHistoryOfReport(int reportId)
        {
            return _historyService.GetListHistory(reportId);
        }

        public string GetHistoryChangesOfReport(Report oldValue, Report newValue)
        {
            string changeValues = "";
            changeValues += !oldValue.Summary.Equals(newValue.Summary) ? "Change [Summary]: " + oldValue.Summary + " -> " + newValue.Summary + "; " : "";

            if (oldValue.InAreaTime == null)
            {
                if (newValue.InAreaTime != null)
                {
                    changeValues += "Update [In Area Time]: " + newValue.InAreaTime + "; ";
                }
            }
            else
            {
                if (newValue.InAreaTime == null)
                {
                    changeValues += "Delete [In Area Time]; ";
                }
                else
                {
                    if (((DateTime)newValue.InAreaTime).AddHours(7) != oldValue.InAreaTime)
                    {
                        changeValues += "Change [In Area Time]: " + oldValue.InAreaTime + " -> " + newValue.InAreaTime + "; ";
                    }
                }
            }

            changeValues += (oldValue.Emergency != newValue.Emergency) ? "Change [Emergency]: " + (newValue.Emergency ? "checked" : "uncheck") + "; " : "";
            if (string.IsNullOrEmpty(oldValue.Description))
            {
                if (!string.IsNullOrEmpty(newValue.Description))
                {
                    changeValues += "Update [Description]: " + newValue.Description + "; ";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(oldValue.Summary))
                {
                    changeValues += "Delete [Description]; ";
                }
                else
                {
                    if (newValue.Description.Equals(oldValue.Description))
                    {
                        changeValues += "Change [Description]: " + oldValue.Description + " -> " + newValue.Description + "; ";
                    }
                }
            }
            return changeValues;
        }

        public string GetHistoryChangesOfTroubleShoot(TroubleShooting oldValue, TroubleShooting newValue)
        {
            string changeValues = "";
            changeValues += oldValue.CategoryID != newValue.CategoryID ? "Change [Category]; " : "";
            changeValues += oldValue.Priority != newValue.Priority ? "Change [Piority]; " : "";
            changeValues += oldValue.Resolved != newValue.Resolved ? "Change [Resolve]; " : "";
            if (!string.IsNullOrEmpty(newValue.IssueReview))
            {
                if (!string.IsNullOrEmpty(oldValue.IssueReview))
                {
                    changeValues += "Change [Issue Review]: "+ oldValue.IssueReview + " -> " + newValue.IssueReview +"; ";
                }
                else
                {
                    changeValues += "Change [Issue Review]; ";
                }
            }

            if (!string.IsNullOrEmpty(newValue.Solution))
            {
                if (!string.IsNullOrEmpty(oldValue.Solution))
                {
                    changeValues += "Change [Solution]: " + oldValue.Solution + " -> " + newValue.Solution + "; ";
                }
                else
                {
                    changeValues += "Change [Solution]; ";
                }
            }
            return changeValues;
        }

        public ApiResult<TroubleListModel> GetListForTechReport(TechnicalReportSearchModel model)
        {
            var parameters = new DynamicParameters();

            #region Add Params
            parameters.Add("@PageIndex", model.paging.CurrentPage);
            parameters.Add("@PageSize", model.paging.PageSize);
            parameters.Add("@WorkflowId", 0);
            parameters.Add("@LocationCode", model.LocationCode);
            parameters.Add("@RoomCode", model.RoomCode);
            parameters.Add("@Role", model.RoleID);
            parameters.Add("@Campus", model.Campus);
            parameters.Add("@Role", model.RoleID);
            parameters.Add("@FromDate", model.FromDate);
            parameters.Add("@ToDate", model.ToDate);
            parameters.Add("@User", model.User);
            #endregion

            return _baseListService.GetList(model.paging, StoredProcedure.GetListTrouble, parameters);
        }

    }
}

