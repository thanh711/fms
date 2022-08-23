using BusinessLogicLayer.IBLLService;
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
    public class DashboardController : ControllerBase
    {
        private readonly INotifyBLL _notifyBLL;

        public DashboardController(INotifyBLL notifyBLL)
        {
            _notifyBLL = notifyBLL;
        }

        [HttpGet]
        [Route("checklist")]
        public IActionResult GetChecklistNotice()
        {
            try
            {
                var result = _notifyBLL.GetChecklistNotify();
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

        [HttpGet]
        [Route("warehouse")]
        public IActionResult GetWarehouseNotice()
        {
            try
            {
                var result = _notifyBLL.GetWarehouseNotify();
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
