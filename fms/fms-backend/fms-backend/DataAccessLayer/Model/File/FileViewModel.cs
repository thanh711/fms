using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.File
{
    public class FileViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ThumbUrl { get; set; }
        public string FilePath { get; set; }
    }
}
