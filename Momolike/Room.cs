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
            }
            throw new ArgumentException("invalid enum val in argument");
        }

        public Point GetNeighborCoordinates(Directions direction)
        {
            switch (direction)
            {
                case Directions.North:
                    return new Point(Location.X, Location.Y - 1);
                case Directions.South:
                    return new Point(Location.X, Location.Y + 1);
                case Directions.East:
                    return new Point(Location.X + 1, Location.Y);
                case Directions.West:
                    return new Point(Location.X - 1, Location.Y);
            }
            throw new ArgumentException("invalid enum val in argument");
        }

        public void AddExit(Directions direction)
        {
            switch (direction)
            {
                case Directions.North:
                    NorthExit = new Exit(direction);
                    break;
                case Directions.South:
                    SouthExit = new Exit(direction);
                    break;
                case Directions.East:
                    EastExit = new Exit(direction);
                    break;
                case Directions.West:
                    WestExit = new Exit(direction);
                    break;
                default:
                    throw new ArgumentException("invalid val in argument" + direction.ToString());
            }
        }
    }
}
