using BusinessLogicLayer.IBLLService;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLLService
{
    public class WorkflowBLL : IWorkflowBLL
    {
        private IWorkflowService _service;
        
        public WorkflowBLL(IWorkflowService service)
        {
            _service = service;
        }

        public ApiResult<Workflow> GetWorkflowByType(string type)
        {
            return _service.GetWorkflowByType(type);
        }
    }
}
