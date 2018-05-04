using System;
using System.Collections.Generic;

namespace checkers
{
    public class Board
    {
        public static int[] ALLOWED_BOARD_SIZES = { 6, 8, 10 };
        private static readonly int kingPointsWorth = 4;
        private static readonly int regularPointsWorth = 1;
        private readonly int m_Size;

        private Piece[,] m_Board;
        private int m_BottomPlayerPoints;
        private ePlayerPosition m_PlayerForfit;
        private bool m_PlayerHasForfit;
        private int m_TopPlayerPoints;

        public Board()
            : this(8)
        {
        }

        public Board(int i_Size)
        {
            // Create a new board of specific size, init it with pieces 
            m_Size = i_Size;
            m_Board = new Piece[m_Size, m_Size];
            m_TopPlayerPoints = 0;
            m_BottomPlayerPoints = 0;
            initBoard();
        }

        private static bool notJump(Move move)
        {
            return move.Type == eMoveType.jump ? false : true;
        }

        private void initBoard()
        {
            // Initialized the board with the right amount of pieces
            // Leaves a null where there are no pieces
            int topPlayerArea = (m_Size / 2) - 2;
            int bottomPlayerArea = (m_Size / 2) + 1;

            for (int i = 0; i < m_Size; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    // Odd row -> Place a piece in the even columns
                    // Even row -> place a piece in the odd columns
                    if (i <= topPlayerArea && (i % 2) + (j % 2) == 1)
                    {
                        m_Board[i, j] = new Piece(ePlayerPosition.TopPlayer);
                        m_TopPlayerPoints += regularPointsWorth;
                    }

                    if (i >= bottomPlayerArea && (i % 2) + (j % 2) == 1)
                    {
                        m_Board[i, j] = new Piece(ePlayerPosition.BottomPlayer);
                        m_BottomPlayerPoints += regularPointsWorth;
                    }
                }
            }
        }

        public void MovePiece(ref Move i_Move, Move i_PreviousMove, out eMoveStatus o_MoveStatus)
        {
            o_MoveStatus = eMoveStatus.Illegal;
            if (checkMoveLegality(ref i_Move, i_PreviousMove))
            {
                o_MoveStatus = eMoveStatus.Legal;
                changePiecePosition(i_Move);
                checkKing(i_Move.End);
                if (i_Move.Type == eMoveType.jump)
                {
                    removedJumpedOverPiece(i_Move);
                    if (isJumpPossible(PossibleMovesForPiece(i_Move.End), out List<Move> jumpsList))
                    {
                        o_MoveStatus = eMoveStatus.AnotherJumpPossible;
                    }
                }
            }
            else
            {
                o_MoveStatus = eMoveStatus.Illegal;
            }
        }

        private void changePiecePosition(Move i_Move)
        {
            m_Board[i_Move.End.Row, i_Move.End.Col] = m_Board[i_Move.Begin.Row, i_Move.Begin.Col];
            m_Board[i_Move.Begin.Row, i_Move.Begin.Col] = null;
        }

        private void removedJumpedOverPiece(Move i_Move)
        {
            int row = i_Move.Begin.Row > i_Move.End.Row ? i_Move.Begin.Row - 1 : i_Move.Begin.Row + 1;
            int col = i_Move.Begin.Col > i_Move.End.Col ? i_Move.Begin.Col - 1 : i_Move.Begin.Col + 1;

            int numOfPoints = m_Board[row, col].Type == ePieceType.regular ? regularPointsWorth : kingPointsWorth;
            changePoints(m_Board[row, col].PlayerPosition, -numOfPoints);

            m_Board[row, col] = null;
        }

        private void changePoints(ePlayerPosition i_Player, int numOfPoints)
        {
            if (i_Player == ePlayerPosition.BottomPlayer)
            {
                m_BottomPlayerPoints += numOfPoints;
            }
            else
            {
                m_TopPlayerPoints += numOfPoints;
            }
        }

        private void checkKing(Position i_Position)
        {
            Piece piece = m_Board[i_Position.Row, i_Position.Col];

            if (i_Position.Row == 0 && piece.PlayerPosition == ePlayerPosition.BottomPlayer
                                    && piece.Type == ePieceType.regular)
            {
                piece.SetKing();
                changePoints(ePlayerPosition.BottomPlayer, kingPointsWorth - regularPointsWorth);
            }
            else if (i_Position.Row == m_Size - 1 && piece.PlayerPosition == ePlayerPosition.TopPlayer
                                                  && piece.Type == ePieceType.regular)
            {
                piece.SetKing();
                changePoints(ePlayerPosition.TopPlayer, kingPointsWorth - regularPointsWorth);
            }
        }

        private bool checkMoveLegality(ref Move i_Move, Move i_PreviousMove)
        {
            List<Move> possibleMoves = GetPossibleMoves(i_Move.Player, i_PreviousMove);
            bool legalMove = false;
            foreach (Move move in possibleMoves)
            {
                if (move.Begin.Equals(i_Move.Begin) && move.End.Equals(i_Move.End))
                {
                    legalMove = true;
                    i_Move.Type = move.Type;
                }
            }

            return legalMove;
        }

        public List<Move> GetPossibleMoves(ePlayerPosition i_CurrentPlayer)
        {
            return GetPossibleMoves(i_CurrentPlayer, null);
        }

        public List<Move> GetPossibleMoves(ePlayerPosition i_CurrentPlayer, Move i_LastMove)
        {
            List<Move> possibleMoves = new List<Move>();
            bool multipleJumpsPossible = false;

            // If the last move was a jump, first check if another jump is possible for that piece
            if (i_LastMove != null && i_LastMove.Type == eMoveType.jump)
            {
                possibleMoves = PossibleMovesForPiece(i_LastMove.End);
                if (possibleMoves != null)
                {
                    possibleMoves.RemoveAll(notJump);
                    if (possibleMoves.Count > 0)
                    {
                        multipleJumpsPossible = true;
                    }
                }
            }

            if (!multipleJumpsPossible)
            {
                // Calculate all possible moves for a player
                for (int i = 0; i < m_Size; i++)
                {
                    for (int j = 0; j < m_Size; j++)
                    {
                        // If the piece belongs to the current player, check the possible moves for it
                        if (m_Board[i, j] != null && m_Board[i, j].PlayerPosition == i_CurrentPlayer)
                        {
                            possibleMoves.AddRange(PossibleMovesForPiece(new Position(i, j)));
                        }
                    }
                }

                if (isJumpPossible(possibleMoves, out List<Move> onlyJumps))
                {
                    possibleMoves = onlyJumps;
                }
            }

            return possibleMoves;
        }

        private bool isJumpPossible(List<Move> i_AllMovesList, out List<Move> o_OnlyJumps)
        {
            bool jumpPossible = false;
            o_OnlyJumps = new List<Move>(i_AllMovesList);

            foreach (Move move in i_AllMovesList)
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

            if (currentPiece != null)
            {
                ePlayerPosition player = currentPiece.PlayerPosition;

                // If the piece is a king, check moves in all directions
                if (currentPiece.Type == ePieceType.king)
                {
                    possibleMovesForPiece.AddRange(possibleMovesForPieceUp(i_PiecePosition, player));
                    possibleMovesForPiece.AddRange(possibleMovesForPieceDown(i_PiecePosition, player));
                }
                else
                {
                    // If the piece is a regular, check moves according to the player 
                    if (currentPiece.PlayerPosition == ePlayerPosition.TopPlayer)
                    {
                        possibleMovesForPiece.AddRange(possibleMovesForPieceDown(i_PiecePosition, player));
                    }

                    if (currentPiece.PlayerPosition == ePlayerPosition.BottomPlayer)
                    {
                        possibleMovesForPiece.AddRange(possibleMovesForPieceUp(i_PiecePosition, player));
                    }
                }
            }

            return possibleMovesForPiece;
        }

        private List<Move> possibleMovesForPieceUp(Position i_StartPosition, ePlayerPosition i_Player)
        {
            Position[] endPositions =
                {
                    new Position(i_StartPosition.Row - 1, i_StartPosition.Col - 1),
                    new Position(i_StartPosition.Row - 1, i_StartPosition.Col + 1)
                };
            return checkMove(i_StartPosition, endPositions, i_Player);
        }

        private List<Move> possibleMovesForPieceDown(Position i_StartPosition, ePlayerPosition i_Player)
        {
            Position[] endPositions =
                {
                    new Position(i_StartPosition.Row + 1, i_StartPosition.Col + 1),
                    new Position(i_StartPosition.Row + 1, i_StartPosition.Col - 1)
                };
            return checkMove(i_StartPosition, endPositions, i_Player);
        }

        private List<Move> checkMove(Position i_StartPosition, Position[] i_EndPositions, ePlayerPosition i_Player)
        {
            List<Move> regularMovesList = new List<Move>();

            foreach (Position endPosition in i_EndPositions)
            {
                eSquareStatus squareStatus = checkSquareStatus(endPosition, out ePlayerPosition squarePlayer);
                if (squareStatus == eSquareStatus.empty)
                {
                    regularMovesList.Add(new Move(i_StartPosition, endPosition, i_Player, eMoveType.regular));
                }
                else if (squareStatus == eSquareStatus.occupied && squarePlayer != i_Player)
                {
                    int jumpRow = i_StartPosition.Row + (2 * (endPosition.Row - i_StartPosition.Row));
                    int jumpCol = i_StartPosition.Col + (2 * (endPosition.Col - i_StartPosition.Col));
                    Position jumpPosition = new Position(jumpRow, jumpCol);
                    eSquareStatus jumpSquareStatus = checkSquareStatus(jumpPosition, out squarePlayer);
                    if (jumpSquareStatus == eSquareStatus.empty)
                    {
                        regularMovesList.Add(new Move(i_StartPosition, jumpPosition, i_Player, eMoveType.jump));
                    }
                }
            }

            return regularMovesList;
        }

        private eSquareStatus checkSquareStatus(Position i_Square, out ePlayerPosition o_Player)
        {
            eSquareStatus squareStatus;
            o_Player = ePlayerPosition.BottomPlayer;
            if (i_Square.Row >= m_Size || i_Square.Row < 0 || i_Square.Col >= m_Size || i_Square.Col < 0)
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
                o_Player = m_Board[i_Square.Row, i_Square.Col].PlayerPosition;
            }

            return squareStatus;
        }

        public Piece[,] GetBoard()
        {
            // Returns the board matrix
            return m_Board;
        }

        public eGameStatus GetGameStatus(Player i_CurrentPlayer, out ePlayerPosition o_Winner)
        {
            // Check the current game status
            // Win -> The current player has no possible moves, the other player wins
            //        The current player has no pieces left, the other player wins
            // Draw -> Both players have no possible moves
            eGameStatus currentStatus = eGameStatus.playing;
            ePlayerPosition winner = ePlayerPosition.TopPlayer;

            if (m_PlayerHasForfit)
            {
                winner = m_PlayerForfit == ePlayerPosition.TopPlayer
                             ? ePlayerPosition.BottomPlayer
                             : ePlayerPosition.TopPlayer;
                currentStatus = eGameStatus.forfit;
            }
            else
            {
                ePlayerPosition currentPlayer = i_CurrentPlayer.PlayerPosition;
                ePlayerPosition otherPlayer = currentPlayer == ePlayerPosition.BottomPlayer
                                                  ? ePlayerPosition.TopPlayer
                                                  : ePlayerPosition.BottomPlayer;

                int currentPlayerPossibleMoves = GetPossibleMoves(currentPlayer).Count;
                int otherPlayerPossibleMoves = GetPossibleMoves(otherPlayer).Count;

                // In case one of the players has no move, the game is either a draw or a win
                if (currentPlayerPossibleMoves == 0 || otherPlayerPossibleMoves == 0)
                {
                    currentStatus = eGameStatus.draw;
                    if (otherPlayerPossibleMoves != 0)
                    {
                        currentStatus = eGameStatus.win;
                        winner = otherPlayer;
                    }
                }
            }

            o_Winner = winner;

            return currentStatus;
        }

        public Move GetRandomMove(ePlayerPosition i_Player)
        {
            List<Move> listOfMoves = GetPossibleMoves(i_Player);
            Random random = new Random();
            int randomPosition = random.Next(listOfMoves.Count);

            return listOfMoves[randomPosition];
        }

        public int GetPlayerScore(Player player)
        {
            return player.PlayerPosition == ePlayerPosition.BottomPlayer ? m_BottomPlayerPoints : m_TopPlayerPoints;
        }

        public void PlayerForfit(Player i_PlayerForfit, out eMoveStatus i_CurrentMoveStatus)
        {
            int forfitPlayerPoints = i_PlayerForfit.PlayerPosition == ePlayerPosition.BottomPlayer
                                         ? m_BottomPlayerPoints
                                         : m_TopPlayerPoints;
            int otherPlayerPoints = i_PlayerForfit.PlayerPosition == ePlayerPosition.BottomPlayer
                                        ? m_TopPlayerPoints
                                        : m_BottomPlayerPoints;
            if (forfitPlayerPoints <= otherPlayerPoints)
            {
                i_CurrentMoveStatus = eMoveStatus.Legal;
                m_PlayerHasForfit = true;
                m_PlayerForfit = i_PlayerForfit.PlayerPosition;
            }
            else
            {
                i_CurrentMoveStatus = eMoveStatus.Illegal;
            }
        }
    }
}