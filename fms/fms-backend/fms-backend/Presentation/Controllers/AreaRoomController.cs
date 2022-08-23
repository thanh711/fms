using BusinessLogicLayer.IBLLService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaRoomController : ControllerBase
    {
        private readonly IAreaBLL _areaBLL ;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AreaRoomController(IAreaBLL areaBLL, IWebHostEnvironment hostingEnvironment)
        {
            _areaBLL = areaBLL;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _areaBLL.GetAllInService();
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
                var result = _areaBLL.GetAll();
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
        public IActionResult Save([FromBody] Area model)
        {
            try
            {
                model.CreatedBy = (string.IsNullOrEmpty(model.CreatedBy)) ?  "admin" : model.CreatedBy;
                var result = _areaBLL.Save(model);
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
        public IActionResult GetList([FromBody] AreaSearchModel model)
        {
            try
            {
                var result = _areaBLL.GetListInService(model);
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
        public IActionResult GetListNoCondition([FromBody] AreaSearchModel model)
        {
            try
            {
                var result = _areaBLL.GetList(model);
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
                var result = _areaBLL.Delete(id);
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
        public IActionResult ChangeActive([FromBody] Area model)
        {
            try
            {
                var result = _areaBLL.ChangeInService(model);
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
        [Route("getByListLocation")]
        public IActionResult GetByListLocation([FromBody] string[] loc)
        {
            try
            {
                var result = _areaBLL.GetByListLocation(loc.ToList());
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
            FileInfo file = new(path + @"\Template_Import_Config_Area.xlsx");
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
                List<Area> items = new();
                foreach (DataTable dataTable in excelReader.AsDataSet().Tables)
                {
                    for (int row = 1; row < dataTable.Rows.Count; row++)
                    {
                        if (dataTable.Rows[row][0].ToString() == "")
                        {
                            continue;
                        }
                        //OK
                        Area item = new();
                        item.Name = dataTable.Rows[row][1].ToString();
                        item.FullName = dataTable.Rows[row][2].ToString();
                        item.LocationCode = dataTable.Rows[row][3].ToString();
                        items.Add(item);
                    }
                }

                return Ok(new ApiResult<Area>
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
