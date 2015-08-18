using System;

namespace Tetris
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static TetrisGame game;    
        static void Main(string[] args)
        {
            using (game = new TetrisGame())
            {
                game.Run();
            }
        }
    }
#endif
}

