using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Model
{
    public class ApiResult<T>
    {
        public Paging Paging { get; set; }     
        public int Status { get; set; }
        public List<T> ListData { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
