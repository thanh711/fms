using BusinessLogicLayer.IBLLService;
using DataAccessLayer.Model.Map;
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
    public class MapController : ControllerBase
    {
        private readonly IMapBLL _mapBLL;

        public MapController(IMapBLL mapBLL)
        {
            _mapBLL = mapBLL;
        }
        [HttpGet]
        [Route("getAllType")]
        public IActionResult GetAllType()
        {
            try
            {
                var res = _mapBLL.GetAllAreaType();

                if (res.Status == 200)
                {
                    return Ok(res);
                }
                return BadRequest(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("getMap")]
        public IActionResult GetMap([FromBody] FilterModel filter)
        {
            try
            {
                var res = _mapBLL.GetMap(filter);

                if (res.Status == 200)
                {
                    return Ok(res);
                }
                return BadRequest(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("getAreaSelect")]
        public IActionResult GetAreaSelect([FromBody] FilterModel filter)
        {
            try
            {
                var res = _mapBLL.GetListAreaSelect(filter);

                if (res.Status == 200)
                {
                    return Ok(res);
                }
                return BadRequest(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("saveMap")]
        public IActionResult SaveMap([FromBody] SaveMapModel model)
        {
            try
            {
                var res = _mapBLL.SaveMap(model);

                if (res.Status == 200)
                {
                    return Ok(res);
                }
                return BadRequest(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
