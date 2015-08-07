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
        public Block[,] boardState = new Block[TetrisGame.boardWidth + 2, TetrisGame.boardHeight + 2];
        private List<Piece> pieces = new List<Piece>();
        private int points;
        //The piece that is held by the player
        private Piece currentPiece;

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

        public int Points
        {
            get { return points; }
        }

        public Piece CurrentPiece
        {
            get { return currentPiece; }
        }

        //Detects when a line is clear and clears the line
        public void clearLines()
        {
            for (int row = 1; row <= TetrisGame.boardHeight; row ++)
            {
                bool value = true;
                for (int column = 1; column <= TetrisGame.boardWidth; column++)
                {
                    if (boardState[column, row] == null)
                    {
                        value = false;
                    }
                }
                if (value)
                {
                    for (int column = 1; column <= TetrisGame.boardWidth; column++)
                    {
                        //boardState[column, row] = null;
                    }
                }
            }
        }
    }
}
