using System;

namespace GameTest
{
#if WINDOWS || XBOX
    static class Program
    {
        public static MomolikeGame Game
        {
            get;
            private set;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Program.Game = new MomolikeGame())
            {
                Game.Run();
            }
        }
    }
#endif
}

