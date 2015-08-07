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
        public static void UpdateMenu()
        {

        }

        public static void UpdatePlay()
        {
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
                    case Keys.Escape:
                        TetrisGame.gameState = GameState.Pause;
                        break;
                    case Keys.LeftShift:
                    case Keys.C:
                        break;
                    case Keys.LeftControl:
                    case Keys.Z:
                        break;
                    case Keys.Space:
                        break;
                    case Keys.X:
                    case Keys.Up:
                        break;
                    case Keys.Right:
                        break;
                    case Keys.Down:
                        break;
                    case Keys.Left:
                        break;
                }
            }

        }

        public static void UpdatePause()
        {

        }
    }
}
