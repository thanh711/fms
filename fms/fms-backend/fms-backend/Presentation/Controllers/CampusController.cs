using BusinessLogicLayer.IBLLService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
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
    public class CampusController : ControllerBase
    {
        public ICampusBLL _campusBLL;

        public CampusController(ICampusBLL campusBLL)
        {
            _campusBLL = campusBLL;

        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _campusBLL.GetAllInService();
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


        [HttpPost]
        [Route("save")]
        public IActionResult Save([FromBody] Campus model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.CreatedBy))
                {
                    model.CreatedBy = "admin";
                }
                
                var result = _campusBLL.Save(model);
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

        [HttpPost]
        [Route("getlist")]
        public IActionResult GetList([FromBody] CampusSearchModel model)
        {
            try
            {
                var result = _campusBLL.GetListInService(model);
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
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _campusBLL.Delete(id);
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

        [HttpPost]
        [Route("changeActive")]
        public IActionResult ChangeActive([FromBody] Campus model)
        {
            try
            {
                var result = _campusBLL.ChangeInService(model);
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
        [Route("getallNoCondition")]
        public IActionResult GetAllNoCondition()
        {
            try
            {
                var result = _campusBLL.GetAll();
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


        [HttpPost]
        [Route("getlistNoCondition")]
        public IActionResult GetListNoCondition([FromBody] CampusSearchModel model)
        {
            try
            {
                var result = _campusBLL.GetList(model);
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
