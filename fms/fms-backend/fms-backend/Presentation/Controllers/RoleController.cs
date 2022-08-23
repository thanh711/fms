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
    public class RoleController : ControllerBase
    {
        public IRoleBLL _roleBLL;

        public RoleController(IRoleBLL roleBLL)
        {
            _roleBLL = roleBLL;

        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _roleBLL.GetAll();
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
