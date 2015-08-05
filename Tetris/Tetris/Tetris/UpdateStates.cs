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
            if (Tetris.keyboard.IsKeyDown(Keys.Escape))
                Program.game.Exit();
            if (Tetris.keyboard.IsKeyDown(Keys.Tab))
                Tetris.graphics.ToggleFullScreen();
        }

        public static void UpdatePlay()
        {
        }
    }
}
