using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris
{
    class Block
    {
        private BlockType blockType;
        //Describes the position within the grid
        private Point position;

        public BlockType BlockType
        {
            get { return blockType; }
        }

        public Point Position
        {
            get { return position; }
            set
            {
                if (value.X > 0 && value.X <= TetrisGame.gridWidth && value.Y > 0 && value.Y <= TetrisGame.gridHeight)
                    position = value;
            }
        }
    }
}
