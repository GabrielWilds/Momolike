using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapGen
{
    public class Room
    {
        public Point Location
        {
            get;
            private set;
        }

        public Exit NorthExit
        {
            get;
            set;
        }

        public Exit SouthExit
        {
            get;
            set;
        }

        public Exit EastExit
        {
            get;
            set;
        }

        public Exit WestExit
        {
            get;
            set;
        }

        public Room(Point p)
        {
            Location = p;
        }

        public Exit GetExit(Directions direction)
        {
            switch (direction)
            {
                case Directions.North:
                    return NorthExit;
                case Directions.East:
                    return EastExit;
                case Directions.West:
                    return WestExit;
                case Directions.South:
                    return SouthExit;
                default:
                    return null;
            }
        }

        public Point GetNeighbor(Directions direction)
        {
            switch (direction)
            {
                case Directions.North:
                    return new Point(Location.X, Location.Y + 1);
                case Directions.South:
                    return new Point(Location.X, Location.Y - 1);
                case Directions.East:
                    return new Point(Location.X + 1, Location.Y);
                case Directions.West:
                    return new Point(Location.X - 1, Location.Y);
                default:
                    return new Point(0, 0);
            }
        }

        public void AddExit(Directions direction)
        {
            Exit targetExit = GetExit(direction);
            targetExit = new Exit(direction);
        }
    }
}
