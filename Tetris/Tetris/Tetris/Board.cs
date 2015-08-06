using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    class Board
    {
        //The board is only 10 by 20, but the border of 1 around act as the walls
        //bool is true if there is block in that space
        bool[,] boardState = new bool[TetrisGame.gridWidth + 2, TetrisGame.gridHeight + 2];


    }
}
