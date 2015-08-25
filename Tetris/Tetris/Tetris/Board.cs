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
        //Add in borders around the actual board to help check when pieces fall off the board
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
        //store board state in the last frame for checkLose()
        public Block[,] oldBoardState = new Block[TetrisGame.boardWidth + leftBorder + rightBorder, TetrisGame.boardHeight + topBorder + bottomBorder];
        //Use a queue to store the upcoming pieces
        private Queue<Piece> upcomingPieces = new Queue<Piece>();
        private int points;
        //The piece that is held by the player
        private Piece currentPiece;

        //The piece that is in the hold spot
        private Piece holdPiece;

        //The piece that shows where the currentPiece will be if hard dropped
        public Piece ShadowPiece = new Piece();

        //true if player can use hold
        private bool canHold = true;


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

        public int Points
        {
            get { return points; }
        }

        public Piece CurrentPiece
        {
            get { return currentPiece; }
            set { currentPiece = value; }
        }

        public Piece HoldPiece
        {
            get { return holdPiece; }
        }

        public Queue<Piece> UpcomingPieces
        {
            get { return upcomingPieces; }
        }

        //updates the position of the current piece to the boardState
        public void updatePosition()
        {
            foreach (Block block in currentPiece.Blocks)
            {
                 boardState[block.Position.X, block.Position.Y] = block;
            }
        }

        //Detects when a line is clear and clears the line
        //drops the lines above after a line is cleared
        //gives points for number of cleared lines
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

        //checks if a point is within the boundaries of the board
        public bool checkOnBoard(Point point) 
        {
            return point.X >= rightBorder && point.X < rightBorder + TetrisGame.boardWidth
                && point.Y >= topBorder && point.Y < topBorder + TetrisGame.boardHeight;
        }

        //Returns false if the whole piece is off the board, returns true otherwise
        public bool checkOnBoard(Piece piece)
        {
            foreach(Block block in piece.Blocks)
            {
                if (checkOnBoard(block.Position))
                    return true;
            }
            return false;
        }

        //used when the currentPiece lands and locks out and the currentPiece is switched to the next one
        public void changeCurrentPiece()
        {
            currentPiece = upcomingPieces.Dequeue();
            canHold = true;
        }

        //makes the hold piece the current piece if there is no hold piece, otherwise switches the hold piece and the current piece
        public void changeHoldPiece()
        {
            if (canHold)
            {
                if (holdPiece == null)
                {
                    holdPiece = currentPiece;
                    removeCurrentPiece();
                    changeCurrentPiece();
                }
                else
                {
                    Piece pieceValue = holdPiece;
                    holdPiece = currentPiece;
                    removeCurrentPiece();
                    currentPiece = new Piece(pieceValue.PieceType);
                }
                canHold = false;
            }
        }

        //fill the queue of upcoming pieces by randomizing the set of 7 blocks repeatedly
        public void fillUpcomingPieces()
        {
            while (upcomingPieces.Count < 140) //Ten times the count of the blocktypes
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

        //checks if the board has lost
        public bool checkLose()
        {
            for (int x = rightBorder; x < rightBorder + TetrisGame.boardWidth; x++)
            {
                for (int y = 0; y < topBorder; y++)
                {
                    if (oldBoardState[x, y] != null && boardState[x,y] != null && !oldBoardState[x, y].Equals(boardState[x, y]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Deletes the currentpiece
        private void removeCurrentPiece()
        {
            for (int counter = 0; counter < currentPiece.Blocks.Length; counter++ )
            {
                boardState[currentPiece.Blocks[counter].Position.X, currentPiece.Blocks[counter].Position.Y] = null;
                currentPiece.Blocks[counter] = null;
            }
            currentPiece = null;
        }
    }
}
