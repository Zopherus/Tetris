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

        //The piece that the block is in
        private Piece piece;

        public Block(BlockType blockType)
        {
            this.blockType = blockType; 
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
                if (value.X >= Board.leftBorder && value.X <  TetrisGame.boardWidth + Board.leftBorder 
                    && value.Y >= Board.topBorder && value.Y < TetrisGame.boardHeight + Board.topBorder
                    && TetrisGame.PlayerBoard.BoardState[value.X,value.Y] == null)
                    position = value;
            }
        }

        public bool canMoveRight()
        {
            return position.X + 1 < TetrisGame.boardWidth + Board.rightBorder &&
               ((piece.Blocks.Contains(TetrisGame.PlayerBoard.BoardState[position.X + 1, position.Y])
               && TetrisGame.PlayerBoard.BoardState[position.X + 1, position.Y].canMoveRight())
               || TetrisGame.PlayerBoard.BoardState[position.X + 1, position.Y] == null);
        }

        public bool canMoveLeft()
        {
             return position.X - 1 >= Board.rightBorder &&
               ((piece.Blocks.Contains(TetrisGame.PlayerBoard.BoardState[position.X - 1, position.Y])
               && TetrisGame.PlayerBoard.BoardState[position.X - 1, position.Y].canMoveLeft())
               || TetrisGame.PlayerBoard.BoardState[position.X - 1, position.Y] == null);
        }

        public bool canFall()
        {
            return position.Y + 1 < TetrisGame.boardHeight + Board.topBorder &&
                ((piece.Blocks.Contains(TetrisGame.PlayerBoard.BoardState[position.X, position.Y + 1]) 
                && TetrisGame.PlayerBoard.BoardState[position.X, position.Y + 1].canFall())
                || TetrisGame.PlayerBoard.BoardState[position.X, position.Y + 1] == null);
        }
    }
}
