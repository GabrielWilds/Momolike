using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapGen
{
    public class WeightedRoom : Room
    {
        public int RoomCount = 0;

        public WeightedRoom(Point p)
            : base(p)
        {
        }

        public override void AddExit(Directions direction)
        {
            RoomCount++;
            base.AddExit(direction);
        }
    }
}
