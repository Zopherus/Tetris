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
        private static int fallBlockStartingInterval = 650;
        private static int softDropStartingInterval = 50;
        private static int moveLateralStartingInterval = 80;
        private static List<Timer> fallBlockTimers = (List<Timer>)Timer.Create(TetrisGame.GameBoards.Count, fallBlockStartingInterval);
        private static List<Timer> softDropTimers = (List<Timer>)Timer.Create(TetrisGame.GameBoards.Count, softDropStartingInterval);
        private static List<Timer> moveLateralTimers = (List<Timer>)Timer.Create(TetrisGame.GameBoards.Count, moveLateralStartingInterval);


        public static void UpdateMenu()
        {

            Point mouse = new Point(TetrisGame.mouse.X, TetrisGame.mouse.Y);

            if (TetrisGame.keyboard.IsKeyDown(Keys.Tab) && TetrisGame.oldKeyboard.IsKeyUp(Keys.Tab))
                TetrisGame.graphics.ToggleFullScreen();

            if (TetrisGame.mouse.LeftButton == ButtonState.Pressed)
            {
                if (Menu.PlayRectangle.Contains(mouse))
                    TetrisGame.gameState = GameState.Play;

                if (Menu.QuitRectangle.Contains(mouse))
                    Program.game.Exit();
            }
        }

        public static void UpdatePlay(GameTime gameTime)
        {
            TetrisGame.PlayerBoard.updatePosition();
            foreach(Timer timer in fallBlockTimers)
            {
                timer.start();
                timer.tick(gameTime);
                if (timer.TimeMilliseconds > timer.Interval)
                {
                    TetrisGame.PlayerBoard.CurrentPiece.fall();
                    timer.resetTimer();
                }
            }
            foreach(Timer timer in softDropTimers)
            {
                timer.tick(gameTime);
            }
            foreach(Timer timer in moveLateralTimers)
            {
                timer.tick(gameTime);
            }

            TetrisGame.PlayerBoard.clearLines();
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
                    case Keys.Z:
                        TetrisGame.PlayerBoard.CurrentPiece.rotateLeft();
                        break;
                    case Keys.Space:
                        TetrisGame.PlayerBoard.CurrentPiece.hardDrop();
                        break;
                    case Keys.X:
                    case Keys.Up:
                        TetrisGame.PlayerBoard.CurrentPiece.rotateRight();
                        break;
                    case Keys.Right:
                        Timer timer = moveLateralTimers.ElementAt(0);
                        timer.start();
                        if (timer.TimeMilliseconds > timer.Interval)
                        {
                            TetrisGame.PlayerBoard.CurrentPiece.moveRight();
                            timer.resetTimer();
                        }
                        break;
                    case Keys.Down:
                        timer = softDropTimers.ElementAt<Timer>(0);
                        timer.start();
                        if (timer.TimeMilliseconds > timer.Interval)
                        {
                            TetrisGame.PlayerBoard.CurrentPiece.fall();
                            timer.resetTimer();
                        }
                        break;
                    case Keys.Left:
                        timer = moveLateralTimers.ElementAt(0);
                        timer.start();
                        if (timer.TimeMilliseconds > timer.Interval)
                        {
                            TetrisGame.PlayerBoard.CurrentPiece.moveLeft();
                            timer.resetTimer();
                        }
                        break;
                }
            }
        }

        public static void UpdatePause()
        {
            if (TetrisGame.keyboard.IsKeyDown(Keys.Escape) && TetrisGame.oldKeyboard.IsKeyUp(Keys.Escape))
                TetrisGame.gameState = GameState.Play;
        }
    }
}
