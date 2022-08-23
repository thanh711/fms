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
    public class HistoryService
    {
        private string _cnnString;
        private readonly IConfiguration _configuration;
        private BaseService<HistoryOfChange> _baseService;

        public HistoryService(IConfiguration configuration)
        {
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DB");
            _baseService = new BaseService<HistoryOfChange>(configuration);
        }

        public bool InsertHistory(HistoryOfChange history)
        {
            string query = "INSERT INTO [Trouble.HistoryChanges] ([ID],[ReportID],[ChangeContent],[Created],[CreateBy],[ChangeDetail]) " +
                "VALUES('" + RenderID(history.ReportID) + "', " + history.ReportID + ", N'" + history.ChangeContent + "', GETDATE(), '" + history.CreateBy + "', N'" + history.ChangeDetail + "')";
            var res = _baseService.SaveByQuery(query);
            return res.Status == 200;
        }

        public string RenderID(int ReportId)
        {
            return "HOF_" + ReportId + "_" + DateTime.Now.Ticks;
        }

        public ApiResult<HistoryOfChange> GetListHistory(int reportId)
        {
            string query = "SELECT * FROM [Trouble.HistoryChanges] WHERE [ReportID] = " + reportId;
            var res = _baseService.GetAll(query);
            return res;
        }

    }
}
