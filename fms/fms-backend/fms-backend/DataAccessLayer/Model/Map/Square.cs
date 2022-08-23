using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Map
{
    public class Square
    {
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int DoorX { get; set; }
        public int DoorY { get; set; }
        public string DirectDoor { get; set; }
        public bool IsHaveDoor { get; set; }
        public string Type { get; set; }
        public int AreaTypeID { get; set; }
        public int AreaID { get; set; }
    }
}
