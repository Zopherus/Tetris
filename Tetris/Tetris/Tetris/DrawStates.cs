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
            TetrisGame.spriteBatch.Draw(TetrisGame.TetrisBoardTexture, new Rectangle(600, 100, 350, 700), Color.White);
            TetrisGame.spriteBatch.Draw(TetrisGame.BlueBlockTexture, new Rectangle(600, 100, 50, 50), Color.White);
            TetrisGame.spriteBatch.Draw(TetrisGame.CyanBlockTexture, new Rectangle(600, 150, 50, 50), Color.White);
            TetrisGame.spriteBatch.Draw(TetrisGame.GreenBlockTexture, new Rectangle(600, 200, 50, 50), Color.White);
            TetrisGame.spriteBatch.Draw(TetrisGame.OrangeBlockTexture, new Rectangle(600, 250, 50, 50), Color.White);
            TetrisGame.spriteBatch.Draw(TetrisGame.PurpleBlockTexture, new Rectangle(600, 300, 50, 50), Color.White);
            TetrisGame.spriteBatch.Draw(TetrisGame.RedBlockTexture, new Rectangle(600, 350, 50, 50), Color.White);
            TetrisGame.spriteBatch.Draw(TetrisGame.YellowBlockTexture, new Rectangle(600, 400, 50, 50), Color.White);
            TetrisGame.spriteBatch.DrawString(TetrisGame.spriteFont, TetrisGame.mouse.ToString(), new Vector2(0, 0), Color.White);
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
