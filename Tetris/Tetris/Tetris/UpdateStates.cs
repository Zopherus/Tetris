using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    class UpdateStates
    {
        //The intervals at which the piece does something in milliseconds
        private static int fallBlockStartingInterval = 650;
        private static int softDropStartingInterval = 50;
        private static int moveLateralStartingInterval = 80;
        //Create the fall block timers as started
        private static List<Timer> fallBlockTimers = (List<Timer>)Timer.Create(TetrisGame.GameBoards.Count, fallBlockStartingInterval);
        private static List<Timer> softDropTimers = (List<Timer>)Timer.Create(TetrisGame.GameBoards.Count, softDropStartingInterval);
        private static List<Timer> moveLateralTimers = (List<Timer>)Timer.Create(TetrisGame.GameBoards.Count, moveLateralStartingInterval);
        private static List<List<Timer>> timers = new List<List<Timer>>
        {
            fallBlockTimers,
            softDropTimers,
            moveLateralTimers
        };

        public static void UpdateMenu()
        {
            Point mouse = new Point(TetrisGame.mouse.X, TetrisGame.mouse.Y);

            if (TetrisGame.keyboard.IsKeyDown(Keys.Tab) && TetrisGame.oldKeyboard.IsKeyUp(Keys.Tab))
                TetrisGame.graphics.ToggleFullScreen();

            if (TetrisGame.mouse.LeftButton == ButtonState.Pressed)
            {
                if (Menu.PlayRectangle.Contains(mouse))
                    TetrisGame.gameState = GameState.Play;

                if (Menu.OptionsRectangle.Contains(mouse))
                    TetrisGame.gameState = GameState.Options;

                if (Menu.HighscoreRectangle.Contains(mouse))
                    TetrisGame.gameState = GameState.Highscore;

                if (Menu.QuitRectangle.Contains(mouse))
                    Program.game.Exit();
            }
        }

        public static void UpdatePlay(GameTime gameTime)
        {
            foreach(List<Timer> list in timers)
            {
                foreach(Timer timer in list)
                {
                    timer.tick(gameTime);
                }
            }
            foreach(Timer timer in fallBlockTimers)
            {
                if (timer.TimeMilliseconds > timer.Interval)
                {
                    TetrisGame.PlayerBoard.CurrentPiece.fall();
                    timer.reset();
                }
            }
            /*
             * Esc = Pause
             * Left Shift = Hold
             * Left Control = Rotate Left
             * Z = Rotate Left
             * X = Rotate Right
             * C = Hold
             * Space = Hard Drop
             * Up Arrow = Rotate Right
             * Right Arrow = Move Right
             * Left Arrow = Move Left
             * Down Arrow = Soft Drop
             */
            foreach(Keys Key in TetrisGame.keyboard.GetPressedKeys())
            {
                switch(Key)
                {
                    case Keys.Tab:
                        TetrisGame.graphics.ToggleFullScreen();
                        break;
                    case Keys.Escape:
                        if (TetrisGame.oldKeyboard.IsKeyUp(Keys.Escape))
                            TetrisGame.gameState = GameState.Pause;
                        break;
                    case Keys.LeftShift:
                    case Keys.C:
                        TetrisGame.PlayerBoard.changeHoldPiece();
                        break;
                    case Keys.LeftControl:
                        if (TetrisGame.oldKeyboard.IsKeyUp(Keys.LeftControl))
                            TetrisGame.PlayerBoard.CurrentPiece.rotateLeft();
                        break;
                    case Keys.Z:
                        if (TetrisGame.oldKeyboard.IsKeyUp(Keys.Z))
                            TetrisGame.PlayerBoard.CurrentPiece.rotateLeft();
                        break;
                    case Keys.Space:
                        if (TetrisGame.oldKeyboard.IsKeyUp(Keys.Space))
                        {
                            while (TetrisGame.PlayerBoard.CurrentPiece.canFall())
                            {
                                TetrisGame.PlayerBoard.CurrentPiece.fall();
                            }
                            //cause the block to fall right away which will switch the current piece
                            fallBlockTimers.ElementAt(0).TimeMilliseconds = fallBlockStartingInterval;
                        }
                        break;
                    case Keys.X:
                        if (TetrisGame.oldKeyboard.IsKeyUp(Keys.X))
                            TetrisGame.PlayerBoard.CurrentPiece.rotateRight();
                        break;
                    case Keys.Up:
                        if (TetrisGame.oldKeyboard.IsKeyUp(Keys.Up))
                            TetrisGame.PlayerBoard.CurrentPiece.rotateRight();
                        break;
                    case Keys.Right:
                        Timer timer = moveLateralTimers.ElementAt(0);
                        if (timer.TimeMilliseconds > timer.Interval)
                        {
                            TetrisGame.PlayerBoard.CurrentPiece.moveRight();
                            timer.reset();
                        }
                        break;
                    case Keys.Down:
                        timer = softDropTimers.ElementAt<Timer>(0);
                        if (timer.TimeMilliseconds > timer.Interval)
                        {
                            TetrisGame.PlayerBoard.CurrentPiece.fall();
                            timer.reset();
                        }
                        break;
                    case Keys.Left:
                        timer = moveLateralTimers.ElementAt(0);
                        if (timer.TimeMilliseconds > timer.Interval)
                        {
                            TetrisGame.PlayerBoard.CurrentPiece.moveLeft();
                            timer.reset();
                        }
                        break;
                }
            }
            TetrisGame.PlayerBoard.updatePosition();
            TetrisGame.PlayerBoard.fillUpcomingPieces();
        }

        public static void UpdatePause()
        {
            if (TetrisGame.keyboard.IsKeyDown(Keys.Escape) && TetrisGame.oldKeyboard.IsKeyUp(Keys.Escape))
                TetrisGame.gameState = GameState.Play;

            if (TetrisGame.keyboard.IsKeyDown(Keys.M))
                TetrisGame.gameState = GameState.Menu;
        }

        public static void UpdateOptions()
        {
            Point mouse = new Point(TetrisGame.mouse.X, TetrisGame.mouse.Y);

            if (TetrisGame.mouse.LeftButton == ButtonState.Pressed)
            {
                if (Options.Fullrectangle.Contains(mouse))
                    TetrisGame.graphics.ToggleFullScreen();

                if (Options.Backrectangle.Contains(mouse))
                    TetrisGame.gameState = GameState.Menu;

            } if (TetrisGame.keyboard.IsKeyDown(Keys.Escape))
                TetrisGame.gameState = GameState.Menu;
        }

        public static void UpdateHighscore()
        {
            Point mouse = new Point(TetrisGame.mouse.X, TetrisGame.mouse.Y);

            if (TetrisGame.mouse.LeftButton == ButtonState.Pressed)
            {
                if (Options.Backrectangle.Contains(mouse))
                    TetrisGame.gameState = GameState.Menu;
            }

            if (TetrisGame.keyboard.IsKeyDown(Keys.Escape))
                TetrisGame.gameState = GameState.Menu;
        }
    }
}
