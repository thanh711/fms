using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Troubles;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Service
{
    public class TroubleshotingService
    {
        private readonly BaseService<TroubleShooting> _baseService;

        public TroubleshotingService(IConfiguration configuration)
        {
            _baseService = new BaseService<TroubleShooting>(configuration);
        }

        public ApiResult<TroubleShooting> GetByReportID(int reportId)
        {
            string query = "SELECT * FROM [Trouble.Troubleshootings] WHERE ReportID = " + reportId;
            return _baseService.GetBy(query);
        }

        public ApiResult<TroubleShooting> Update(TroubleShooting model)
        {
            string query = "UPDATE [Trouble.Troubleshootings] SET ";
            if (model.CategoryID > 0)
            {
                query += " [CategoryID] = " + model.CategoryID + " ,";
            }
            query += " [Resolved] = " + (model.Resolved == true ? 1 : 0) + " ,";
            if (model.Priority > 0)
            {
                query += " [Priority] = " + model.Priority + " ,";
            }
            if (!string.IsNullOrEmpty(model.IssueReview))
            {
                query += " [IssueReview] = " + "N'" + model.IssueReview + "'" + " ,";
            }
            if (!string.IsNullOrEmpty(model.Solution))
            {
                query += " [Solution] = " + "N'" + model.Solution + "'" + " ,";
            }
            query = query.Substring(0, query.Length - 2);
            query += " WHERE [ReportID] = " + model.ReportID;
            return _baseService.SaveByQuery(query);
        }
    }
}
