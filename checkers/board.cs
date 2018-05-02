using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace checkers
{
    public class Board
    {
        public Piece[,] m_Board; // TODO: CHANGE TO PRIVATE 
        private int m_Size;
        private int m_TopPlayerPoints;
        private int m_BottomPlayerPoints;

        public static int[] ALLOWED_BOARD_SIZES = { 6, 8, 10 };

        public Board() : this(8)
        {
        }

        public Board(int i_Size)
        {
            // Create a new board of specific size, init it with pieces 
            // TODO: Check if the board size is legit here also?
            m_Size = i_Size;
            m_Board = new Piece[m_Size,m_Size];
            m_TopPlayerPoints = 0; // TODO: Calculate the amount of points a player starts with
            m_BottomPlayerPoints = 0;
            initBoard();
        }
        
        private void initBoard()
        {
            // Initialized the board with the right amount of pieces
            // Leaves a null where there are no pieces
            int player1Area = (m_Size / 2) - 2;
            int player2Area = (m_Size / 2) + 1;

            for (int i = 0; i < m_Size; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    // Odd row -> Place a piece in the even columns
                    // Even row -> place a piece in the odd columns
                    if ((i <= player1Area) && (((i % 2) + (j % 2)) == 1))
                    {
                        m_Board[i,j] = new Piece(ePlayer.TopPlayer);
                    }

                    if ((i >= player2Area) && (((i % 2) + (j % 2)) == 1))
                    {
                        m_Board[i,j] = new Piece(ePlayer.BottomPlayer);
                    }
                }
            }
        }

        public void MovePiece(Move i_Move, Move? i_PreviousMove, out eMoveStatus o_MoveStatus)
        {
            if (checkMoveLegality(i_Move, i_PreviousMove))
            {
                m_Board[i_Move.End.Row, i_Move.End.Col] = m_Board[i_Move.Begin.Row, i_Move.Begin.Col];
                m_Board[i_Move.Begin.Row, i_Move.Begin.Col] = null;
                o_MoveStatus = eMoveStatus.legal;
                // If the move was a jump, need to remove the piece jumped over
                checkKing(i_Move.End);
            }
            else
            {
                o_MoveStatus = eMoveStatus.illegal;
            } 
        }

        private void checkKing(Position i_Position)
        {
            Piece piece = m_Board[i_Position.Row, i_Position.Col];

            if ((i_Position.Row == 0) && (piece.PlayerNumber == ePlayer.BottomPlayer) 
                                      && (piece.Type == ePieceType.regular))
            {
                piece.SetKing();
                // maybe m_Board[i_Position.Row, i_Position.Col].setKing()?
            }
            else if ((i_Position.Row == (m_Size - 1)) && (piece.PlayerNumber == ePlayer.TopPlayer) 
                                                      && (piece.Type == ePieceType.regular))
            {
                piece.SetKing();
            }
        }

        private bool checkMoveLegality(Move i_Move, Move? i_PreviousMove)
        {
            // TODO: also allow a player to quit
            List<Move> possibleMoves = GetPossibleMoves(i_Move.player, i_PreviousMove);
            bool legalMove = false;
            foreach(Move move in possibleMoves)
            {
                if ((move.Begin.Equals(i_Move.Begin)) && (move.End.Equals(i_Move.End)))
                    legalMove = true;
            }

            return legalMove;
        }

        public List<Move> GetPossibleMoves(ePlayer i_CurrentPlayer, Move? i_LastMove)
        {
            List<Move> possibleMoves = new List<Move>();

            // If the last move was a jump, first check if another jump is possible for that piece
            if ((i_LastMove != null) && (i_LastMove.Value.Type == eMoveType.jump))
            {
                possibleMoves = PossibleMovesForPiece(i_LastMove.Value.End);
                foreach(Move currentMove in possibleMoves)
                {
                    if (currentMove.Type == eMoveType.regular)
                        possibleMoves.Remove(currentMove);
                }

                if (possibleMoves.Count > 0)
                    return possibleMoves;
            }

            // Calculate all possible moves for a player
            for (int i = 0; i < m_Size; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    // If the piece belongs to the current player, check the possible moves for it
                    if ((m_Board[i,j] != null) && (m_Board[i,j].PlayerNumber == i_CurrentPlayer))
                        possibleMoves.AddRange(PossibleMovesForPiece(new Position(i,j)));
                }
            }

            if (isJumpPossible(possibleMoves, out List<Move> onlyJumps))
                possibleMoves = onlyJumps;

            return possibleMoves;
        }

        private bool isJumpPossible(List<Move> i_AllMovesList, out List<Move> o_OnlyJumps)
        {
            bool jumpPossible = false;
            o_OnlyJumps = new List<Move>(i_AllMovesList);

            foreach(Move move in i_AllMovesList)
            {
                if (move.Type == eMoveType.jump)
                {
                    jumpPossible = true;
                }
                else
                {
                    o_OnlyJumps.Remove(move);
                }
            }

            return jumpPossible;
        }

        public List<Move> PossibleMovesForPiece(Position i_PiecePosition)
        {
            Piece currentPiece = m_Board[i_PiecePosition.Row, i_PiecePosition.Col];
            List<Move> possibleMovesForPiece = new List<Move>();

            if (currentPiece == null) return possibleMovesForPiece;

            ePlayer player = currentPiece.PlayerNumber;

            // If the piece is a king, check moves in all directions
            if (currentPiece.Type == ePieceType.king)
            {
                possibleMovesForPiece.AddRange(possibleMovesForPieceUp(i_PiecePosition, player));
                possibleMovesForPiece.AddRange(possibleMovesForPieceDown(i_PiecePosition, player));
            }
            else
            {
                // If the piece is a regular, check moves according to the player 
                if (currentPiece.PlayerNumber == ePlayer.TopPlayer)
                {
                    possibleMovesForPiece.AddRange(possibleMovesForPieceDown(i_PiecePosition,player));
                }

                if(currentPiece.PlayerNumber == ePlayer.BottomPlayer)
                {
                    possibleMovesForPiece.AddRange(possibleMovesForPieceUp(i_PiecePosition, player));
                }
            }

            return possibleMovesForPiece;
        }

        private List<Move> possibleMovesForPieceUp(Position i_StartPosition, ePlayer i_Player)
        {            
            Position[] endPositions =
                {
                    new Position(i_StartPosition.Row - 1, i_StartPosition.Col - 1),
                    new Position(i_StartPosition.Row - 1, i_StartPosition.Col + 1)
                };
            return checkRegularMoves(i_StartPosition, endPositions, i_Player);
        }

        private List<Move> possibleMovesForPieceDown(Position i_StartPosition, ePlayer i_Player)
        {
            Position[] endPositions =
                {
                    new Position(i_StartPosition.Row + 1, i_StartPosition.Col + 1),
                    new Position(i_StartPosition.Row + 1, i_StartPosition.Col - 1)
                };
            return checkRegularMoves(i_StartPosition, endPositions, i_Player);
        }

        private List<Move> checkRegularMoves(Position i_StartPosition, Position[] i_EndPositions, ePlayer i_Player)
        {
            List<Move> regularMovesList = new List<Move>();

            foreach(Position endPosition in i_EndPositions)
            {
                eSquareStatus squareStatus = checkSquareStatus(endPosition, out ePlayer squarePlayer);
                if (squareStatus == eSquareStatus.empty)
                    regularMovesList.Add(new Move(i_StartPosition, endPosition, i_Player, eMoveType.regular));
                else if ((squareStatus == eSquareStatus.occupied) && (squarePlayer != i_Player))
                {
                    int jumpRow = i_StartPosition.Row + 2 * (endPosition.Row - i_StartPosition.Row);
                    int jumpCol = i_StartPosition.Col + 2 * (endPosition.Col - i_StartPosition.Col);
                    Position jumpPosition = new Position(jumpRow, jumpCol);
                    eSquareStatus jumpSquareStatus = checkSquareStatus(jumpPosition, out squarePlayer);
                    if (jumpSquareStatus == eSquareStatus.empty)
                        regularMovesList.Add(new Move(i_StartPosition, jumpPosition, i_Player, eMoveType.jump));
                }
            }

            return regularMovesList;
        }

        private eSquareStatus checkSquareStatus(Position i_Square, out ePlayer o_Player)
        {
            eSquareStatus squareStatus;
            o_Player = ePlayer.BottomPlayer;
            if ((i_Square.Row >= m_Size) || (i_Square.Row < 0) || (i_Square.Col >= m_Size) || (i_Square.Col < 0))
            {
                squareStatus = eSquareStatus.outOfBounds;
            }
            else if (m_Board[i_Square.Row, i_Square.Col] == null)
            {
                squareStatus = eSquareStatus.empty;
            }
            else
            {
                squareStatus = eSquareStatus.occupied;
                o_Player = m_Board[i_Square.Row, i_Square.Col].PlayerNumber;
            }                
            return squareStatus;
        }

        public Piece[,] GetBoard()
        {
            // Returns the board matrix
            return m_Board;
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
