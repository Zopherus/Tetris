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
            TetrisGame.spriteBatch.Draw(TetrisGame.CyanBlockTexture, new Rectangle(100, 100, 50, 50), Color.White);
            drawOutlineRectangle(new Rectangle());
        }

        public static void DrawPlay()
        {

        }

        private static void drawOutlineRectangle(Rectangle rectangle)
        {
            TetrisGame.spriteBatch.Draw(TetrisGame.BlackTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, 1), Color.White);
            TetrisGame.spriteBatch.Draw(TetrisGame.BlackTexture, new Rectangle(rectangle.X, rectangle.Y, 1, rectangle.Height), Color.White);
            TetrisGame.spriteBatch.Draw(TetrisGame.BlackTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width + 1, 1), Color.White);
            TetrisGame.spriteBatch.Draw(TetrisGame.BlackTexture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, 1, rectangle.Height), Color.White);
        }
    }
}
