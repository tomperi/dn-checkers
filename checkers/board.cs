using System;
using System.Collections.Generic;

namespace checkers
{
    public class Board
    {
        private const int k_KingPointsWorth = 4;
        private const int k_RegularPointsWorth = 1;
        private const int k_Difficulty = 3;
        private static readonly int[] sr_AllowedBoardSizes = { 6, 8, 10 };
        private readonly int r_Size;

        private Piece[,] m_BoardMatrix;
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
            r_Size = i_Size;
            m_BoardMatrix = new Piece[r_Size, r_Size];
            m_TopPlayerPoints = 0;
            m_BottomPlayerPoints = 0;
            initBoard();
        }

        private Board(Piece[,] i_BoardMatrix, int i_TopPlayerPoints, int i_BottomPlayerPoints)
        {
            m_BoardMatrix = i_BoardMatrix;
            r_Size = m_BoardMatrix.Length;
            m_TopPlayerPoints = i_TopPlayerPoints;
            m_BottomPlayerPoints = i_BottomPlayerPoints;
        }

        private static bool notJump(Move i_Movemove)
        {
            return i_Movemove.Type != eMoveType.Jump;
        }

        private void initBoard()
        {
            // Initialized the board with the right amount of pieces
            // Leaves a null where there are no pieces
            int topPlayerArea = (r_Size / 2) - 2;
            int bottomPlayerArea = (r_Size / 2) + 1;

            for (int i = 0; i < r_Size; i++)
            {
                for (int j = 0; j < r_Size; j++)
                {
                    // Odd row -> Place a piece in the even columns
                    // Even row -> place a piece in the odd columns
                    if (i <= topPlayerArea && (i % 2) + (j % 2) == 1)
                    {
                        m_BoardMatrix[i, j] = new Piece(ePlayerPosition.TopPlayer);
                        m_TopPlayerPoints += k_RegularPointsWorth;
                    }

                    if (i >= bottomPlayerArea && (i % 2) + (j % 2) == 1)
                    {
                        m_BoardMatrix[i, j] = new Piece(ePlayerPosition.BottomPlayer);
                        m_BottomPlayerPoints += k_RegularPointsWorth;
                    }
                }
            }
        }

        public void MovePiece(ref Move io_Move, Move i_PreviousMove, out eMoveStatus o_MoveStatus)
        {
            o_MoveStatus = eMoveStatus.Illegal;
            if (checkMoveLegality(ref io_Move, i_PreviousMove))
            {
                o_MoveStatus = eMoveStatus.Legal;
                changePiecePosition(io_Move);
                checkKing(io_Move.End);
                if (io_Move.Type == eMoveType.Jump)
                {
                    removedJumpedOverPiece(io_Move);
                    if (isJumpPossible(possibleMovesForPiece(io_Move.End), out List<Move> jumpsList))
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
            m_BoardMatrix[i_Move.End.Row, i_Move.End.Col] = m_BoardMatrix[i_Move.Begin.Row, i_Move.Begin.Col];
            m_BoardMatrix[i_Move.Begin.Row, i_Move.Begin.Col] = null;
        }

        private void removedJumpedOverPiece(Move i_Move)
        {
            int row = i_Move.Begin.Row > i_Move.End.Row ? i_Move.Begin.Row - 1 : i_Move.Begin.Row + 1;
            int col = i_Move.Begin.Col > i_Move.End.Col ? i_Move.Begin.Col - 1 : i_Move.Begin.Col + 1;

            int numOfPoints = m_BoardMatrix[row, col].Type == ePieceType.Regular ? k_RegularPointsWorth : k_KingPointsWorth;
            changePoints(m_BoardMatrix[row, col].PlayerPosition, -numOfPoints);

            m_BoardMatrix[row, col] = null;
        }

        private void changePoints(ePlayerPosition i_Player, int i_NumOfPoints)
        {
            if (i_Player == ePlayerPosition.BottomPlayer)
            {
                m_BottomPlayerPoints += i_NumOfPoints;
            }
            else
            {
                m_TopPlayerPoints += i_NumOfPoints;
            }
        }

        private void checkKing(Position i_Position)
        {
            Piece piece = m_BoardMatrix[i_Position.Row, i_Position.Col];

            if (i_Position.Row == 0 && piece.PlayerPosition == ePlayerPosition.BottomPlayer
                                    && piece.Type == ePieceType.Regular)
            {
                piece.SetKing();
                changePoints(ePlayerPosition.BottomPlayer, k_KingPointsWorth - k_RegularPointsWorth);
            }
            else if (i_Position.Row == r_Size - 1 && piece.PlayerPosition == ePlayerPosition.TopPlayer
                                                  && piece.Type == ePieceType.Regular)
            {
                piece.SetKing();
                changePoints(ePlayerPosition.TopPlayer, k_KingPointsWorth - k_RegularPointsWorth);
            }
        }

        private bool checkMoveLegality(ref Move io_Move, Move i_PreviousMove)
        {
            List<Move> possibleMoves = GetPossibleMoves(io_Move.Player, i_PreviousMove);
            bool legalMove = false;
            foreach (Move move in possibleMoves)
            {
                if (move.Begin.Equals(io_Move.Begin) && move.End.Equals(io_Move.End))
                {
                    legalMove = true;
                    io_Move.Type = move.Type;
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
            if (i_LastMove != null && i_LastMove.Type == eMoveType.Jump)
            {
                possibleMoves = possibleMovesForPiece(i_LastMove.End);
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
                for (int i = 0; i < r_Size; i++)
                {
                    for (int j = 0; j < r_Size; j++)
                    {
                        // If the piece belongs to the current player, check the possible moves for it
                        if (m_BoardMatrix[i, j] != null && m_BoardMatrix[i, j].PlayerPosition == i_CurrentPlayer)
                        {
                            possibleMoves?.AddRange(possibleMovesForPiece(new Position(i, j)));
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
                if (move.Type == eMoveType.Jump)
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

        private List<Move> possibleMovesForPiece(Position i_PiecePosition)
        {
            Piece currentPiece = m_BoardMatrix[i_PiecePosition.Row, i_PiecePosition.Col];
            List<Move> possibleMovesForPiece = new List<Move>();

            if (currentPiece != null)
            {
                ePlayerPosition player = currentPiece.PlayerPosition;

                // If the piece is a king, check moves in all directions
                if (currentPiece.Type == ePieceType.King)
                {
                    possibleMovesForPiece.AddRange(possibleMovesForPieceUp(i_PiecePosition, player));
                    possibleMovesForPiece.AddRange(possibleMovesForPieceDown(i_PiecePosition, player));
                }
                else
                {
                    // If the piece is a regular, check moves according to the player 
                    switch (currentPiece.PlayerPosition)
                    {
                        case ePlayerPosition.TopPlayer:
                            possibleMovesForPiece.AddRange(possibleMovesForPieceDown(i_PiecePosition, player));
                            break;
                        case ePlayerPosition.BottomPlayer:
                            possibleMovesForPiece.AddRange(possibleMovesForPieceUp(i_PiecePosition, player));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
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
                if (squareStatus == eSquareStatus.Empty)
                {
                    regularMovesList.Add(new Move(i_StartPosition, endPosition, i_Player, eMoveType.Regular));
                }
                else if (squareStatus == eSquareStatus.Occupied && squarePlayer != i_Player)
                {
                    int jumpRow = i_StartPosition.Row + (2 * (endPosition.Row - i_StartPosition.Row));
                    int jumpCol = i_StartPosition.Col + (2 * (endPosition.Col - i_StartPosition.Col));
                    Position jumpPosition = new Position(jumpRow, jumpCol);
                    eSquareStatus jumpSquareStatus = checkSquareStatus(jumpPosition, out squarePlayer);
                    if (jumpSquareStatus == eSquareStatus.Empty)
                    {
                        regularMovesList.Add(new Move(i_StartPosition, jumpPosition, i_Player, eMoveType.Jump));
                    }
                }
                else if (squareStatus == eSquareStatus.OutOfBounds)
                {
                }
            }

            return regularMovesList;
        }

        private eSquareStatus checkSquareStatus(Position i_Square, out ePlayerPosition o_Player)
        {
            eSquareStatus squareStatus;
            o_Player = ePlayerPosition.BottomPlayer;
            if (i_Square.Row >= r_Size || i_Square.Row < 0 || i_Square.Col >= r_Size || i_Square.Col < 0)
            {
                squareStatus = eSquareStatus.OutOfBounds;
            }
            else if (m_BoardMatrix[i_Square.Row, i_Square.Col] == null)
            {
                squareStatus = eSquareStatus.Empty;
            }
            else
            {
                squareStatus = eSquareStatus.Occupied;
                o_Player = m_BoardMatrix[i_Square.Row, i_Square.Col].PlayerPosition;
            }

            return squareStatus;
        }

        public Piece[,] BoardMatrix
        {
            get
            {
                return m_BoardMatrix;
            }
        }

        public static int[] AllowedBoardSizes
        {
            get
            {
                return sr_AllowedBoardSizes;
            }
        }

        public eGameStatus GetGameStatus(Player i_CurrentPlayer, out ePlayerPosition o_Winner)
        {
            // Check the current game status
            // Win -> The current player has no possible moves, the other player wins
            //        The current player has no pieces left, the other player wins
            //        The current player has forfited and has fewer points, the other player wins
            // Draw -> Both players have no possible moves 
            //         A player has forfited when the scoring is a tie 
            eGameStatus currentStatus = eGameStatus.Playing;
            ePlayerPosition winner = ePlayerPosition.TopPlayer;

            if (m_PlayerHasForfit)
            {
                ePlayerPosition otherPlayer = (m_PlayerForfit == ePlayerPosition.TopPlayer)
                                  ? ePlayerPosition.BottomPlayer
                                  : ePlayerPosition.TopPlayer;
                if (GetPlayerScore(m_PlayerForfit) == GetPlayerScore(otherPlayer))
                {
                    currentStatus = eGameStatus.Draw;
                }
                else
                {
                    currentStatus = eGameStatus.Forfit;
                    winner = otherPlayer;
                }
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
                    currentStatus = eGameStatus.Draw;
                    if (otherPlayerPossibleMoves != 0)
                    {
                        currentStatus = eGameStatus.Win;
                        winner = otherPlayer;
                    }
                }
            }

            o_Winner = winner;

            return currentStatus;
        }

        public Move GetRandomMove(ePlayerPosition i_Player, Move i_PreviousMove)
        {
            List<Move> listOfMoves = GetPossibleMoves(i_Player, i_PreviousMove);
            Random random = new Random();
            int randomPosition = random.Next(listOfMoves.Count);

            return listOfMoves[randomPosition];
        }

        public int GetPlayerScore(ePlayerPosition i_Player)
        {
            return i_Player == ePlayerPosition.BottomPlayer ? m_BottomPlayerPoints : m_TopPlayerPoints;
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