using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Model
{
    public class BaseImportModel<T>
    {
        public string CurrentUser { get; set; }
        public List<T> ListData { get; set; }
        public T Data { get; set; }

    }
}
