using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris
{
    [Serializable]
    //Comprised of four blocks
    public class Piece
    {
        private BlockType pieceType;
        private Block[] blocks = new Block[4];
        private RotationState rotationState = RotationState.One;

        private Point[,] boundingSquare = {
                                              {new Point(4, 0), new Point(4, 1), new Point(4, 2), new Point(4, 3)},
                                              {new Point(5, 0), new Point(5, 1), new Point(5, 2), new Point(5, 3)},
                                              {new Point(6, 0), new Point(6, 1), new Point(6, 2), new Point(6, 3)},
                                              {new Point(7, 0), new Point(7, 1), new Point(7, 2), new Point(7, 3)}
                                          }; 

        public Piece()
        { }

        public Piece(BlockType pieceType)
        {
            this.pieceType = pieceType;
            //When a new piece object is created, the piece is placed at the starting position
            switch(pieceType)
            {
                case BlockType.I:
                    blocks[0] = new Block(pieceType, new Point(Board.rightBorder + 3, Board.topBorder - 1), this);
                    blocks[1] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder - 1), this);
                    blocks[2] = new Block(pieceType, new Point(Board.rightBorder + 5, Board.topBorder - 1), this);
                    blocks[3] = new Block(pieceType, new Point(Board.rightBorder + 6, Board.topBorder - 1), this);
                    break;
                case BlockType.J:
                    blocks[0] = new Block(pieceType, new Point(Board.rightBorder + 3, Board.topBorder - 2), this);
                    blocks[1] = new Block(pieceType, new Point(Board.rightBorder + 3, Board.topBorder - 1), this);
                    blocks[2] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder - 1), this);
                    blocks[3] = new Block(pieceType, new Point(Board.rightBorder + 5, Board.topBorder - 1), this);
                    break;
                case BlockType.L:
                    blocks[0] = new Block(pieceType, new Point(Board.rightBorder + 3, Board.topBorder - 1), this);
                    blocks[1] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder - 1), this);
                    blocks[2] = new Block(pieceType, new Point(Board.rightBorder + 5, Board.topBorder - 1), this);
                    blocks[3] = new Block(pieceType, new Point(Board.rightBorder + 5, Board.topBorder - 2), this);
                    break;
                case BlockType.O:
                    blocks[0] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder - 1), this);
                    blocks[1] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder - 2), this);
                    blocks[2] = new Block(pieceType, new Point(Board.rightBorder + 5, Board.topBorder - 1), this);
                    blocks[3] = new Block(pieceType, new Point(Board.rightBorder + 5, Board.topBorder - 2), this);
                    break;
                case BlockType.S:
                    blocks[0] = new Block(pieceType, new Point(Board.rightBorder + 3, Board.topBorder - 1), this);
                    blocks[1] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder - 1), this);
                    blocks[2] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder - 2), this);
                    blocks[3] = new Block(pieceType, new Point(Board.rightBorder + 5, Board.topBorder - 2), this);
                    break;
                case BlockType.T:
                    blocks[0] = new Block(pieceType, new Point(Board.rightBorder + 3, Board.topBorder - 1), this);
                    blocks[1] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder - 1), this);
                    blocks[2] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder - 2), this);
                    blocks[3] = new Block(pieceType, new Point(Board.rightBorder + 5, Board.topBorder - 1), this);
                    break;
                case BlockType.Z:
                    blocks[0] = new Block(pieceType, new Point(Board.rightBorder + 3, Board.topBorder - 2), this);
                    blocks[1] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder - 2), this);
                    blocks[2] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder - 1), this);
                    blocks[3] = new Block(pieceType, new Point(Board.rightBorder + 5, Board.topBorder - 1), this);
                    break;
            }
        }

        public Piece(Block[] blocks)
        {
            this.blocks = blocks;
        }

        public BlockType PieceType
        {
            get { return pieceType; }
        }

        public Block[] Blocks
        {
            get { return blocks; }
            set { blocks = value; }
        }

        public Point[,] BoundingSquare
        {
            get { return boundingSquare; }
        }

        public RotationState RotationState
        {
            get { return rotationState; }
        }

        public void fall()
        {
            if (canFall())
            {
                clearOldPosition();
                foreach (Block block in blocks)
                {
                    block.Position = new Point(block.Position.X, block.Position.Y + 1);
                }
                boundingSquareFall();
            }
            else
            {
                if (!TetrisGame.PlayerBoard.checkOnBoard(TetrisGame.PlayerBoard.CurrentPiece))
                    TetrisGame.gameState = GameState.EnterName;
                TetrisGame.PlayerBoard.changeCurrentPiece();
                TetrisGame.PlayerBoard.clearLines();
            }
        }

        public void moveRight() 
        {
            if (canMoveRight())
            {
                clearOldPosition();
                foreach (Block block in blocks)
                {
                    block.Position = new Point(block.Position.X + 1, block.Position.Y);
                }
                boundingSquareMoveRight();
            }
        }

        public void moveLeft()
        {
            if (canMoveLeft())
            {
                clearOldPosition();
                foreach (Block block in blocks)
                {
                    block.Position = new Point(block.Position.X - 1, block.Position.Y);
                }
                boundingSquareMoveLeft();
            }
        }

        public void rotateRight()
        {
            clearOldPosition();
            //Increase the RotationState by one, making sure to keep it in the bounds
            rotationState = (RotationState)((mod((int)rotationState + 1, 4)));
            switch(pieceType)
            {
                case BlockType.I:
                    rotateI();
                    break;
                case BlockType.J:
                    rotateJ();
                    break;
                case BlockType.L:
                    rotateL();
                    break;
                case BlockType.O:
                    return;
                case BlockType.S:
                    rotateS();
                    break;
                case BlockType.T:
                    rotateT();
                    break;
                case BlockType.Z:
                    rotateZ();
                    break;
            }
        }

        public void rotateLeft()
        {
            clearOldPosition();
            //Decrease the RotationState by one, making sure to keep it in the bounds
            rotationState = (RotationState)((mod((int)rotationState - 1, 4)));
            switch (pieceType)
            {
                case BlockType.I:
                    rotateI();
                    break;
                case BlockType.J:
                    rotateJ();
                    break;
                case BlockType.L:
                    rotateL();
                    break;
                case BlockType.O:
                    return;
                case BlockType.S:
                    rotateS();
                    break;
                case BlockType.T:
                    rotateT();
                    break;
                case BlockType.Z:
                    rotateZ();
                    break;
            }
        }

        private void boundingSquareFall()
        {
            for (int x = 0; x < BoundingSquare.GetLength(0); x++)
            {
                for (int y = 0; y < BoundingSquare.GetLength(1); y++)
                {
                    BoundingSquare[x, y].Y++;
                }
            }
        }

        private void boundingSquareMoveRight()
        {
            for (int x = 0; x < BoundingSquare.GetLength(0); x++)
            {
                for (int y = 0; y < BoundingSquare.GetLength(1); y++)
                {
                    BoundingSquare[x, y].X++;
                }
            }
        }

        private void boundingSquareMoveLeft()
        {
            for (int x = 0; x < BoundingSquare.GetLength(0); x++)
            {
                for (int y = 0; y < BoundingSquare.GetLength(1); y++)
                {
                    BoundingSquare[x, y].X--;
                }
            }
        }

        public bool canFall()
        {
            bool value = true;
            foreach (Block block in blocks)
            {
                if (!block.canFall())
                    value = false;
            }
            return value;
        }

        private bool canMoveRight()
        {
            bool value = true;
            foreach (Block block in blocks)
            {
                if (!block.canMoveRight())
                    value = false;
            }
            return value;
        }

        private bool canMoveLeft()
        {
            bool value = true;
            foreach (Block block in blocks)
            {
                if (!block.canMoveLeft())
                    value = false;
            }
            return value;
        }

        private void clearOldPosition()
        {
            foreach(Block block in blocks)
            {
                TetrisGame.PlayerBoard.BoardState[block.Position.X, block.Position.Y] = null;
            }
        }

        private void rotateI()
        {
            switch (rotationState)
            {
                case RotationState.One:
                    blocks[0].Position = boundingSquare[0, 1];
                    blocks[1].Position = boundingSquare[1, 1];
                    blocks[2].Position = boundingSquare[2, 1];
                    blocks[3].Position = boundingSquare[3, 1];
                    break;
                case RotationState.Two:
                    blocks[0].Position = boundingSquare[2, 0];
                    blocks[1].Position = boundingSquare[2, 1];
                    blocks[2].Position = boundingSquare[2, 2];
                    blocks[3].Position = boundingSquare[2, 3];
                    break;
                case RotationState.Three:
                    blocks[0].Position = boundingSquare[0, 2];
                    blocks[1].Position = boundingSquare[1, 2];
                    blocks[2].Position = boundingSquare[2, 2];
                    blocks[3].Position = boundingSquare[3, 2];
                    break;
                case RotationState.Four:
                    blocks[0].Position = boundingSquare[1, 0];
                    blocks[1].Position = boundingSquare[1, 1];
                    blocks[2].Position = boundingSquare[1, 2];
                    blocks[3].Position = boundingSquare[1, 3];
                    break;
            }
        }

        private void rotateJ()
        {
            switch (rotationState)
            {
                case RotationState.One:
                    blocks[0].Position = boundingSquare[0, 0];
                    blocks[1].Position = boundingSquare[0, 1];
                    blocks[2].Position = boundingSquare[1, 1];
                    blocks[3].Position = boundingSquare[2, 1];
                    break;
                case RotationState.Two:
                    blocks[0].Position = boundingSquare[2, 0];
                    blocks[1].Position = boundingSquare[1, 0];
                    blocks[2].Position = boundingSquare[1, 1];
                    blocks[3].Position = boundingSquare[1, 2];
                    break;
                case RotationState.Three:
                    blocks[0].Position = boundingSquare[2, 2];
                    blocks[1].Position = boundingSquare[2, 1];
                    blocks[2].Position = boundingSquare[1, 1];
                    blocks[3].Position = boundingSquare[0, 1];
                    break;
                case RotationState.Four:
                    blocks[0].Position = boundingSquare[0, 2];
                    blocks[1].Position = boundingSquare[1, 2];
                    blocks[2].Position = boundingSquare[1, 1];
                    blocks[3].Position = boundingSquare[1, 0];
                    break;
            }
        }

        private void rotateL()
        {
            switch (rotationState)
            {
                case RotationState.One:
                    blocks[0].Position = boundingSquare[0, 1];
                    blocks[1].Position = boundingSquare[1, 1];
                    blocks[2].Position = boundingSquare[2, 1];
                    blocks[3].Position = boundingSquare[2, 0];
                    break;
                case RotationState.Two:
                    blocks[0].Position = boundingSquare[1, 0];
                    blocks[1].Position = boundingSquare[1, 1];
                    blocks[2].Position = boundingSquare[1, 2];
                    blocks[3].Position = boundingSquare[2, 2];
                    break;
                case RotationState.Three:
                    blocks[0].Position = boundingSquare[2, 1];
                    blocks[1].Position = boundingSquare[1, 1];
                    blocks[2].Position = boundingSquare[0, 1];
                    blocks[3].Position = boundingSquare[0, 2];
                    break;
                case RotationState.Four:
                    blocks[0].Position = boundingSquare[1, 2];
                    blocks[1].Position = boundingSquare[1, 1];
                    blocks[2].Position = boundingSquare[1, 0];
                    blocks[3].Position = boundingSquare[0, 0];
                    break;
            }
        }

        private void rotateS()
        {
            switch (rotationState)
            {
                case RotationState.One:
                    blocks[0].Position = boundingSquare[0, 1];
                    blocks[1].Position = boundingSquare[1, 1];
                    blocks[2].Position = boundingSquare[1, 0];
                    blocks[3].Position = boundingSquare[2, 0];
                    break;
                case RotationState.Two:
                    blocks[0].Position = boundingSquare[1, 0];
                    blocks[1].Position = boundingSquare[1, 1];
                    blocks[2].Position = boundingSquare[2, 1];
                    blocks[3].Position = boundingSquare[2, 2];
                    break;
                case RotationState.Three:
                    blocks[0].Position = boundingSquare[2, 1];
                    blocks[1].Position = boundingSquare[1, 1];
                    blocks[2].Position = boundingSquare[1, 2];
                    blocks[3].Position = boundingSquare[0, 2];
                    break;
                case RotationState.Four:
                    blocks[0].Position = boundingSquare[1, 2];
                    blocks[1].Position = boundingSquare[1, 1];
                    blocks[2].Position = boundingSquare[0, 1];
                    blocks[3].Position = boundingSquare[0, 0];
                    break;
            }
        }

        private void rotateT()
        {
            switch (rotationState)
            {
                case RotationState.One:
                    blocks[0].Position = boundingSquare[0, 1];
                    blocks[1].Position = boundingSquare[1, 1];
                    blocks[2].Position = boundingSquare[1, 0];
                    blocks[3].Position = boundingSquare[2, 1];
                    break;
                case RotationState.Two:
                    blocks[0].Position = boundingSquare[1, 0];
                    blocks[1].Position = boundingSquare[1, 1];
                    blocks[2].Position = boundingSquare[2, 1];
                    blocks[3].Position = boundingSquare[1, 2];
                    break;
                case RotationState.Three:
                    blocks[0].Position = boundingSquare[2, 1];
                    blocks[1].Position = boundingSquare[1, 1];
                    blocks[2].Position = boundingSquare[1, 2];
                    blocks[3].Position = boundingSquare[0, 1];
                    break;
                case RotationState.Four:
                    blocks[0].Position = boundingSquare[1, 2];
                    blocks[1].Position = boundingSquare[1, 1];
                    blocks[2].Position = boundingSquare[0, 1];
                    blocks[3].Position = boundingSquare[1, 0];
                    break;
            }
        }

        private void rotateZ()
        {
            switch (rotationState)
            {
                case RotationState.One:
                    blocks[0].Position = boundingSquare[0, 0];
                    blocks[1].Position = boundingSquare[1, 0];
                    blocks[2].Position = boundingSquare[1, 1];
                    blocks[3].Position = boundingSquare[2, 1];
                    break;
                case RotationState.Two:
                    blocks[0].Position = boundingSquare[2, 0];
                    blocks[1].Position = boundingSquare[2, 1];
                    blocks[2].Position = boundingSquare[1, 1];
                    blocks[3].Position = boundingSquare[1, 2];
                    break;
                case RotationState.Three:
                    blocks[0].Position = boundingSquare[2, 2];
                    blocks[1].Position = boundingSquare[1, 2];
                    blocks[2].Position = boundingSquare[1, 1];
                    blocks[3].Position = boundingSquare[0, 1];
                    break;
                case RotationState.Four:
                    blocks[0].Position = boundingSquare[0, 2];
                    blocks[1].Position = boundingSquare[0, 1];
                    blocks[2].Position = boundingSquare[1, 1];
                    blocks[3].Position = boundingSquare[1, 0];
                    break;
            }
        }

        //returns the mod of x in base m, % is the remainder function not mod function
        private int mod(int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}
