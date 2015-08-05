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
            if (TetrisGame.keyboard.IsKeyDown(Keys.Escape))
                Program.game.Exit();
            if (TetrisGame.keyboard.IsKeyDown(Keys.Tab))
                TetrisGame.graphics.ToggleFullScreen();
        }

        public static void UpdatePlay()
        {
        }
    }
}
