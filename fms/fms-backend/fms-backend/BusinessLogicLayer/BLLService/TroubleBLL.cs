using BusinessLogicLayer.IBLLService;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using DataAccessLayer.Model.Troubles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLLService
{
    public class TroubleBLL : ITroubleBLL
    {
        private ITroubleService _service;
        private IImageService _imageService;
        public TroubleBLL(ITroubleService service, IImageService imageService)
        {
            _service = service;
            _imageService = imageService;
        }

        public ApiResult<Trouble> CancelReport(int reportId)
        {
            return _service.CancelReport(reportId);
        }

        public ApiResult<TroubleListModel> ChangeTechnician(TroubleListModel model)
        {
            return _service.ChangeTechnician(model);
        }

        public ApiResult<Trouble> CreateTrouble(Trouble trouble)
        {
            return _service.CreateTrouble(trouble);
        }

        public ApiResult<Image> DeleteImage(string id)
        {
            return _imageService.Delete(id);
        }

        public ApiResult<Trouble> DeleteReport(int id)
        {
            return _service.DeleteReport(id);
        }

        public ApiResult<Trouble> GetByID(int reportId)
        {
            return _service.GetByID(reportId);
        }

        public ApiResult<TroubleListModel> GetList(TroubleSearchModel model)
        {
            return _service.GetList(model);
        }

        public ApiResult<Trouble> UpdateTrouble(Trouble model)
        {
            return _service.UpdateTrouble(model);
        }

        public ApiResult<CountModel> CountReports(TroubleSearchModel model)
        {
            return _service.CountReports(model);
        }

        public ApiResult<HistoryOfChange> GetHistoryOfReport(int reportId)
        {
            return _service.GetHistoryOfReport(reportId);
        }
    }
}
