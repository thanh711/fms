using BusinessLogicLayer.IBLLService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private IWorkflowBLL _workflowBLL;
        public WorkflowController(IWorkflowBLL workflowBLL)
        {
            _workflowBLL = workflowBLL;
        }

        [HttpGet]
        [Route("getByType")]
        public IActionResult GetByType(string type)
        {
            try
            {
                var result = _workflowBLL.GetWorkflowByType(type);
                if (result.Status == 200)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
