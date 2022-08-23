using DataAccessLayer.IService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Service
{
    public class WorkflowService : IWorkflowService
    {
        private string _cnnString;
        private readonly IConfiguration _configuration;
        private BaseService<Workflow> _baseService;
        public WorkflowService(IConfiguration configuration)
        {
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DB");
            _baseService = new BaseService<Workflow>(configuration);
        }

        public ApiResult<Workflow> GetWorkflowByType(string type)
        {
            string query = "select * from [Configuration.Workflows] where [Type] = '" + type + "'";
            return _baseService.GetAll(query);
        }
    } 
}
