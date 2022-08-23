using BusinessLogicLayer.IBLLService;
using DataAccessLayer.Model.Model.SearchModel;
using DataAccessLayer.Model.TechnicalReport;
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
    public class TechnicalReportController : ControllerBase
    {
        private readonly ITechnicalReportBLL _technicalReportBLL;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public TechnicalReportController(IWebHostEnvironment hostingEnvironment, ITechnicalReportBLL technicalReportBLL)
        {
            _technicalReportBLL = technicalReportBLL;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [Route("getReport")]
        public IActionResult GetList([FromBody] TechnicalReportSearchModel model)
        {
            try
            {
                var result = _technicalReportBLL.GetReport(model);
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
        public IActionResult ExportTroubles([FromBody] TechnicalReport model)
        {
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "Template");
            FileInfo file = new(path + @"\Template_Export_TechnicalReport.xlsx");
            try
            {
                using MemoryStream memoryStream = new();
                using ExcelPackage package = new(file);
                ExcelWorksheet sheet = package.Workbook.Worksheets[0];

                #region I
                sheet.Cells[8, 2].Value = String.Format("{0:dd MMM yyyy}", DateTime.Now);
                sheet.Cells[8, 4].Value = model.Location;
                sheet.Cells[8, 6].Value = model.SystemRoom;

                string equire = "";
                int stt = 1;
                foreach (var item in model.Equipment)
                {
                    equire += stt + ". " + item.Name + ", SL: " + item.Quantity + "\n";
                    stt++;
                }
                sheet.Cells[9, 2].Value = equire;
                #endregion
                #region II. troubles

                int start = 12, i = 0;
                sheet.Cells[start, 1].Value = "Tổng/ Total: " + model.TotalTroubles;
                stt = 1;
                if (model.TroubleshootingReport?.Count > 0)
                {
                    start += 2;
                    sheet.InsertRow(start, model.TroubleshootingReport.Count);

                    i = start;
                    foreach (var item in model.TroubleshootingReport)
                    {
                        #region config cells
                        sheet.Cells[i, 1].Value = stt;
                        sheet.Cells[i, 2].Value = String.Format("{0:dd MMM yyyy}", item.CreateDate);
                        sheet.Cells[i, 3].Value = item.Summary;
                        sheet.Cells[i, 3].Style.WrapText = true;
                        sheet.Cells[i, 4].Value = item.Status;
                        sheet.Cells[i, 5].Value = item.Priority == 1 ? "Low" : (item.Priority == 2 ? "Medium" : (item.Priority == 3 ? "High" : ""));
                        sheet.Cells[i, 6].Value = item.Technician;
                        #endregion
                        i++;
                        stt++;
                    }
                    start = i - 1;
                    string modelRange = "A14:F" + start.ToString();
                    var modelTable = sheet.Cells[modelRange];

                    // Assign borders
                    modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
                else
                {
                    start++;
                }


                #endregion

                #region checklist
                start += 2;
                stt = 1;
                sheet.Cells[start, 1].Value = "Tổng/ Total: " + model.TotalChecklists;
                string range = "A" + (start + 1) + ":F";

                if (model.ChecklistReport.Count > 0)
                {
                    start += 2;
                    if (model.ChecklistReport.Count > 1)
                    {
                        sheet.InsertRow(start + 1, model.ChecklistReport.Count - 1);
                    }

                    i = start;
                    foreach (var item in model.ChecklistReport)
                    {
                        #region config cells
                        sheet.Cells[i, 1].Value = stt;
                        sheet.Cells[i, 2].Value = String.Format("{0:dd MMM yyyy}", item.CheckingDate);
                        sheet.Cells[i, 3].Value = item.Status == null ? "?" : item.Status.ToLower().Equals("true") ? "OK" : "NOK";
                        sheet.Cells[i, 4].Value = item.Issues;
                        sheet.Cells[i, 4].Style.WrapText = true;
                        sheet.Cells[i, 5].Value = item.Solution;
                        sheet.Cells[i, 5].Style.WrapText = true;
                        sheet.Cells[i, 6].Value = item.Technician;
                        #endregion
                        i++;
                        stt++;
                    }
                    if (model.ChecklistReport.Count > 1)
                    {
                        start = i - 1;
                    }

                    var modelRange = range + start.ToString();
                    var modelTable = sheet.Cells[modelRange];

                    // Assign borders
                    modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
                else
                {
                    start += 2;
                }


                #endregion
                string cause = "";
                stt = 1;
                start += 2;
                foreach (var c in model.Causes)
                {
                    cause += "CAUSE - " + 1 + ". [" + c.Priority + "] " + c.Value + "\n";
                }
                sheet.Cells[start, 1].Value = cause;

                stt = 1;
                cause = "";
                start += 2;
                foreach (var s in model.Solutions)
                {
                    cause += "SOLUTION - " + 1 + ". " + s + "\n";
                }
                sheet.Cells[start, 1].Value = cause;

                stt = 1;
                cause = "";
                start += 2;
                foreach (var p in model.Plans)
                {
                    cause += "PLAN - " + 1 + ". " + p + "\n";
                }
                sheet.Cells[start, 1].Value = cause;

                sheet.Cells[start + 3, 1].Value = model.Review;
                sheet.Cells[start + 5, 1].Value = model.Prediction;
                sheet.Cells[start + 7, 3].Value = model.CurrentUser;
                return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
