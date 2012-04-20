﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapGen
{
    public class MapGenerator
    {

        public virtual Room[,] GenerateRooms(int maxRooms)
        {
            int minX = 0;
            int maxX = 0;
            int minY = 0;
            int maxY = 0;

            Directions[] directions = (Directions[])Enum.GetValues(typeof(Directions));
            List<Room> rooms = new List<Room>();
            rooms.Add(new Room(new Point(0, 0)));

            while (rooms.Count < maxRooms)
            {
                Room targetRoom = rooms[Randomizer.GetRandomNumber(rooms.Count)];
                Directions direction = Randomizer.GetRandomDirection();
                Exit targetExit = targetRoom.GetExit(direction);

                if (targetExit != null)
                    continue;

                targetExit = new Exit(direction);
                targetRoom.AddExit(direction);
                Room neighbor = GetRoomAtPoint(targetRoom.GetNeighborCoordinates(direction), rooms);
                Directions neighborDirection = ReverseDirection(direction);

                if (neighbor != null)
                {
                    neighbor.AddExit(neighborDirection);
                }
                else
                {
                    Room newRoom = new Room(targetRoom.GetNeighborCoordinates(direction));
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
            Room[,] roomArray = ConvertListToMap(minX, maxX, minY, maxY, rooms);

            return roomArray;
        }

        protected T[,] ConvertListToMap<T>(int minX, int maxX, int minY, int maxY, IEnumerable<T> rooms) where T : Room
        {
            T[,] roomArray = new T[maxX + -minX + 1, maxY + -minY + 1];

            foreach (var room in rooms)
            {
                room.Location.X += -minX;
                room.Location.Y += -minY;
                roomArray[room.Location.X, room.Location.Y] = room;
            }
            return roomArray;
        }

        public T GetRoomAtPoint<T>(Point location, IEnumerable<T> rooms) where T : Room
        {
            foreach (var room in rooms)
                if (room.Location.X == location.X && room.Location.Y == location.Y)
                    return room;

            return null;
        }

        public Map GenerateMap(int maxRooms)
        {
            Map map = new Map(GenerateRooms(maxRooms));
            return map;
        }

        public Directions ReverseDirection(Directions direction)
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
            }
            throw new FormatException("bad enum arg");
        }

    }
}
