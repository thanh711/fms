using BusinessLogicLayer.IBLLService;
using DataAccessLayer.Model.Checklist;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
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


namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChecklistController : ControllerBase
    {
        private readonly IChecklistBLL _checklistBLL;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ChecklistController(IWebHostEnvironment hostingEnvironment, IChecklistBLL checklistBll)
        {
            _checklistBLL = checklistBll;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [Route("getTemplate")]
        public IActionResult GetTemplates(string campus)
        {
            try
            {
                var result = _checklistBLL.GetTemplateByCampus(campus);
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
        public IActionResult GetList([FromBody] ChecklistSearchModel model)
        {
            try
            {
                var result = _checklistBLL.GetList(model);
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
        [Route("getWeeklyList")]
        public IActionResult GetWeeklyList([FromBody] WeeklyChecklistSearchModel model)
        {
            try
            {
                var result = _checklistBLL.GetWeeklyChecklist(model);
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
        [Route("getDetail")]
        public IActionResult GetDetail(string checklistId)
        {
            try
            {
                var result = _checklistBLL.GetDetail(checklistId);
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
        public IActionResult SaveChecklist([FromBody] ChecklistDetailApiResult model)
        {
            try
            {
                var result = _checklistBLL.SaveChecklist(model);
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
        [Route("template")]
        public IActionResult GetTemplateImport()
        {
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "Template");
            FileInfo file = new(path + @"\Template_Import_Checklist.xlsx");
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
        [Route("importFile")]
        public IActionResult SaveImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                var stream = new MemoryStream();
                file.CopyTo(stream);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream);
                List<Item> items = new();
                foreach (DataTable dataTable in excelReader.AsDataSet().Tables)
                {
                    for (int row = 1; row < dataTable.Rows.Count; row++)
                    {
                        if (dataTable.Rows[row][0].ToString() == "")
                        {
                            continue;
                        }
                        //OK
                        Item item = new()
                        {
                            Name = dataTable.Rows[row][0].ToString(),
                            Requirements = dataTable.Rows[row][1].ToString()
                        };
                        item.ListReqs = string.IsNullOrEmpty(item.Requirements) ? null : item.Requirements.Split(';').ToList();
                        item.DefaultValue = dataTable.Rows[row][2].ToString().Trim().Equals("OK");
                        items.Add(item);
                    }
                }

                return Ok(new ApiResult<Item>
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

        [HttpPost]
        [Route("saveImport")]
        public IActionResult SaveImport([FromBody] ImportModel model)
        {
            try
            {
                var result = _checklistBLL.SaveImport(model);
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
        [Route("getTemplateDetail")]
        public IActionResult GetTemplateDetail(int id)
        {
            try
            {
                var result = _checklistBLL.GetTemplateDetail(id);
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
        [Route("getCustomizeDetail")]
        public IActionResult GetCustomizeDetail(int tempId)
        {
            try
            {
                var result = _checklistBLL.GetCustomizeDetail(tempId);
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
        [Route("getAllComponents")]
        public IActionResult GetAllComponents()
        {
            try
            {
                var result = _checklistBLL.GetAllComponents();
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
        [Route("deleteTemplate")]
        public IActionResult DeleteTemplate(int id)
        {
            try
            {
                var result = _checklistBLL.DeleteChecklistTemplate(id);
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
        [Route("createNewItem")]
        public IActionResult CreateNewItem([FromBody] ImportModel model)
        {
            try
            {
                var result = _checklistBLL.CreateNew(model);
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
        [Route("deleteItem")]
        public IActionResult DeleteItem(int id)
        {
            try
            {
                var result = _checklistBLL.DeleteItem(id);
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
        [Route("updateItem")]
        public IActionResult UpdateItem([FromBody] Item model)
        {
            try
            {
                var result = _checklistBLL.UpdateItem(model);
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
        [Route("updateConfiguration")]
        public IActionResult UpdateConfiguration([FromBody] Component model)
        {
            try
            {
                var result = _checklistBLL.SaveConfiguration(model);
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
        [Route("saveTemplate")]
        public IActionResult SaveTemplate([FromBody] Template model)
        {
            try
            {
                var result = _checklistBLL.SaveTemplate(model);
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
        [Route("deleteComponent")]
        public IActionResult DeleteComponent(int id)
        {
            try
            {
                var result = _checklistBLL.DeleteComponent(id);
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
        [Route("getTemplateBasicInfomation")]
        public IActionResult GetTemplateBasicInfomation(int id)
        {
            try
            {
                var result = _checklistBLL.GetTemplateBasicInfomation(id);
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
        [Route("create")]
        public IActionResult Create([FromBody] CreateChecklistModel model)
        {
            try
            {
                var result = _checklistBLL.CreateChecklist(model);
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
