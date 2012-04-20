using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapGen
{
    public static class Randomizer
    {
        private static Random _random = new Random();
        private static Directions[] _directions = (Directions[])Enum.GetValues(typeof(Directions));
        private static int _seed;

        public static int Seed
        {
            get { return _seed; }
            private set { _seed = value; _random = new Random(value); }
        }

        public static T PickRandomItem<T>(this T[] items)
        {
            return items[_random.Next(0, items.Length)];
        }

        public static int GetRandomNumber()
        {
            return _random.Next();
            
        }
        public static int GetRandomNumber(int max)
        {
            return _random.Next(max);
        }
        public static int GetRandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public static double GetRandomDouble()
        {
            return _random.NextDouble();
        }

        public static bool GetRandomBool(double odds)
        {
            return GetRandomDouble() <= odds;
        }

        public static Directions GetRandomDirection()
        {    
            return _directions.PickRandomItem<Directions>();
        }

        public static void SetSeed(int seed)
        {
            Seed = seed;
        }

        public static void RandomizeSeed()
        {
            _random = new Random();
        }
    }
}
