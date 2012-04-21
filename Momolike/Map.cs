using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapGen
{
    public class Map
    {
        private Room[,] _internalArray = null;

        public Room this[int X, int Y]
        {
            get { return _internalArray[X, Y]; }
        }

        public Room[,] Rooms
        {
            get { return _internalArray; }
            private set { _internalArray = value; }
        }

        public Map(int roomCount)
        {
            MapGenerator gen = new RandomMapGenerator();
            _internalArray = gen.GenerateRooms(roomCount);
        }

        public Map(Room[,] rooms)
        {
            _internalArray = rooms;
        }
    }
}
