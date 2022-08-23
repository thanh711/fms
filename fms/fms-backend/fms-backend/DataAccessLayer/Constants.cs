using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Constants
    {
        public static string MESS_SAVE_SUS = "Save successfully.";
        public static string MESS_DEL_SUS = "Delete successfully.";
        public static int FLOOR_MAX = 9;
        public static string REGEX_ROOM = "[0-2][0-9]%";
        public static string REGEX_WC = "WC%";
        public static string REGEX_HLCT = "LOB%";
        public static List<string> STATUS = new List<string>
        {
            "Draft","Opening","Processing","Up Work", "Done", "Cancel"
        };
    }
}
