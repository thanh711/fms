using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using DataAccessLayer.Model.TechnicalReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IBLLService
{
    public interface ITechnicalReportBLL
    {
        ApiResult<TechnicalReport> GetReport(TechnicalReportSearchModel model);
    }
}
