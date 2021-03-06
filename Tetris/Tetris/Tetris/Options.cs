﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris
{
    //The rectangles used in the options screen
    class Options
    {
        private static Rectangle fullrectangle = new Rectangle(TetrisGame.screenWidth / 15, TetrisGame.screenHeight / 15, TetrisGame.screenWidth / 3, TetrisGame.screenHeight / 10);
        private static Rectangle backrectangle = new Rectangle(7 * TetrisGame.screenWidth / 8, 7 * TetrisGame.screenHeight / 8, TetrisGame.screenWidth / 10, TetrisGame.screenHeight / 10);
        private static Rectangle checkrectangle = new Rectangle(TetrisGame.screenWidth / 10, TetrisGame.screenHeight / 4, TetrisGame.screenWidth / 15, TetrisGame.screenHeight / 15);

        public static Rectangle Fullrectangle
        {
            get { return fullrectangle; }
        }

        public static Rectangle Backrectangle
        {
            get { return backrectangle; }
        }

        public static Rectangle Checkrectangle
        {
            get { return checkrectangle; }
        }
    }
}
