using DataAccessLayer.IService;
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
    public class AssetController : ControllerBase
    {
        public IAssetBLL _assetBLL;

        public AssetController(IAssetBLL assetBLL)
        {
            _assetBLL = assetBLL;

        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _assetBLL.GetAll();
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
        public IActionResult Save([FromBody] Asset model)
        {
            try
            {
                model.CreatedBy = (string.IsNullOrEmpty(model.CreatedBy)) ? "nhatbnhe140280" : model.CreatedBy;
                var result = _assetBLL.Save(model);
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
        public IActionResult GetList([FromBody] AssetSearchModel model)
        {
            try
            {
                var result = _assetBLL.GetList(model);
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
                var result = _assetBLL.Delete(id);
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
        public IActionResult ChangeActive([FromBody] Asset model)
        {
            try
            {
                var result = _assetBLL.ChangeActive(model);
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
        [Route("getMeasure")]
        public IActionResult GetAllMeasure()
        {
            try
            {
                var result = _assetBLL.GetAllMeasureUnit();
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
