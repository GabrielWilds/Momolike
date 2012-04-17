using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MapGen;

namespace MapTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Room[,] rooms = MapGenerator.GenerateRooms(20);
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

            string[,] mapOutput = new string[rooms.GetLength(0), rooms.GetLength(1)];

            for (int x = 0; x < rooms.GetLength(0); x++)
            {
                for (int y = 0; y < rooms.GetLength(1); y++)
                {
                    if (rooms[x, y] == null)
                    {
                        mapOutput[x, y] = "  ";
                    }
                    else
                    {
                        string output = "[";
                        if (rooms[x, y].NorthExit != null)
                            output += "n";
                        if (rooms[x, y].SouthExit != null)
                            output += "s";
                        if (rooms[x, y].EastExit != null)
                            output += "e";
                        if (rooms[x, y].WestExit != null)
                            output += "w";
                        output += "]";
                        mapOutput[x, y] = output;
                    }
                    Console.Write(mapOutput[x, y]);
                }
                Console.WriteLine();
            }
            

            Console.ReadKey();
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
