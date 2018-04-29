using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace checkers
{
    class Board
    {
        private Piece?[,] m_Board;
        private int m_Size;

        public Board()
        {
            // default constructor
        }

        public Board(eBoardSize i_Size)
        {
            m_Size = (int) i_Size;
            m_Board = new Piece?[m_Size,m_Size];
            initBoard();
            // create a new board of specific size, init it with pieces
        }

        private void initBoard()
        {
            // initialized the board with the right amount of pieces
            // init null where there are no objects
            int player1Area = (m_Size / 2) - 2;
            int player2Area = (m_Size / 2) + 1;

            for (int i = 0; i < m_Size; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    if ((i <= player1Area) || (i >= player2Area))
                    {
                        // Odd row -> Place a piece in the even columns
                        // Even row -> place a piece in the odd columns
                        if (((i % 2) + (j % 2)) == 1)
                        {
                            ePiecePlayer player = (i <= player1Area) ? ePiecePlayer.player1 : ePiecePlayer.player2;
                            m_Board[i,j] = new Piece(player);
                        }
                    }
                }
            }
        }

        protected void MovePiece(Move i_Move, out eMoveStatus o_MoveStatus) // naming convention for out variables? 
        {
            o_MoveStatus = eMoveStatus.legal; // place holder
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

        protected eGameStatus GetGameStatus()
        {
            // check the current game status, without returning the winner and points
            return eGameStatus.playing;
        }

        protected eGameStatus GetGameStatus(out Player o_Winner, out int o_Points)
        {
            // check the current game status
            // if there is a win or a draw, return the winner and the amount of points he got
            o_Winner = null;
            o_Points = 0;
            return eGameStatus.playing;
        }
    }
}
