using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris
{
    class DrawStates
    {
        public static void DrawMenu()
        {
            TetrisGame.spriteBatch.Draw(TetrisGame.BackgroundTexture, new Rectangle(0, 0, TetrisGame.screenWidth, TetrisGame.screenHeight), Color.White);
        }

        public static void DrawPlay()
        {
            TetrisGame.spriteBatch.Draw(TetrisGame.BackgroundTexture, new Rectangle(0, 0, TetrisGame.screenWidth, TetrisGame.screenHeight), Color.White);
            //Places the board in the center of the screen
            TetrisGame.spriteBatch.Draw(TetrisGame.TetrisBoardTexture, TetrisGame.PlayerBoard.Position, Color.White);
        }
    }
}
