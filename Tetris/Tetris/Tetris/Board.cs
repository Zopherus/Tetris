using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris
{
    //Contains the state of the Tetris Board
    public class Board
    {
        //The position of the Rectangle on the screen
        private Rectangle position;
        //Add in borders of 1 to help detect when pieces cross border
        //value is null if no block is there
        private Block[,] boardState = new Block[TetrisGame.boardWidth + 2, TetrisGame.boardHeight + 2];
        private List<Piece> pieces = new List<Piece>();

        public Board() { }
        public Board(Rectangle position)
        {
            this.position = position;
        }

        public Rectangle Position
        {
            get { return position; }
        }

        public Block[,] BoardState
        {
            get { return boardState; }
        }

        public List<Piece> Pieces
        {
            get { return pieces; }
        }
    }
}
