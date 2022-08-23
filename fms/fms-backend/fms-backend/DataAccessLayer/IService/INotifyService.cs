﻿using DataAccessLayer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IService
{
    public interface INotifyService
    {
        ApiResult<Notify> GetChecklistNotify();
        ApiResult<Notify> GetWarehouseNotify();
    }
}
