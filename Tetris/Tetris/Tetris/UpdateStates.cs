using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    class UpdateStates
    {
        private static bool EnterName = true;
        private static bool HighScore = true;
        private static bool reset = false;

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
            if (reset)
            {
                TetrisGame.Start();
                reset = false;
            }
            TetrisGame.PlayerBoard.oldBoardState = ObjectCopier.Clone<Block[,]>(TetrisGame.PlayerBoard.BoardState);
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
                        if (TetrisGame.oldKeyboard.IsKeyUp(Keys.LeftShift))
                            TetrisGame.PlayerBoard.changeHoldPiece();
                        break;
                    case Keys.C:
                        if (TetrisGame.oldKeyboard.IsKeyUp(Keys.C))
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
            if (TetrisGame.PlayerBoard.checkLose())
                TetrisGame.gameState = GameState.EnterName;
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
                if (Options.Checkrectangle.Contains(mouse))
                    TetrisGame.graphics.ToggleFullScreen();

                if (Options.Backrectangle.Contains(mouse))
                    TetrisGame.gameState = GameState.Menu;
            }
        }

        public static void UpdateEnterName()
        {
            reset = true;
            if (EnterName)
            {
                using (StreamReader sr = new StreamReader("Content/Name.txt"))
                {
                    string line = sr.ReadLine();
                    if (line == null)
                        Highscore.currentName = "";
                    else
                        Highscore.currentName = line;
                }
                EnterName = false;
            }
            if (Highscore.currentName.Length < 16)
            {
                foreach (Keys key in TetrisGame.keyboard.GetPressedKeys())
                {
                    if (TetrisGame.oldKeyboard.IsKeyUp(key))
                    {
                        switch (key)
                        {
                            case Keys.A:
                                Highscore.currentName += "A";
                                break;
                            case Keys.B:
                                Highscore.currentName += "B";
                                break;
                            case Keys.C:
                                Highscore.currentName += "C";
                                break;
                            case Keys.D:
                                Highscore.currentName += "D";
                                break;
                            case Keys.E:
                                Highscore.currentName += "E";
                                break;
                            case Keys.F:
                                Highscore.currentName += "F";
                                break;
                            case Keys.G:
                                Highscore.currentName += "G";
                                break;
                            case Keys.H:
                                Highscore.currentName += "H";
                                break;
                            case Keys.I:
                                Highscore.currentName += "I";
                                break;
                            case Keys.J:
                                Highscore.currentName += "J";
                                break;
                            case Keys.K:
                                Highscore.currentName += "K";
                                break;
                            case Keys.L:
                                Highscore.currentName += "L";
                                break;
                            case Keys.M:
                                Highscore.currentName += "M";
                                break;
                            case Keys.N:
                                Highscore.currentName += "N";
                                break;
                            case Keys.O:
                                Highscore.currentName += "O";
                                break;
                            case Keys.P:
                                Highscore.currentName += "P";
                                break;
                            case Keys.Q:
                                Highscore.currentName += "Q";
                                break;
                            case Keys.R:
                                Highscore.currentName += "R";
                                break;
                            case Keys.S:
                                Highscore.currentName += "S";
                                break;
                            case Keys.T:
                                Highscore.currentName += "T";
                                break;
                            case Keys.U:
                                Highscore.currentName += "U";
                                break;
                            case Keys.V:
                                Highscore.currentName += "V";
                                break;
                            case Keys.W:
                                Highscore.currentName += "W";
                                break;
                            case Keys.X:
                                Highscore.currentName += "X";
                                break;
                            case Keys.Y:
                                Highscore.currentName += "Y";
                                break;
                            case Keys.Z:
                                Highscore.currentName += "Z";
                                break;
                            case Keys.Space:
                                Highscore.currentName += " ";
                                break;
                            case Keys.D1:
                                Highscore.currentName += "1";
                                break;
                            case Keys.D2:
                                Highscore.currentName += "2";
                                break;
                            case Keys.D3:
                                Highscore.currentName += "3";
                                break;
                            case Keys.D4:
                                Highscore.currentName += "4";
                                break;
                            case Keys.D5:
                                Highscore.currentName += "5";
                                break;
                            case Keys.D6:
                                Highscore.currentName += "6";
                                break;
                            case Keys.D7:
                                Highscore.currentName += "7";
                                break;
                            case Keys.D8:
                                Highscore.currentName += "8";
                                break;
                            case Keys.D9:
                                Highscore.currentName += "9";
                                break;
                            case Keys.D0:
                                Highscore.currentName += "0";
                                break;
                            case Keys.Tab:
                                TetrisGame.gameState = GameState.Play;
                                break;
                            case Keys.Enter:
                                using (StreamWriter sw = new StreamWriter("Content/Name.txt"))
                                {
                                    sw.WriteLine(Highscore.currentName);
                                    sw.Close();
                                }
                                Highscore.addScore(new Score(Highscore.currentName.Trim(), TetrisGame.PlayerBoard.Points));
                                TetrisGame.gameState = GameState.Menu;
                                HighScore = true;
                                EnterName = true;
                                break;
                        }
                    }
                }
            }
            if (TetrisGame.keyboard.IsKeyDown(Keys.Back))
            {
                if (Highscore.currentName.Length > 0)
                    Highscore.currentName = Highscore.currentName.Remove(Highscore.currentName.Length - 1);
            }
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

            if (HighScore)
            {
                Highscore.ReadFromFile();
                HighScore = false;
            }
            if (TetrisGame.keyboard.IsKeyDown(Keys.LeftShift))
                TetrisGame.gameState = GameState.Menu;
        }

        public static void UpdateLose()
        {

        }
    }
}
