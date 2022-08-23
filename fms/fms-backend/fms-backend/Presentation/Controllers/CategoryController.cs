using BusinessLogicLayer.IBLLService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model.SearchModel;
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
    public class CategoryController : ControllerBase
    {
        public ICategoryBLL _categoryBLL;

        public CategoryController(ICategoryBLL categoryBLL)
        {
            _categoryBLL = categoryBLL;

        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _categoryBLL.GetAll();
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
        public IActionResult Save([FromBody] Category model)
        {
            try
            {
                model.CreatedBy = (string.IsNullOrEmpty(model.CreatedBy)) ? "nhatbnhe140280" : model.CreatedBy;
                var result = _categoryBLL.Save(model);
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
        public IActionResult GetList([FromBody] CategorySearchModel model)
        {
            try
            {
                var result = _categoryBLL.GetList(model);
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
                var result = _categoryBLL.Delete(id);
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
