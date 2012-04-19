using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MapGen;
using MapImageRender;

namespace MapTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Map map = new Map(2000);
            //foreach (Room room in rooms)
            //{
            //    if (room != null)
            //    {
            //        int hits = CheckForDuplicateRooms(rooms, room.Location);
            //        Console.WriteLine(room.Location.X.ToString() + "," + room.Location.Y.ToString() + ": " + hits.ToString() + " copies found");
            //    }
            //}
            //Console.WriteLine(rooms.Rank.ToString());
            //Console.WriteLine("X: " + rooms.GetLength(0) + " Y: " + rooms.GetLength(1));

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
    }
}
