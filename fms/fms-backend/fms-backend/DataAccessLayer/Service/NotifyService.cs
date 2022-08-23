using DataAccessLayer.IService;
using DataAccessLayer.Model.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Service
{
    public class NotifyService : INotifyService
    {
        private readonly BaseService<Notify> _service;
        public NotifyService(IConfiguration configuration)
        {
            _service = new BaseService<Notify>(configuration);
        }

        public ApiResult<Notify> GetChecklistNotify()
        {
            return _service.GetListNoPrams(StoredProcedure.Dash_GetChecklistNotify);
        }

        public ApiResult<Notify> GetWarehouseNotify()
        {
            return _service.GetListNoPrams(StoredProcedure.Dash_GetWarehouseNotify);
        }
    }
}
