using BusinessLogicLayer.IBLLService;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLLService
{
    public class NotifyBLL : INotifyBLL
    {
        private readonly INotifyService _service;
        public NotifyBLL(INotifyService service)
        {
            _service = service;
        }
        public ApiResult<Notify> GetChecklistNotify()
        {
            return _service.GetChecklistNotify();
        }

        public ApiResult<Notify> GetWarehouseNotify()
        {
            return _service.GetWarehouseNotify();
        }
    }
}
