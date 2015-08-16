﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris
{
    //Comprised of four blocks
    public class Piece
    {
        private BlockType pieceType;
        private Block[] blocks = new Block[4];
        private RotationState rotationState = RotationState.One;

        public Piece()
        { }

        public Piece(BlockType pieceType)
        {
            this.pieceType = pieceType;
            //When the constructor is used, the piece is placed at the starting position
            switch(pieceType)
            {
                case BlockType.I:
                    blocks[0] = new Block(pieceType, new Point(Board.rightBorder + 3, Board.topBorder), this);
                    blocks[1] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder), this);
                    blocks[2] = new Block(pieceType, new Point(Board.rightBorder + 5, Board.topBorder), this);
                    blocks[3] = new Block(pieceType, new Point(Board.rightBorder + 6, Board.topBorder), this);
                    break;
                case BlockType.J:
                    blocks[0] = new Block(pieceType, new Point(Board.rightBorder + 3, Board.topBorder), this);
                    blocks[1] = new Block(pieceType, new Point(Board.rightBorder + 3, Board.topBorder - 1), this);
                    blocks[2] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder), this);
                    blocks[3] = new Block(pieceType, new Point(Board.rightBorder + 5, Board.topBorder), this);
                    break;
                case BlockType.L:
                    blocks[0] = new Block(pieceType, new Point(Board.rightBorder + 3, Board.topBorder), this);
                    blocks[1] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder), this);
                    blocks[2] = new Block(pieceType, new Point(Board.rightBorder + 5, Board.topBorder), this);
                    blocks[3] = new Block(pieceType, new Point(Board.rightBorder + 5, Board.topBorder - 1), this);
                    break;
                case BlockType.O:
                    blocks[0] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder), this);
                    blocks[1] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder - 1), this);
                    blocks[2] = new Block(pieceType, new Point(Board.rightBorder + 5, Board.topBorder), this);
                    blocks[3] = new Block(pieceType, new Point(Board.rightBorder + 5, Board.topBorder - 1), this);
                    break;
                case BlockType.S:
                    blocks[0] = new Block(pieceType, new Point(Board.rightBorder + 3, Board.topBorder), this);
                    blocks[1] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder), this);
                    blocks[2] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder - 1), this);
                    blocks[3] = new Block(pieceType, new Point(Board.rightBorder + 5, Board.topBorder - 1), this);
                    break;
                case BlockType.T:
                    blocks[0] = new Block(pieceType, new Point(Board.rightBorder + 3, Board.topBorder), this);
                    blocks[1] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder), this);
                    blocks[2] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder - 1), this);
                    blocks[3] = new Block(pieceType, new Point(Board.rightBorder + 5, Board.topBorder), this);
                    break;
                case BlockType.Z:
                    blocks[0] = new Block(pieceType, new Point(Board.rightBorder + 3, Board.topBorder - 1), this);
                    blocks[1] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder), this);
                    blocks[2] = new Block(pieceType, new Point(Board.rightBorder + 4, Board.topBorder - 1), this);
                    blocks[3] = new Block(pieceType, new Point(Board.rightBorder + 5, Board.topBorder), this);
                    break;
            }
        }

        public BlockType PieceType
        {
            get { return pieceType; }
        }

        public Block[] Blocks
        {
            get { return blocks; }
        }

        public void fall()
        {
            bool value = true;
            foreach(Block block in blocks)
            {
                if (!block.canFall())
                    value = false;
            }
            if (value)
            {
                foreach(Block block in blocks)
                {
                    TetrisGame.PlayerBoard.BoardState[block.Position.X, block.Position.Y] = null;
                    block.Position = new Point(block.Position.X, block.Position.Y + 1);
                }
            }
            else
            {
                TetrisGame.PlayerBoard.changeCurrentPiece();
            }
        }

        public void moveRight() 
        {
            bool value = true;
            foreach (Block block in blocks)
            {
                Point point = new Point(block.Position.X + 1, block.Position.Y);
                if (!TetrisGame.PlayerBoard.checkOnBoard(point))
                    value = false;
            }
            if (value)
            {
                foreach(Block block in blocks)
                {
                    TetrisGame.PlayerBoard.BoardState[block.Position.X, block.Position.Y] = null;
                    block.Position = new Point(block.Position.X + 1, block.Position.Y);
                }
            }
        }

        public void moveLeft()
        {
            bool value = true;
            foreach (Block block in blocks)
            {
                Point point = new Point(block.Position.X - 1, block.Position.Y); 
                if (!TetrisGame.PlayerBoard.checkOnBoard(point))
                    value = false;
            }
            if (value)
            {
                foreach (Block block in blocks)
                {
                    TetrisGame.PlayerBoard.BoardState[block.Position.X, block.Position.Y] = null;
                    block.Position = new Point(block.Position.X - 1, block.Position.Y);
                }
            }
        }

        public void rotateRight()
        {
            //Increase the RotationState by one, making sure to keep it in the bounds
            rotationState = (RotationState)((mod((int)rotationState + 1, 4)));
        }

        public void rotateLeft()
        {
            //Increase the RotationState by one, making sure to keep it in the bounds
            rotationState = (RotationState)((mod((int)rotationState - 1, 4)));
        }

        public void softDrop() { }

        public void hardDrop() { }

        //returns the mod of x in base m, % is the remainder function not mod function
        private int mod(int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}
