using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris
{
    [Serializable]
    //A single square in a piece
    public class Block
    {
        private BlockType blockType;

        //Describes the position of the block within the board
        private Point position = new Point();

        //The piece that the block is in
        public Piece piece;

        

        public Block(BlockType blockType)
        {
            this.blockType = blockType; 
        }

        public Block(Point position)
        {
            this.position = position;
        }

        public Block(BlockType blockType, Piece piece) 
        {
            this.blockType = blockType;
            this.piece = piece;
        }

        public Block(BlockType blockType, Point position, Piece piece)
        {
            this.blockType = blockType;
            this.position = position;
            this.piece = piece;
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
                if (value.X >= 0 && value.X < TetrisGame.boardWidth + Board.rightBorder + Board.leftBorder &&
                    value.Y >= 0 && value.Y < TetrisGame.boardHeight + Board.topBorder + Board.bottomBorder)
                position = value; 
            }
        }

        //True if the block is able to move right
        public bool canMoveRight()
        {
            //first line checks if the block will still be on the board after it moves one to the right
            //second and third line checks if the space one to the right is also a block in the same piece and if that block can also move right
            //fourth line checks if the space one to the right is empty
            return position.X + 1 < TetrisGame.boardWidth + Board.leftBorder &&
               ((piece.Blocks.Contains(TetrisGame.PlayerBoard.BoardState[position.X + 1, position.Y])
               && TetrisGame.PlayerBoard.BoardState[position.X + 1, position.Y].canMoveRight())
               || TetrisGame.PlayerBoard.BoardState[position.X + 1, position.Y] == null);
        }

        //True if the block is able to move left
        public bool canMoveLeft()
        {
            //first line checks if the block will still be on the board after it moves one to the left
            //second and third line checks if the space one to the left is also a block in the same piece and if that block can also move left
            //fourth line checks if the space one to the left is empty
             return position.X - 1 >= Board.leftBorder &&
               ((piece.Blocks.Contains(TetrisGame.PlayerBoard.BoardState[position.X - 1, position.Y])
               && TetrisGame.PlayerBoard.BoardState[position.X - 1, position.Y].canMoveLeft())
               || TetrisGame.PlayerBoard.BoardState[position.X - 1, position.Y] == null);
        }

        //True if the block is able to fall
        public bool canFall()
        {
            //first line checks if the block will still be on the board after it moves one down
            //second and third line checks if the space one down is also a block in the same piece and if that block can also move down
            //fourth line checks if the space one down is empty
                return position.Y + 1 < TetrisGame.boardHeight + Board.topBorder &&
                ((piece.Blocks.Contains(TetrisGame.PlayerBoard.BoardState[position.X, position.Y + 1]) 
                && TetrisGame.PlayerBoard.BoardState[position.X, position.Y + 1].canFall())
                || TetrisGame.PlayerBoard.BoardState[position.X, position.Y + 1] == null);
        }

        public bool canMoveUp()
        {
            //first line checks if the block will still be on the board after it moves one up
            //second and third line checks if the space one up is also a block in the same piece and if that block can also move up
            //fourth line checks if the space one up is empty
            return position.Y - 1 >= Board.topBorder &&
                ((piece.Blocks.Contains(TetrisGame.PlayerBoard.BoardState[position.X, position.Y - 1])
                && TetrisGame.PlayerBoard.BoardState[position.X, position.Y - 1].canMoveUp())
                || TetrisGame.PlayerBoard.BoardState[position.X, position.Y - 1] == null);
        }

        //Return true if the current block is in the currentPiece of the board
        private bool inCurrentPiece(Block block)
        {
            if (block.piece == TetrisGame.PlayerBoard.CurrentPiece)
                return false;
            foreach(Block currentBlock in TetrisGame.PlayerBoard.CurrentPiece.Blocks)
            {
                if (block.Position.X == currentBlock.Position.X && block.Position.Y == currentBlock.Position.Y)
                    return true;
            }
            return false;
        }

        //override the default equals, is true when the position and blockType are the same
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            //If parameter cannot be cast to Rectangle return false
            Block block = (Block)obj;
            if ((Object)block == null)
                return false;

            //Return true if the two rectangles match
            return position == block.position && blockType == block.blockType;
        }

        public bool Equals(Block block)
        {
            return position == block.position && blockType == block.blockType;
        }
    }
}
