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

            TetrisGame.spriteBatch.Draw(TetrisGame.pokemonsunsetTexture, new Rectangle(0, 0, TetrisGame.screenWidth, TetrisGame.screenHeight), Color.White);

            Point Mouse = new Point(TetrisGame.mouse.X, TetrisGame.mouse.Y);

            if (Menu.PlayRectangle.Contains(Mouse))
            {
                TetrisGame.spriteBatch.Draw(TetrisGame.PlayButtonPressedTexture, Menu.PlayRectangle, Color.White);
            }
            else
            {
                TetrisGame.spriteBatch.Draw(TetrisGame.PlayButtonUnpressedTexture, Menu.PlayRectangle, Color.White);
            }

            if (Menu.OptionsRectangle.Contains(Mouse))
            {
                TetrisGame.spriteBatch.Draw(TetrisGame.OptionsButtonPressedTexture, Menu.OptionsRectangle, Color.White);
            }
            else
            {
                TetrisGame.spriteBatch.Draw(TetrisGame.OptionsButtonUnpressedTexture, Menu.OptionsRectangle, Color.White);
            }

            if (Menu.QuitRectangle.Contains(Mouse))
            {
                TetrisGame.spriteBatch.Draw(TetrisGame.QuitButtonPressedTexture, Menu.QuitRectangle, Color.White);
            }
            else
            {
                TetrisGame.spriteBatch.Draw(TetrisGame.QuitButtonUnpressedTexture, Menu.QuitRectangle, Color.White);
            }

            if (Menu.HighscoreRectangle.Contains(Mouse))
            {
                TetrisGame.spriteBatch.Draw(TetrisGame.HighscoreButtonPressedTexture, Menu.HighscoreRectangle, Color.White);
            }
            else
            {
                TetrisGame.spriteBatch.Draw(TetrisGame.HighscoreButtonUnpressedTexture, Menu.HighscoreRectangle, Color.White);
            }
            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, "Play",
                new Vector2(Menu.PlayRectangle.Center.X - TetrisGame.PressStartFont.MeasureString("Play").X / 2,
                    Menu.PlayRectangle.Center.Y - TetrisGame.PressStartFont.MeasureString("Play").Y / 2), Color.Black);

            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, "Options",
                new Vector2(Menu.OptionsRectangle.Center.X - TetrisGame.PressStartFont.MeasureString("Options").X / 2,
                    Menu.OptionsRectangle.Center.Y - TetrisGame.PressStartFont.MeasureString("Options").Y / 2), Color.Black);

            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, "Quit",
                new Vector2(Menu.QuitRectangle.Center.X - TetrisGame.PressStartFont.MeasureString("Quit").X / 2,
                    Menu.QuitRectangle.Center.Y - TetrisGame.PressStartFont.MeasureString("Quit").Y / 2), Color.Black);

            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, "Highscore",
                new Vector2(Menu.HighscoreRectangle.Center.X - TetrisGame.PressStartFont.MeasureString("Highscore").X / 2,
                    Menu.HighscoreRectangle.Center.Y - TetrisGame.PressStartFont.MeasureString("Highscore").Y / 2), Color.Black);
        }

        public static void DrawPlay()
        {
            TetrisGame.spriteBatch.Draw(TetrisGame.BackgroundTexture, new Rectangle(0, 0, TetrisGame.screenWidth, TetrisGame.screenHeight), Color.White);
            //Places the board in the center of the screen
            TetrisGame.spriteBatch.Draw(TetrisGame.TransparentSquareTexture, TetrisGame.PlayerBoard.Position, Color.White);

            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, "SCORE:" + TetrisGame.PlayerBoard.Points.ToString(), new Vector2(TetrisGame.PlayerBoard.Position.Left, TetrisGame.PlayerBoard.Position.Top - TetrisGame.PressStartFont.MeasureString("Score : ").Y), Color.Black);

            //Draws the upcoming blocks section
            TetrisGame.spriteBatch.Draw(TetrisGame.TransparentSquareTexture,
                new Rectangle(TetrisGame.PlayerBoard.Position.Right + TetrisGame.screenWidth / 90, TetrisGame.PlayerBoard.Position.Top + TetrisGame.screenHeight / 15, TetrisGame.screenWidth / 15, TetrisGame.screenHeight / 3), Color.White);

            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, "NEXT",
              new Vector2(TetrisGame.PlayerBoard.Position.Right + TetrisGame.screenWidth / 90, TetrisGame.PlayerBoard.Position.Top + TetrisGame.screenHeight / 15 - TetrisGame.PressStartFont.MeasureString("NEXT").Y), Color.Black);

            Piece[] pieces = TetrisGame.PlayerBoard.UpcomingPieces.ToArray();

            for (int counter = 0; counter < 3; counter++)
            {
                Texture2D texture = null;
                switch (pieces[counter].PieceType)
                {
                    case BlockType.I:
                        texture = TetrisGame.IblockTexture;
                        break;
                    case BlockType.J:
                        texture = TetrisGame.JblockTexture;
                        break;
                    case BlockType.L:
                        texture = TetrisGame.LblockTexture;
                        break;
                    case BlockType.O:
                        texture = TetrisGame.OblockTexture;
                        break;
                    case BlockType.S:
                        texture = TetrisGame.SblockTexture;
                        break;
                    case BlockType.T:
                        texture = TetrisGame.TblockTexture;
                        break;
                    case BlockType.Z:
                        texture = TetrisGame.ZblockTexture;
                        break;
                }

                TetrisGame.spriteBatch.Draw(texture, new Rectangle(TetrisGame.PlayerBoard.Position.Right + TetrisGame.screenWidth / 90 + TetrisGame.screenWidth / 180, TetrisGame.PlayerBoard.Position.Top + TetrisGame.screenHeight / 15 - TetrisGame.screenHeight / 30, TetrisGame.screenWidth / 20, TetrisGame.screenHeight / 9), Color.White);

                //Draws the hold block section

                TetrisGame.spriteBatch.Draw(TetrisGame.TransparentSquareTexture, new Rectangle(TetrisGame.PlayerBoard.Position.Left - TetrisGame.screenWidth / 15 - TetrisGame.screenWidth / 90, TetrisGame.PlayerBoard.Position.Top + TetrisGame.screenHeight / 15, TetrisGame.screenWidth / 15, TetrisGame.screenWidth / 15), Color.White);

                TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, "HOLD",
                    new Vector2(TetrisGame.PlayerBoard.Position.Left - TetrisGame.screenWidth / 15 - TetrisGame.screenWidth / 90, TetrisGame.PlayerBoard.Position.Top + TetrisGame.screenHeight / 15 - TetrisGame.PressStartFont.MeasureString("HOLD").Y), Color.Black);

                //Draw the lines of the Tetris Board
                for (int x = TetrisGame.PlayerBoard.Position.X; x <= TetrisGame.PlayerBoard.Position.Right; x += TetrisGame.gridSize)
                {
                    TetrisGame.spriteBatch.Draw(TetrisGame.BlackTexture,
                        new Rectangle(x, TetrisGame.PlayerBoard.Position.Y, 1, TetrisGame.PlayerBoard.Position.Height), Color.Black);
                }
                for (int y = TetrisGame.PlayerBoard.Position.Y; y <= TetrisGame.PlayerBoard.Position.Bottom; y += TetrisGame.gridSize)
                {
                    TetrisGame.spriteBatch.Draw(TetrisGame.BlackTexture,
                        new Rectangle(TetrisGame.PlayerBoard.Position.X, y, TetrisGame.PlayerBoard.Position.Width, 1), Color.Black);
                }

                //Draw all the blocks on the board
                foreach (Block block in TetrisGame.PlayerBoard.BoardState)
                {
                    //only draws the block if it is not null and is within the boundaries of the board
                    if (block != null && block.Position.X >= Board.leftBorder && block.Position.X < TetrisGame.boardWidth + Board.leftBorder
                        && block.Position.Y >= Board.topBorder && block.Position.Y < TetrisGame.boardHeight + Board.topBorder)
                    {
                        texture = null;
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
                                texture = TetrisGame.PurpleBlockTexture;
                                break;
                            case BlockType.Z:
                                texture = TetrisGame.RedBlockTexture;
                                break;
                        }
                        //The actual board starts on the 3rd row and 2nd column so subtract to compensate for the shift
                        TetrisGame.spriteBatch.Draw(texture,
                            new Rectangle(TetrisGame.PlayerBoard.Position.X + TetrisGame.gridSize * (block.Position.X - Board.rightBorder),
                            TetrisGame.PlayerBoard.Position.Y + TetrisGame.gridSize * (block.Position.Y - Board.topBorder),
                            TetrisGame.gridSize, TetrisGame.gridSize), Color.White);
                    }
                }
            }
        }

        public static void DrawPause()
        {
            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont,
             "Controls: \n\nEsc = Pause \nLeft Shift = Hold \nLeft Control = Rotate Left \nZ = Rotate Left \nX = Rotate Right \nC = Hold \nSpace = Hard Drop \nUp Arrow = Rotate Right \nRight Arrow = Move Right \nLeft Arrow = Move Left \nDown Arrow = Soft Drop", 
              new Vector2(5, 5), Color.Black);

            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, "Press Escape to Resume. Press M to return to Menu", new Vector2(5, TetrisGame.screenHeight - TetrisGame.PressStartFont.MeasureString("Press Escape to Resume").Y - 5), Color.Black);
        }

        public static void DrawOptions()
        {
            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, "Select to toggle fullscreen",
            new Vector2(5, 5), Color.Black);

            TetrisGame.spriteBatch.Draw(TetrisGame.bordersquareTexture, Options.Fullrectangle, Color.White);
        }

        public static void DrawHighscore()
        {
            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, "HIGHSCORES",
                new Vector2(TetrisGame.screenWidth / 2 - TetrisGame.PressStartFont.MeasureString("HIGHSCORES").X / 2, 5), Color.Black);

            TetrisGame.spriteBatch.Draw(TetrisGame.bordersquareTexture, Options.Backrectangle, Color.White);
        }

        //Used to draw only the outline of a rectangle using a black sprite
        private static void drawOutlineRectangle(Rectangle rectangle)
        {
            TetrisGame.spriteBatch.Draw(TetrisGame.BlackTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, 1), Color.White);
            TetrisGame.spriteBatch.Draw(TetrisGame.BlackTexture, new Rectangle(rectangle.X, rectangle.Y, 1, rectangle.Height), Color.White);
            TetrisGame.spriteBatch.Draw(TetrisGame.BlackTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width, 1), Color.White);
            TetrisGame.spriteBatch.Draw(TetrisGame.BlackTexture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, 1, rectangle.Height + 1), Color.White);
        }
    }
}
