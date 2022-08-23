using BusinessLogicLayer.BLLService;
using BusinessLogicLayer.IBLLService;
using DataAccessLayer.Model.File;
using DataAccessLayer.Model.Model.SearchModel;
using DataAccessLayer.Model.Troubles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TroubleController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ITroubleBLL _troubleBLL;
        public TroubleController(IWebHostEnvironment hostingEnvironment, ITroubleBLL troubleBLL)
        {
            _hostingEnvironment = hostingEnvironment;
            _troubleBLL = troubleBLL;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateTrouble([FromBody] Trouble model)
        {
            try
            {
                model.Report.CreatedBy = (string.IsNullOrEmpty(model.Report.CreatedBy)) ? "Anonymous" : model.Report.CreatedBy;
                var result = _troubleBLL.CreateTrouble(model);
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
        public IActionResult GetList([FromBody] TroubleSearchModel model)
        {
            try
            {
                var result = _troubleBLL.GetList(model);
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
        [Route("export")]
        public IActionResult ExportTroubles([FromBody] TroubleSearchModel model)
        {
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "Template");
            FileInfo file = new (path + @"\Template_Export_Trouble.xlsx");
            try
            {
                using MemoryStream memoryStream = new();
                using ExcelPackage package = new(file);
                ExcelWorksheet sheet = package.Workbook.Worksheets[0];
                var res = _troubleBLL.GetList(model);
                int i = 1;
                if (res.ListData != null)
                {
                    foreach (var item in res.ListData)
                    {
                        #region config cells
                        sheet.Cells[i + 1, 1].Value = i;
                        sheet.Cells[i + 1, 2].Value = item.AreaName;
                        sheet.Cells[i + 1, 3].Value = String.Format("{0:dd MMM yyyy}", item.CreateDate);
                        sheet.Cells[i + 1, 4].Value = item.Priority == 1 ? "Low" : (item.Priority == 2 ? "Medium" : (item.Priority == 3 ? "High" : ""));
                        sheet.Cells[i + 1, 5].Value = item.CategoryName;
                        sheet.Cells[i + 1, 6].Value = item.Summary;
                        sheet.Cells[i + 1, 7].Value = item.Description;
                        sheet.Cells[i + 1, 8].Value = item.Reporter;
                        sheet.Cells[i + 1, 9].Value = item.Status;
                        sheet.Cells[i + 1, 10].Value = item.Technician;
                        sheet.Cells[i + 1, 11].Value = item.IssueReview;
                        sheet.Cells[i + 1, 12].Value = item.Solution;
                        #endregion
                        i++;
                    }
                }
                string modelRange = "A1:L" + (i).ToString();
                var modelTable = sheet.Cells[modelRange];

                // Assign borders
                modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("getbyid")]
        public IActionResult GetByID(int reportId)
        {
            try
            {
                var result = _troubleBLL.GetByID(reportId);
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
        [Route("changetechnician")]
        public IActionResult ChangeTechnician([FromBody] TroubleListModel model)
        {
            try
            {
                var result = _troubleBLL.ChangeTechnician(model);
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
        [Route("deleteimage")]
        public IActionResult DeleteImage ([FromBody] Image image)
        {
            try
            {
                var result = _troubleBLL.DeleteImage(image.ID);
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
        [Route("cancel")]
        public IActionResult CancelReport(int id)
        {
            try
            {
                var result = _troubleBLL.CancelReport(id);
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
        [Route("deleteReport")]
        public IActionResult DeleteReport(int id)
        {
            try
            {
                var result = _troubleBLL.DeleteReport(id);
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
        [Route("update")]
        public IActionResult SaveTroubleshooting([FromBody] Trouble model)
        {
            try
            {
                var result = _troubleBLL.UpdateTrouble(model);
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
        [Route("count")]
        public IActionResult CountReports([FromBody] TroubleSearchModel model)
        {
            try
            {
                var result = _troubleBLL.CountReports(model);
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
        [Route("getHistory")]
        public IActionResult GetHistory(int reportId)
        {
            try
            {
                var result = _troubleBLL.GetHistoryOfReport(reportId);
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
