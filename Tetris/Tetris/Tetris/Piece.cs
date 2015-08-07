using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    //Comprised of four blocks
    public class Piece
    {
        private BlockType pieceType;
        private Block[] blocks;

        public Piece()
        { }


        public BlockType PieceType
        {
            get { return pieceType; }
        }

        public Block[] Blocks
        {
            get { return blocks; }
        }
    }
}
