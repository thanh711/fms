using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Troubles
{
    public class Trouble
    {
        public Report Report { get; set; }
        public TroubleShooting Shooting { get; set; }
        public List<Image> ReportImage { get; set; }
        public List<Image> ShootImage { get; set; }
    }
}
