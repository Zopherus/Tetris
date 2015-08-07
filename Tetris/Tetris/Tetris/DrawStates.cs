using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    class DrawStates
    {
        public static void DrawMenu()
        {

        }

        public static void DrawPlay()
        {
            //TetrisGame.spriteBatch.Draw(TetrisGame.BackgroundTexture, new Rectangle(0, 0, TetrisGame.screenWidth, TetrisGame.screenHeight), Color.White);
            //Places the board in the center of the screen
            //TetrisGame.spriteBatch.Draw(TetrisGame.TransparentSquareTexture, TetrisGame.PlayerBoard.Position, Color.White);
            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, TetrisGame.mouse.X + "," + TetrisGame.mouse.Y, new Vector2(0, 0), Color.Black);
            foreach(Block block in TetrisGame.PlayerBoard.boardState)
            {
                if (block != null)
                {
                    Texture2D texture = null;
                    switch (block.BlockType)
                    {
                        //I is Cyan, O is Yellow, L is Orange, Z is Red, S is green, T is Purple, J is Blue
                        case BlockType.I:
                            texture = TetrisGame.CyanBlockTexture;
                            break;
                        case BlockType.J:
                            texture = TetrisGame.BlueBlockTexture;
                            break;
                        case BlockType.L:
                            texture = TetrisGame.OrangeBlockTexture;
                            break;
                        case BlockType.O:
                            texture = TetrisGame.YellowBlockTexture;
                            break;
                        case BlockType.S:
                            texture = TetrisGame.GreenBlockTexture;
                            break;
                        case BlockType.T:
                            texture = TetrisGame.RedBlockTexture;
                            break;
                        case BlockType.Z:
                            texture = TetrisGame.RedBlockTexture;
                            break;
                    }
                    TetrisGame.spriteBatch.Draw(texture, new Rectangle(TetrisGame.PlayerBoard.Position.X + TetrisGame.gridSize * (block.Position.X - 1),
                        TetrisGame.PlayerBoard.Position.Y + TetrisGame.gridSize * (block.Position.Y - 1), TetrisGame.gridSize, TetrisGame.gridSize),
                        Color.White);
                }
            }
        }

        public static void DrawPause()
        {

        }

        //Used to draw only the outline of a rectangle
        private static void drawOutlineRectangle(Rectangle rectangle)
        {
            TetrisGame.spriteBatch.Draw(TetrisGame.BlackTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, 1), Color.White);
            TetrisGame.spriteBatch.Draw(TetrisGame.BlackTexture, new Rectangle(rectangle.X, rectangle.Y, 1, rectangle.Height), Color.White);
            TetrisGame.spriteBatch.Draw(TetrisGame.BlackTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width, 1), Color.White);
            TetrisGame.spriteBatch.Draw(TetrisGame.BlackTexture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, 1, rectangle.Height + 1), Color.White);
        }
    }
}
