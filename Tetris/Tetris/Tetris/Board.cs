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
        public const int topBorder = 2;
        public const int leftBorder = 1;
        public const int rightBorder = 1;
        public const int bottomBorder = 1;
        //The position of the Rectangle on the screen
        private Rectangle position;
        //Add in borders to help detect when pieces cross border, extra one line at the top
        //actual board goes from 1 to boardWidth by 2 to boardHeight, all inclusive
        //value is null if no block is there
        private Block[,] boardState = new Block[TetrisGame.boardWidth + leftBorder + rightBorder, TetrisGame.boardHeight + topBorder + bottomBorder];
        private List<Piece> pieces = new List<Piece>();
        private Queue<Piece> upcomingPieces = new Queue<Piece>();
        private int points;
        //The piece that is held by the player
        private Piece currentPiece;
        //The piece that is in the hold spot
        private Piece holdPiece;

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
            set { currentPiece = value; }
        }

        public Queue<Piece> UpcomingPieces
        {
            get { return upcomingPieces; }
        }

        public void updatePosition()
        {
            foreach (Block block in currentPiece.Blocks)
            {
                boardState[block.Position.X, block.Position.Y] = block;
            }
        }

        //Detects when a line is clear and clears the line
        //drops the lines above after a line is cleared
        public void clearLines()
        {
            int linesCleared = 0;
            for (int row = topBorder; row < TetrisGame.boardHeight + topBorder; row ++)
            {
                bool value = true;
                for (int column = leftBorder; column < TetrisGame.boardWidth + leftBorder; column++)
                {
                    if (boardState[column, row] == null)
                    {
                        value = false;
                    }
                }
                if (value)
                {
                    linesCleared++;
                    for (int column = leftBorder; column < TetrisGame.boardWidth + leftBorder; column++)
                    {
                        boardState[column, row] = null;
                    }
                    for (int y = row; y >= topBorder; y--)
                    {
                        for (int x = leftBorder; x < TetrisGame.boardWidth + leftBorder; x++)
                        {
                            if (boardState[x, y] != null)
                            {
                                boardState[x, y].Position = new Point(x, y + 1);
                                boardState[x, y + 1] = boardState[x, y];
                                boardState[x, y] = null;
                            }
                        }
                    }
                }
            }
            switch (linesCleared)
            {
                case 1:
                    points += 100;
                    break;
                case 2:
                    points += 250;
                    break;
                case 3:
                    points += 500;
                    break;
                case 4:
                    points += 1000;
                    break;
            }
        }

        public bool checkOnBoard(Point point) 
        {
            return point.X >= rightBorder && point.X < rightBorder + TetrisGame.boardWidth
                && point.Y >= topBorder && point.Y < topBorder + TetrisGame.boardHeight;
        }

        public void changeCurrentPiece()
        {
            currentPiece = upcomingPieces.Dequeue();
        }

        public void changeHoldPiece()
        {
            if (holdPiece == null)
            {
                holdPiece = currentPiece;
            }
            else
            {
                Piece value = holdPiece;
                holdPiece = currentPiece;
                currentPiece = value;
            }
        }

        public void fillUpcomingPieces()
        {
            while (upcomingPieces.Count < 70) //Quadruple the count of the blocktypes
            {
                List<int> values = Enumerable.Range(0, Enum.GetValues(typeof(BlockType)).Length).ToList<int>();
                Random random = new Random();
                while (values.Count > 0)
                {
                    int number = values.ElementAt(random.Next(values.Count));
                    values.Remove(number);
                    upcomingPieces.Enqueue(new Piece((BlockType)number));
                }
            }
        }
    }
}
