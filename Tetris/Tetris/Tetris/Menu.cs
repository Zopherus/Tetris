using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris
{
    //The rectangles that are drawn in the Menu screen
    class Menu
    {
        private static Rectangle playRectangle = new Rectangle(TetrisGame.screenWidth / 3, 1 * TetrisGame.screenHeight / 5, TetrisGame.screenWidth / 3, TetrisGame.screenHeight / 6);
        private static Rectangle optionsRectangle = new Rectangle(TetrisGame.screenWidth / 3, 2 * TetrisGame.screenHeight / 5, TetrisGame.screenWidth / 3, TetrisGame.screenHeight / 6);
        private static Rectangle quitRectangle = new Rectangle(TetrisGame.screenWidth / 3, 4 * TetrisGame.screenHeight / 5, TetrisGame.screenWidth / 3, TetrisGame.screenHeight / 6);
        private static Rectangle highscoreRectangle = new Rectangle(TetrisGame.screenWidth / 3, 3 * TetrisGame.screenHeight / 5, TetrisGame.screenWidth / 3, TetrisGame.screenHeight / 6);


        public static Rectangle PlayRectangle
        {
            get { return playRectangle; }
        }

        public static Rectangle OptionsRectangle
        {
            get { return optionsRectangle; }
        }

        public static Rectangle QuitRectangle
        {
            get { return quitRectangle; }
        }

        public static Rectangle HighscoreRectangle
        {
            get { return highscoreRectangle; }
        }
    }
}
