using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using MapGen;
using MapImageRender;
using System.Security.Cryptography;

namespace MapTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Randomizer.SetSeed(0);
            var generators = GetMapGenerators();

            foreach (var g in generators)
            {
                var directoryName = g.GetType().ToString();
                if (!Directory.Exists(directoryName)) 
                    Directory.CreateDirectory(directoryName);

                for (int i = 0; i < 10; i++)
                {
                    var imageName = Path.Combine(directoryName, i.ToString("00000") + ".png");

                    Console.Write("Generating ");
                    Console.Write(imageName);
                    Console.WriteLine("...");
                    
                    var map = g.GenerateMap(20);
                    ImageMaker.GenerateImageMap(imageName, map, new RenderArguments());
                }

                DetectDuplicates(directoryName, directoryName + ".txt");
            }
        }

        private static void DetectDuplicates(string folder, string reportName)
        {
            Console.WriteLine("Searching for duplicates in " + folder + "...");
            
            var hasher = new SHA512Managed();
            var files = Directory.GetFiles(folder, "*.png");
            var dictionary = new Dictionary<string, List<string>>();

            foreach (var file in files)
            {
                string hash = Convert.ToBase64String(hasher.ComputeHash(File.ReadAllBytes(file)));

                if (dictionary.ContainsKey(hash))
                    dictionary[hash].Add(file);
                else
                {
                    var list = new List<string>();
                    list.Add(file);
                    dictionary.Add(hash, list);
                }
            }

            string output = string.Empty;
            int duplicateCount = 0;
            foreach (var keyPair in dictionary)
            {
                if (keyPair.Value.Count < 2)
                    continue;

                duplicateCount++;
                output += "Matching Hash: " + keyPair.Key + Environment.NewLine;
                output += "==============================" + Environment.NewLine;
                foreach (var file in keyPair.Value)
                    output += file + Environment.NewLine;
                output += Environment.NewLine;
            }

            if (duplicateCount == 0)
                output = "No duplicates!";

            File.WriteAllText(reportName, output);
        }

        private static MapGenerator[] GetMapGenerators()
        {
            return new MapGenerator[] { 
                new RandomMapGenerator(),
                new WeightedMapGenerator(),
                new IsaacMapGenerator()
            };
        }

        private static void PrintMapToConsole(Map map)
        {
            string[,] mapOutput = new string[map.Rooms.GetLength(0), map.Rooms.GetLength(1)];

            for (int y = 0; y < map.Rooms.GetLength(1); y++)
            {
                for (int x = 0; x < map.Rooms.GetLength(0); x++)
                {
                    if (map[x, y] == null)
                    {
                        mapOutput[x, y] = "  ";
                    }
                    else
                    {
                        string output = "[";
                        if (map[x, y].NorthExit != null)
                            output += "n";
                        if (map[x, y].SouthExit != null)
                            output += "s";
                        if (map[x, y].EastExit != null)
                            output += "e";
                        if (map[x, y].WestExit != null)
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

        private static int CheckForDuplicateRooms(Room[,] rooms, Point p)
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

        private static Map GetStaticMap()
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
