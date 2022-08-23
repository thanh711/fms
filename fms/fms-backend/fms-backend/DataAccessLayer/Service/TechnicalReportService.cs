using DataAccessLayer.IService;
using DataAccessLayer.Model.Checklist;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using DataAccessLayer.Model.TechnicalReport;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Service
{
    public class TechnicalReportService : ITechnicalReportService
    {
        private readonly BaseService<TechnicalReport> _baseService;
        private readonly ChecklistService _checklistService;
        private readonly TroubleService _troubleService;
        private readonly AssetService _assetService;
        public TechnicalReportService(IConfiguration configuration)
        {
            _baseService = new BaseService<TechnicalReport>(configuration);
            _checklistService = new ChecklistService(configuration);
            _troubleService = new TroubleService(configuration);
            _assetService = new AssetService(configuration);
        }

        public ApiResult<TechnicalReport> GetReport(TechnicalReportSearchModel model)
        {
            var reportRes = _troubleService.GetListForTechReport(model);
            var checklistRes = _checklistService.GetListByArea(model);
            var equiRes = _assetService.GetByArea(model.RoomCode);

            return new ApiResult<TechnicalReport>
            {
                Status = 200,
                Data = new TechnicalReport
                {
                    ExportDate = model.ExportDate != null ? (DateTime) model.ExportDate : DateTime.Now,
                    SystemRoom = model.RoomCode,
                    Location = model.LocationCode,
                    Equipment = equiRes?.ListData,
                    TroubleshootingReport = reportRes?.ListData,
                    TotalTroubles = reportRes?.ListData?.Count,
                    ChecklistReport = checklistRes?.ListData,
                    TotalChecklists = checklistRes?.ListData?.Count
                }
            };
        }
    }
}
