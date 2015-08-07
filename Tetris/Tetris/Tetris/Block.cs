using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris
{
    //A single square in a piece
    public class Block
    {
        private BlockType blockType;
        //Describes the position of the block within the board
        private Point position;


        public Block(BlockType blockType)
        {
            this.blockType = blockType; 
        }
        public Block(BlockType blockType, Point position)
        {
            this.blockType = blockType;
            this.position = position;
        }

        public BlockType BlockType
        {
            get { return blockType; }
        }
        
        public Point Position
        {
            get { return position; }
            set
            {
                if (value.X >= 1 && value.X <= TetrisGame.boardWidth && value.Y >= 1 && value.Y <= TetrisGame.boardHeight)
                    position = value;
            }
        }
    }
}
