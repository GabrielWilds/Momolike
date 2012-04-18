using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapGen
{
    public class Map
    {
        Room[,] _internalArray = null;

        public Room this[Point p]
        {
            get { return _internalArray[p.X, p.Y]; }
        }

        public Room[,] Rooms
        {
            get { return _internalArray; }
            private set { _internalArray = value; }
        }

        public Map(int maxX, int maxY)
        {
            _internalArray = MapGenerator.GenerateRooms((maxX + maxY) / 2);
        }
    }
}
