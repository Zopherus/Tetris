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
        //The linespacing, in pixels, used when printing lines of text
        private static int lineSpacing = TetrisGame.gridSize;

        public static void DrawMenu()
        {

            TetrisGame.spriteBatch.Draw(TetrisGame.pokemonsunsetTexture, new Rectangle(0, 0, TetrisGame.screenWidth, TetrisGame.screenHeight), Color.White);

            Point Mouse = new Point(TetrisGame.mouse.X, TetrisGame.mouse.Y);

            if (Menu.PlayRectangle.Contains(Mouse))
                TetrisGame.spriteBatch.Draw(TetrisGame.PlayButtonPressedTexture, Menu.PlayRectangle, Color.White);
            else
                TetrisGame.spriteBatch.Draw(TetrisGame.PlayButtonUnpressedTexture, Menu.PlayRectangle, Color.White);

            if (Menu.OptionsRectangle.Contains(Mouse))
                TetrisGame.spriteBatch.Draw(TetrisGame.OptionsButtonPressedTexture, Menu.OptionsRectangle, Color.White);
            else
                TetrisGame.spriteBatch.Draw(TetrisGame.OptionsButtonUnpressedTexture, Menu.OptionsRectangle, Color.White);

            if (Menu.QuitRectangle.Contains(Mouse))
                TetrisGame.spriteBatch.Draw(TetrisGame.QuitButtonPressedTexture, Menu.QuitRectangle, Color.White);
            else
                TetrisGame.spriteBatch.Draw(TetrisGame.QuitButtonUnpressedTexture, Menu.QuitRectangle, Color.White);

            if (Menu.HighscoreRectangle.Contains(Mouse))
                TetrisGame.spriteBatch.Draw(TetrisGame.HighscoreButtonPressedTexture, Menu.HighscoreRectangle, Color.White);
            else
                TetrisGame.spriteBatch.Draw(TetrisGame.HighscoreButtonUnpressedTexture, Menu.HighscoreRectangle, Color.White);

            //Centers all the buttons horizontally, equally spaces them vertically
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
                Texture2D texture = fullTextureForPiece(pieces[counter]);
                TetrisGame.spriteBatch.Draw(texture,
                    new Rectangle(TetrisGame.PlayerBoard.Position.Right + TetrisGame.screenWidth / 90 + TetrisGame.screenWidth / 90, 
                        TetrisGame.PlayerBoard.Position.Top + TetrisGame.screenHeight/10 + TetrisGame.screenHeight / 10 * counter , 
                        TetrisGame.screenWidth / 20, TetrisGame.screenHeight / 20), Color.White);
            }
            //Draws the hold block section

            TetrisGame.spriteBatch.Draw(TetrisGame.TransparentSquareTexture, 
                new Rectangle(TetrisGame.PlayerBoard.Position.Left - TetrisGame.screenWidth / 15 - TetrisGame.screenWidth / 90, 
                    TetrisGame.PlayerBoard.Position.Top + TetrisGame.screenHeight / 15, TetrisGame.screenWidth / 15, TetrisGame.screenWidth / 15), 
                    Color.White);

            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, "HOLD",
                new Vector2(TetrisGame.PlayerBoard.Position.Left - TetrisGame.screenWidth / 15 - TetrisGame.screenWidth / 90, 
                    TetrisGame.PlayerBoard.Position.Top + TetrisGame.screenHeight / 15 - TetrisGame.PressStartFont.MeasureString("HOLD").Y), 
                    Color.Black);
            
            Texture2D holdBlockTexture = fullTextureForPiece(TetrisGame.PlayerBoard.HoldPiece);

            TetrisGame.spriteBatch.Draw(holdBlockTexture, 
                new Rectangle(TetrisGame.PlayerBoard.Position.Left - TetrisGame.screenWidth / 15 - TetrisGame.screenWidth / 90, 
                    TetrisGame.PlayerBoard.Position.Top + TetrisGame.screenHeight / 15, TetrisGame.screenWidth / 15, TetrisGame.screenWidth / 25), 
                    Color.White);


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
                    Texture2D texture = blockTextureForBlock(block);
                    //The actual board starts on the 3rd row and 2nd column so subtract to compensate for the shift
                    TetrisGame.spriteBatch.Draw(texture,
                        new Rectangle(TetrisGame.PlayerBoard.Position.X + TetrisGame.gridSize * (block.Position.X - Board.rightBorder),
                        TetrisGame.PlayerBoard.Position.Y + TetrisGame.gridSize * (block.Position.Y - Board.topBorder),
                        TetrisGame.gridSize, TetrisGame.gridSize), Color.White);
                }
            }
            //Draw the shadow blocks where the block would be if it was hard dropped
            foreach(Block block in TetrisGame.PlayerBoard.ShadowPiece.Blocks)
            {
                if (block != null)
                {
                    if (TetrisGame.PlayerBoard.BoardState[block.Position.X, block.Position.Y] == null)
                        TetrisGame.spriteBatch.Draw(TetrisGame.ShadowBlockTexture, new Rectangle(TetrisGame.PlayerBoard.Position.X + TetrisGame.gridSize * (block.Position.X - Board.rightBorder),
                            TetrisGame.PlayerBoard.Position.Y + TetrisGame.gridSize * (block.Position.Y - Board.topBorder),
                            TetrisGame.gridSize, TetrisGame.gridSize), Color.White);
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
            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, "OPTIONS",
                new Vector2(TetrisGame.screenWidth / 2 - TetrisGame.PressStartFont.MeasureString("OPTIONS").X / 2, TetrisGame.screenHeight / 60), Color.Black);

            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, "*Select to toggle fullscreen",
                new Vector2(TetrisGame.screenWidth / 5, TetrisGame.screenHeight / 4 + TetrisGame.PressStartFont.MeasureString("*Select to toggle fullscreen").Y / 2), Color.Black);

            TetrisGame.spriteBatch.Draw(TetrisGame.bordersquareTexture, Options.Checkrectangle, Color.White);

            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, "*Additonal Options",
                new Vector2(TetrisGame.screenWidth / 5, TetrisGame.screenHeight / 4 + 7 * lineSpacing + TetrisGame.PressStartFont.MeasureString("Additonal Options").Y / 2), Color.Black);
        }

        public static void DrawHighscore()
        {
            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, "HIGHSCORES",
                new Vector2(TetrisGame.screenWidth / 2 - TetrisGame.PressStartFont.MeasureString("HIGHSCORES").X / 2, 5), Color.Black);

            TetrisGame.spriteBatch.Draw(TetrisGame.bordersquareTexture, Options.Backrectangle, Color.White);

            for (int counter = 1; counter < Highscore.Scores.Length + 1; counter++)
            {
                if (Highscore.Scores[counter - 1] != null)
                {
                    TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, counter.ToString() + ". " + Highscore.Scores[counter - 1].ToString(),
                        new Vector2(TetrisGame.screenWidth / 3, TetrisGame.screenHeight / 15 + (counter - 1) * lineSpacing * 4), Color.Black);
                }
            }
        }

        public static void DrawEnterName()
        {
            string value = "Your Score:" + TetrisGame.PlayerBoard.Points.ToString();
            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, value,
                new Vector2(TetrisGame.screenWidth / 2 - TetrisGame.PressStartFont.MeasureString("Your Score:").X / 2, TetrisGame.screenHeight / 2), Color.Black);


            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, "Name : " + Highscore.currentName,
                 new Vector2(3 * TetrisGame.screenWidth / 8, 7 * TetrisGame.screenHeight / 12), Color.Black);

            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, "Press TAB to play again!",
                new Vector2(TetrisGame.screenWidth / 2 - TetrisGame.PressStartFont.MeasureString("Press TAB to play again!").X / 2, 2 * TetrisGame.screenHeight / 3), Color.Black);

            TetrisGame.spriteBatch.DrawString(TetrisGame.PressStartFont, "GAME OVER",
                new Vector2(TetrisGame.screenWidth / 2 - TetrisGame.PressStartFont.MeasureString("GAME OVER").X / 2, TetrisGame.screenHeight / 6), Color.Black);
        }

        //Used to draw only the outline of a rectangle using a black sprite
        private static void drawOutlineRectangle(Rectangle rectangle)
        {
            TetrisGame.spriteBatch.Draw(TetrisGame.BlackTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, 1), Color.White);
            TetrisGame.spriteBatch.Draw(TetrisGame.BlackTexture, new Rectangle(rectangle.X, rectangle.Y, 1, rectangle.Height), Color.White);
            TetrisGame.spriteBatch.Draw(TetrisGame.BlackTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width, 1), Color.White);
            TetrisGame.spriteBatch.Draw(TetrisGame.BlackTexture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, 1, rectangle.Height + 1), Color.White);
        }

        private static Texture2D fullTextureForPiece(Piece piece)
        {
            Texture2D texture = null;
            if (piece == null)
                return TetrisGame.emptyTexture;
            else
            {
                switch (piece.PieceType)
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
                return texture;
            }
        }

        private static Texture2D blockTextureForBlock(Block block)
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
                    texture = TetrisGame.PurpleBlockTexture;
                    break;
                case BlockType.Z:
                    texture = TetrisGame.RedBlockTexture;
                    break;
            }
            return texture;
        }

    }
}
