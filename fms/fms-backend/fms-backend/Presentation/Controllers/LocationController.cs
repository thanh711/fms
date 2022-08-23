using BusinessLogicLayer.IBLLService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationBLL _locationBLL;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public LocationController(ILocationBLL locationBLL, IWebHostEnvironment hostingEnvironment)
        {
            _locationBLL = locationBLL;
            _hostingEnvironment = hostingEnvironment;

        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _locationBLL.GetAllInService();
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
        public IActionResult Save([FromBody] Location model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.CreatedBy))
                {
                    model.CreatedBy = "admin";
                }
                
                var result = _locationBLL.SaveLocation(model);
                if (result.Status == 200)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);}
        }

        [HttpPost]
        [Route("getlist")]
        public IActionResult GetList([FromBody] LocationSearchModel model)
        {
            try
            {
                var result = _locationBLL.GetListInService(model);
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
                var result = _locationBLL.Delete(id);
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
        public IActionResult ChangeActive([FromBody] Location model)
        {
            try
            {
                var result = _locationBLL.ChangeInService(model);
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
        [Route("getByListCampus")]
        public IActionResult GetByListCampus([FromBody] string[] campus) 
        {
            try
            {
                var result = _locationBLL.GetByListCampus(campus.ToList());
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
                var result = _locationBLL.GetAll();
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
        public IActionResult GetListNoCondition([FromBody] LocationSearchModel model)
        {
            try
            {
                var result = _locationBLL.GetList(model);
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
        [Route("downloadTemplate")]
        public IActionResult DownloadTemplate()
        {
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "Template");
            FileInfo file = new(path + @"\Template_Import_Config_Location.xlsx");
            try
            {
                using MemoryStream memoryStream = new();
                using ExcelPackage package = new(file);
                return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("import")]
        public IActionResult Import()
        {
            try
            {
                var file = Request.Form.Files[0];
                var stream = new MemoryStream();
                file.CopyTo(stream);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream);
                List<Location> items = new();
                foreach (DataTable dataTable in excelReader.AsDataSet().Tables)
                {
                    for (int row = 1; row < dataTable.Rows.Count; row++)
                    {
                        if (dataTable.Rows[row][0].ToString() == "")
                        {
                            continue;
                        }
                        //OK
                        Location item = new();
                        item.CampusName = dataTable.Rows[row][1].ToString().Trim();
                        item.Code = dataTable.Rows[row][2].ToString().Trim();
                        item.Name = dataTable.Rows[row][3].ToString().Trim();
                        item.FullName = dataTable.Rows[row][4].ToString().Trim();
                        items.Add(item);
                    }
                }

                return Ok(new ApiResult<Location>
                {
                    Status = 200,
                    ListData = items
                });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
