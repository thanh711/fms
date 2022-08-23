using BusinessLogicLayer.IBLLService;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using DataAccessLayer.Model.TechnicalReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLLService
{
    public class TechnicalReportBLL : ITechnicalReportBLL
    {
        private ITechnicalReportService _service;
        public TechnicalReportBLL(ITechnicalReportService service)
        {
            _service = service;
        }
        public ApiResult<TechnicalReport> GetReport(TechnicalReportSearchModel model)
        {
            return _service.GetReport(model);
        }
    }
}
