using BusinessLogicLayer.IBLLService;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class UserController : ControllerBase
    {
        public IUserBLLService _userBLLService;
        
        public UserController(IUserBLLService userBLLService)
        {
            _userBLLService = userBLLService;
            
        }

        [HttpGet]
        [Route("getUserByEmail")]
        public IActionResult GetUserByEmail(string email)
        {
            try
            {
                var result = _userBLLService.GetUserByEmail(email);
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
        [Route("getall")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _userBLLService.GetAll();
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
        public IActionResult Save([FromBody] User model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.CreatedBy))
                {
                    model.CreatedBy = "admin";
                }
                var result = _userBLLService.Save(model);
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
        public IActionResult GetList([FromBody] UserSearchModel model)
        {
            try
            {
                var result = _userBLLService.GetList(model);
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
        public IActionResult Delete(string username)
        {
            try
            {
                var result = _userBLLService.Delete(username);
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
        public IActionResult ChangeActive([FromBody] User model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.CreatedBy))
                {
                    model.CreatedBy = "nhatbnhe140280";
                }
                var result = _userBLLService.ChangeInService(model);
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
        [Route("getByRole")]
        public IActionResult GetByRole(string role)
        {
            try
            {
                var result = _userBLLService.GetAllByRole(role);
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
