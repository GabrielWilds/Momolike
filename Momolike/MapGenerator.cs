using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapGen
{
    public static class MapGenerator
    {
        private static Random random = new Random();

        public static Room[,] GenerateRooms(int maxRooms)
        {
            int minX = 0;
            int maxX = 0;
            int minY = 0;
            int maxY = 0;

            Directions[] directions = (Directions[])Enum.GetValues(typeof(Directions));
            List<Room> rooms = new List<Room>();
            rooms.Add(new Room(new Point(0,0)));

            while (rooms.Count < maxRooms)
            {
                Room targetRoom = rooms[random.Next(rooms.Count)];
                Directions direction = directions[random.Next(directions.Length)];
                Exit targetExit = targetRoom.GetExit(direction);

                if (targetExit != null)
                {
                    continue;
                }
                else
                {
                    targetExit = new Exit(direction);
                    Room neighbor = CheckForExistingRoom(targetRoom.GetNeighbor(direction), rooms);
                    Directions neighborDirection = ReverseDirection(direction);

                    if (neighbor != null)
                    {
                        neighbor.AddExit(neighborDirection);
                    }
                    else
                    {
                        Room newRoom = new Room(targetRoom.GetNeighbor(direction));
                        newRoom.AddExit(neighborDirection);

                        if (newRoom.Location.X < minX)
                            minX = newRoom.Location.X;
                        if (newRoom.Location.X > maxX)
                            maxX = newRoom.Location.X;
                        if (newRoom.Location.Y < minY)
                            minY = newRoom.Location.Y;
                        if (newRoom.Location.Y > maxY)
                            maxY = newRoom.Location.Y;

                        rooms.Add(newRoom);
                    }
                }
            }

            Room[,] roomArray = new Room[maxX + 5,maxY + 5];

            foreach (Room room in rooms)
            {
                room.Location.X -= minX;
                room.Location.Y -= minY;
                roomArray[room.Location.X,room.Location.Y] = room;
            }
            return roomArray;
        }

        public static Room CheckForExistingRoom(Point p, List<Room> rooms)
        {
            foreach (Room room in rooms)
            {
                if (room.Location.X == p.X && room.Location.Y == p.Y)
                    return room;
            }
            return null;
        }

        public static Directions ReverseDirection(Directions direction)
        {
            switch (direction)
            {
                case Directions.North:
                    return Directions.South;
                case Directions.South:
                    return Directions.North;
                case Directions.West:
                    return Directions.East;
                case Directions.East:
                    return Directions.West;
                default:
                    return Directions.North;
            }
        }

    }
}
