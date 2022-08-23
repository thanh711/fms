using DataAccessLayer.Model.Model;
using DataAccessLayer.Model.Troubles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IService
{
    public interface IImageService
    {
        ApiResult<Image> Delete(string id);
    }
}
