using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Model.SearchModel;
using DataAccessLayer.Model.Troubles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IService
{
    public interface ITroubleService
    {
        ApiResult<Trouble> CreateTrouble(Trouble trouble);
        ApiResult<TroubleListModel> GetList(TroubleSearchModel model);
        ApiResult<Trouble> GetByID(int reportId);
        ApiResult<TroubleListModel> ChangeTechnician(TroubleListModel model);
        ApiResult<Trouble> CancelReport(int reportId);
        ApiResult<Trouble> DeleteReport(int id);
        ApiResult<Trouble> UpdateTrouble(Trouble model);
        ApiResult<CountModel> CountReports(TroubleSearchModel model);
        ApiResult<HistoryOfChange> GetHistoryOfReport(int reportId);
    }
}
