using BusinessLogicLayer.IBLLService;
using DataAccessLayer.IService;
using DataAccessLayer.Model.Configuration;
using DataAccessLayer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLLService
{
    public class RoleBLL : IRoleBLL
    {
        private IRoleService _roleService;

        public RoleBLL(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public ApiResult<Role> GetAll()
        {
            return _roleService.GetAll();
        }
    }
}
