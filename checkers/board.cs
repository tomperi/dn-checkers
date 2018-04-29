using System;
using System.Collections.Generic;
using System.Text;

namespace checkers
{
    class Board
    {
        Piece?[][] m_Board;
        boardSize m_Size;

        protected Board()
        {
            // default constructor
        }

        protected Board(boardSize i_size)
        {
            // create a new board of specific size, init it with pieces
        }

        private void initBoard()
        {
            // initialized the board with the right amount of pieces
            // init null where there are no objects
        }

        protected void MovePiece(Move i_Move, out moveStatus o_moveStatus) // naming convention for out variables? 
        {
            o_moveStatus = moveStatus.legal; // place holder
            // if the move is legit, move the piece
        }

        protected Move[] GetPossibleMoves(Player i_CurrentPlayer, Move i_LastMove)
        {
            // returns the list of all possible moves for a specific player
            // if the last move was a jump, first check if another jump is possible
            return null;
        }

        protected Piece[][] GetBoard()
        {
            // returns the board matrix
            return null;
        }

        protected gameStatus GetGameStatus()
        {
            // check the current game status, without returning the winner and points
            return gameStatus.playing;
        }

        protected gameStatus GetGameStatus(out Player o_Winner, out int o_Points)
        {
            // check the current game status
            // if there is a win or a draw, return the winner and the amount of points he got
            o_Winner = null;
            o_Points = 0;
            return gameStatus.playing;
        }
    }
}
