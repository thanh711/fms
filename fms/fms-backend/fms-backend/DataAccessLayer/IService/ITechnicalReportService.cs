using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using DataAccessLayer.Model.TechnicalReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IService
{
    public interface ITechnicalReportService
    {
        ApiResult<TechnicalReport> GetReport(TechnicalReportSearchModel model);
    }
}
