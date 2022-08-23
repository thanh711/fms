using BusinessLogicLayer.IBLLService;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using DataAccessLayer.Model.Warehouse;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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
    public class WarehouseController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IWarehouseBLL _warehouseBLL;

        public WarehouseController(IWebHostEnvironment hostingEnvironment, IWarehouseBLL warehouseBLL)
        {
            _hostingEnvironment = hostingEnvironment;
            _warehouseBLL = warehouseBLL;
        }

        [HttpPost]
        [Route("downloadImportTemplate")]
        public IActionResult DownloadImportTemplate()
        {
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "Template");
            FileInfo file = new(path + @"\Template_Import_Asset.xlsx");
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
        [Route("importAsset")]
        public IActionResult ImportAsset()
        {
            try
            {
                var file = Request.Form.Files[0];
                var stream = new MemoryStream();
                file.CopyTo(stream);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream);
                List<WarehouseImportItem> items = new List<WarehouseImportItem>();
                foreach (DataTable dataTable in excelReader.AsDataSet().Tables)
                {
                    for (int row = 2; row < dataTable.Rows.Count; row++)
                    {
                        if (dataTable.Rows[row][0].ToString() == "")
                        {
                            continue;
                        }
                        //OK
                        WarehouseImportItem item = new()
                        {
                            ImportDate = (DateTime)dataTable.Rows[0][1],
                            Reason = dataTable.Rows[0][4].ToString().Trim(),

                            CategoryName = dataTable.Rows[row][1].ToString().Trim(),
                            AssetCode = dataTable.Rows[row][2].ToString().Trim(),
                            AssetName = dataTable.Rows[row][3].ToString().Trim(),
                            MeasureName = dataTable.Rows[row][4].ToString().Trim(),
                            Quantity = float.Parse(dataTable.Rows[row][5].ToString()),
                            IsReady = dataTable.Rows[row][6].ToString().Trim().Equals("OK")
                        };
                        item = _warehouseBLL.MapDataImport(item);
                        items.Add(item);
                    }
                }

                return Ok(new ApiResult<WarehouseImportItem>
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
        [Route("saveImportWarehouse")]
        public IActionResult SaveImportWarehouse([FromBody] WarehouseImportModel model)
        {
            try
            {
                var res = _warehouseBLL.SaveImportWarehouse(model);

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

        [HttpGet]
        [Route("getAssetByCode")]
        public IActionResult GetAssetByCode(string code)
        {
            try
            {
                var res = _warehouseBLL.GetAsset(code);

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
        [Route("downloadExportTemplate")]
        public IActionResult DownloadExportTemplate()
        {
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "Template");
            FileInfo file = new(path + @"\Template_Export_Asset.xlsx");
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
        [Route("importExportAsset")]
        public IActionResult ImportExportAsset()
        {
            try
            {
                var file = Request.Form.Files[0];
                var stream = new MemoryStream();
                file.CopyTo(stream);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream);
                List<WarehouseExportItem> items = new();
                foreach (DataTable dataTable in excelReader.AsDataSet().Tables)
                {
                    for (int row = 2; row < dataTable.Rows.Count; row++)
                    {
                        if (dataTable.Rows[row][0].ToString() == "")
                        {
                            continue;
                        }
                        //OK
                        WarehouseExportItem item = new()
                        {
                            ExportDate = (DateTime)dataTable.Rows[0][2],
                            Reason = dataTable.Rows[row][4].ToString().Trim(),
                            AssetCode = dataTable.Rows[row][1].ToString().Trim(),
                            Receiver = dataTable.Rows[row][3].ToString().Trim(),
                            Quantity = float.Parse(dataTable.Rows[row][2].ToString()),
                            References = dataTable.Rows[row][5].ToString()
                        };
                        item = _warehouseBLL.MapExportData(item);
                        items.Add(item);
                    }
                }

                return Ok(new ApiResult<WarehouseExportItem>
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
        [Route("validateURL")]
        public IActionResult ValidateURL([FromBody] WarehouseExportItem model)
        {
            try
            {
                var res = _warehouseBLL.ValidateURL(model.References);

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

        [HttpGet]
        [Route("getAllAsset")]
        public IActionResult GetAllAsset()
        {
            try
            {
                var res = _warehouseBLL.GetAllAsset();

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
        [Route("saveExportWarehouse")]
        public IActionResult SaveExportWarehouse([FromBody] WarehouseExportModel model)
        {
            try
            {
                var res = _warehouseBLL.SaveExportWarehouse(model);

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
        [Route("getListHistory")]
        public IActionResult GetListHistory([FromBody] WarehouseSearchModel model)
        {
            try
            {
                var res = _warehouseBLL.GetListHistory(model);

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
        [Route("getListRemaining")]
        public IActionResult GetListRemaining([FromBody] WarehouseSearchModel model)
        {
            try
            {
                var res = _warehouseBLL.GetListRemaining(model);

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
        [Route("getListConfig")]
        public IActionResult GetListConfig([FromBody] WarehouseSearchModel model)
        {
            try
            {
                var res = _warehouseBLL.GetListConfig(model);

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
        [Route("exportHistory")]
        public IActionResult ExportHistory([FromBody] WarehouseSearchModel model)
        {
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "Template");
            FileInfo file = new(path + @"\Template_Export_Warehouse_History.xlsx");
            try
            {
                using MemoryStream memoryStream = new();
                using ExcelPackage package = new(file);
                ExcelWorksheet sheet = package.Workbook.Worksheets[0];
                var res = _warehouseBLL.GetListHistory(model);
                int i = 3, stt = 1;
                sheet.Cells[2, 9].Value = model.From != null ? String.Format("{0:dd MMM yyyy}", model.From) : "";
                sheet.Cells[2, 11].Value = model.To != null ? String.Format("{0:dd MMM yyyy}", model.To) : "";
                if (res.ListData != null)
                {
                    foreach (var item in res.ListData)
                    {
                        #region config cells
                        sheet.Cells[i + 1, 1].Value = stt;
                        sheet.Cells[i + 1, 2].Value = String.Format("{0:dd MMM yyyy}", item.Date);
                        sheet.Cells[i + 1, 3].Value = item.Type;
                        sheet.Cells[i + 1, 4].Value = item.CategoryName;
                        sheet.Cells[i + 1, 5].Value = item.AssetCode;
                        sheet.Cells[i + 1, 6].Value = item.Name;
                        sheet.Cells[i + 1, 7].Value = item.Reason;
                        sheet.Cells[i + 1, 8].Value = item.Quantity;
                        sheet.Cells[i + 1, 9].Value = item.CreatedBy;
                        sheet.Cells[i + 1, 10].Value = item.Receiver;
                        sheet.Cells[i + 1, 11].Value = item.IsReady == false? "Not ready to use" : "";
                        #endregion
                        i++;
                        stt++;
                    }
                }
                string modelRange = "A3:K" + (i).ToString();
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

        [HttpPost]
        [Route("changeReady")]
        public IActionResult ChangeReady([FromBody] UpdateByID model)
        {
            try
            {
                var res = _warehouseBLL.ChangeReady(model);

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
        [Route("updateStandard")]
        public IActionResult UpdateStandard([FromBody] WarehouseAsset model)
        {
            try
            {
                var res = _warehouseBLL.UpdateStandard(model);

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
