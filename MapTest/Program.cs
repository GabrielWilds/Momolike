using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using MapGen;
using MapImageRender;

namespace MapTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Map map = new Map(20);
            //Map map = GetStaticMap();

            string[,] mapOutput = new string[map.Rooms.GetLength(0), map.Rooms.GetLength(1)];

            for (int y = 0; y < map.Rooms.GetLength(1); y++)
            {
                for (int x = 0; x < map.Rooms.GetLength(0); x++)
                {
                    if (map.Rooms[x, y] == null)
                    {
                        mapOutput[x, y] = "  ";
                    }
                    else
                    {
                        string output = "[";
                        if (map.Rooms[x, y].NorthExit != null)
                            output += "n";
                        if (map.Rooms[x, y].SouthExit != null)
                            output += "s";
                        if (map.Rooms[x, y].EastExit != null)
                            output += "e";
                        if (map.Rooms[x, y].WestExit != null)
                            output += "w";
                        output += "]";
                        mapOutput[x, y] = output;
                    }
                    Console.Write(mapOutput[x, y]);
                }
                Console.WriteLine();
            }
            ImageMaker.GenerateImageMap("testMap.png", map, new RenderArguments());

            //Console.ReadKey();
            Process.Start("testMap.png");
            
        }

        static int CheckForDuplicateRooms(Room[,] rooms, Point p)
        {
            int foundRooms = 0;
            foreach (Room room in rooms)
            {
                if (room != null)
                {
                    if (room.Location.X == p.X && room.Location.Y == p.Y)
                        foundRooms++;
                }
            }
            return foundRooms;
        }

        static Map GetStaticMap()
        {
            Room[,] rooms = new Room[3, 3];
            for (int x = 0; x < rooms.GetLength(0); x++)
                for (int y = 0; y < rooms.GetLength(1); y++)
                    rooms[x, y] = new Room(new Point(x, y));

            rooms[0, 0].SouthExit = new Exit(Directions.South);
            rooms[0, 0].EastExit = new Exit(Directions.East);

            rooms[1, 0].WestExit = new Exit(Directions.West);
            rooms[1, 0].EastExit = new Exit(Directions.East);

            rooms[2, 0].WestExit = new Exit(Directions.West);
            rooms[2, 0].SouthExit = new Exit(Directions.South);

            rooms[0, 1].NorthExit = new Exit(Directions.North);
            rooms[0, 1].EastExit = new Exit(Directions.East);
            rooms[0, 1].SouthExit = new Exit(Directions.South);

            rooms[0, 2].NorthExit = new Exit(Directions.North);
            rooms[0, 2].EastExit = new Exit(Directions.East);

            rooms[1, 1].WestExit = new Exit(Directions.West);

            rooms[1, 2].WestExit = new Exit(Directions.West);
            rooms[1, 2].EastExit = new Exit(Directions.East);

            rooms[2, 1].NorthExit = new Exit(Directions.North);
            rooms[2, 1].SouthExit = new Exit(Directions.South);

            rooms[2, 2].NorthExit = new Exit(Directions.North);
            rooms[2, 2].WestExit = new Exit(Directions.West);

            return new Map(rooms);
        }
    }
}
