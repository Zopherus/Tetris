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
            Tetris.spriteBatch.Draw(Tetris.CyanBlockTexture, new Rectangle(100, 100, 50, 50), Color.White);
            drawOutlineRectangle(new Rectangle(600, 75, 375, 750));
        }

        public static void DrawPlay()
        {

        }

        private static void drawOutlineRectangle(Rectangle rectangle)
        {
            Tetris.spriteBatch.Draw(Tetris.BlackTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, 1), Color.White);
            Tetris.spriteBatch.Draw(Tetris.BlackTexture, new Rectangle(rectangle.X, rectangle.Y, 1, rectangle.Height), Color.White);
            Tetris.spriteBatch.Draw(Tetris.BlackTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width + 1, 1), Color.White);
            Tetris.spriteBatch.Draw(Tetris.BlackTexture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, 1, rectangle.Height), Color.White);
        }
    }
}
