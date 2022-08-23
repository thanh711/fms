using DataAccessLayer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.SampleTestData
{
    internal class CommonData
    {
        internal static Paging paging1 = new Paging
        {
            CurrentPage = 1,
            PageSize = 5,
        };
        internal static Paging paging2 = new Paging
        {
            CurrentPage = 1,
            PageSize = 4,
        };
        internal static Paging paging3 = new Paging
        {
            CurrentPage = 2,
            PageSize = 2,
        };
        internal static Paging paging4 = new Paging
        {
            CurrentPage = 1,
            PageSize = 200,
        };
    }
}
