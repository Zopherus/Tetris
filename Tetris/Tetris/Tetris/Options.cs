using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris
{
    class Options
    {
        private static Rectangle fullrectangle = new Rectangle(TetrisGame.screenWidth / 15, TetrisGame.screenHeight / 15, TetrisGame.screenWidth / 3, TetrisGame.screenHeight / 10);
        private static Rectangle backrectangle = new Rectangle(7 * TetrisGame.screenWidth / 8, 7 * TetrisGame.screenHeight / 8, TetrisGame.screenWidth / 10, TetrisGame.screenHeight / 10);
        
        public static Rectangle Fullrectangle
        {
            get { return fullrectangle; }
        }

        public static Rectangle Backrectangle
        {
            get { return backrectangle; }
        }
    }
}
