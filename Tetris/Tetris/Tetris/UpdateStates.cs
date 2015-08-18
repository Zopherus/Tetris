﻿using System;
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
        }
    }
}
