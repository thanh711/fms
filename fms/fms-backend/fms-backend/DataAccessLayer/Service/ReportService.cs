using Dapper;
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
    public class ReportService
    {
        private readonly BaseService<Report> _baseService;
        private readonly BaseService<CountModel> _countService;

        public ReportService(IConfiguration configuration)
        {
            _baseService = new BaseService<Report>(configuration);
            _countService = new BaseService<CountModel>(configuration);
        }

        public ApiResult<Report> Save(Report model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ID", model.ID);
            parameters.Add("@AreaID", model.AreaID);
            parameters.Add("@Summary", model.Summary);
            parameters.Add("@Description", model.Description);
            parameters.Add("@Emergency", model.Emergency);
            parameters.Add("@WorkflowID", model.WorkflowID);
            if (model.InAreaTime != null)
            {
                DateTime date = (DateTime)model.InAreaTime;
                date = date.AddHours(7);
                parameters.Add("@InAreaTime", date);
            }
            else
            {
                parameters.Add("@InAreaTime", model.InAreaTime);
            }

            parameters.Add("@CreateBy", model.CreatedBy);

            return _baseService.SaveReturnEntity(StoredProcedure.SaveTrouble, parameters);
        }

        public ApiResult<Report> GetById(int ReportId)
        {
            string query = "SELECT rep.*, area.[Name] as AreaName, loc.Code as LocationCode, cam.[Name] as CampusName, wf.Step," +
                "(CASE WHEN rep.Finished <> NULL THEN DATEDIFF(HOUR,rep.Finished,GETDATE()) ELSE DATEDIFF(HOUR,rep.Created,GETDATE()) END) AS SLA " +
                "FROM[Trouble.Reports] rep " +
                "INNER JOIN[Configuration.Areas] area ON rep.AreaID = area.ID " +
                "INNER JOIN[Configuration.Locations] loc ON area.LocationID = loc.ID " +
                "INNER JOIN[Configuration.Campuses] cam ON loc.CampusID = cam.ID " +
                "INNER JOIN[Configuration.Workflows] wf ON rep.WorkflowID = wf.ID " +
                "WHERE rep.ID = " + ReportId;
            return _baseService.GetBy(query);
        }

        public ApiResult<Report> ChangeWorkflow(int reportId, int workflowId)
        {
            string query = "UPDATE [Trouble.Reports] SET [WorkflowID] = " + workflowId + " WHERE ID = " + reportId;
            if (workflowId == 5)
            {
                query = "UPDATE [Trouble.Reports] SET [WorkflowID] = " + workflowId + " , Finished = GETDATE() WHERE ID = " + reportId;
            }
            return _baseService.SaveByQuery(query);
        }

        public ApiResult<CountModel> CountReports(TroubleSearchModel model)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@roleId", model.RoleID);
            parameters.Add("@campusName", model.Campus);
            parameters.Add("@username", model.User);
            return _countService.GetList(model.paging, StoredProcedure.Trouble_Count, parameters);
        }

    }
}

