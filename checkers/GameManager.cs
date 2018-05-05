namespace checkers
{
    public enum eBoardSize
    {
        Samll = 6,
        Medium = 8,
        Large = 10
    }

    public enum eGameStatus
    {
        Playing,
        Win,
        Draw,
        Forfit
    }

    public enum ePlayerType
    {
        Human,
        Computer
    }

    public enum eMoveType
    {
        Regular,
        Jump
    }

    public enum eMoveStatus
    {
        Legal,
        Illegal,
        AnotherJumpPossible
    } 

    public enum eSquareStatus
    {
        Empty,
        OutOfBounds,
        Occupied
    }

    public class GameManager
    {
        private const int k_MaxNameSize = 20;
        private readonly Player r_Player1;
        private readonly Player r_Player2;
        private readonly CheckersConsolUI r_UI;
        private Board m_Board;
        private int m_BoardSize;
        private Player m_CurrentPlayer;

        public GameManager()
        {
            r_Player1 = new Player(ePlayerPosition.BottomPlayer);
            r_Player2 = new Player(ePlayerPosition.TopPlayer);
            m_CurrentPlayer = r_Player1;
            r_UI = new CheckersConsolUI();
        }

        public void Start()
        {
            r_Player1.Name = r_UI.GetUserNameInput(k_MaxNameSize);

            m_BoardSize = r_UI.GetUserBoardSize(Board.AllowedBoardSizes);

            r_Player2.PlayerType = r_UI.GetPlayerType();
            r_Player2.Name = r_Player2.PlayerType == ePlayerType.Human
                                 ? r_UI.GetUserNameInput(k_MaxNameSize)
                                 : "Computer";

            bool continuePlaying = true;

            while (continuePlaying)
            {
                playSingleGame();
                continuePlaying = r_UI.GetUserAnotherGameInput();
            }

            r_UI.EndGameMessage();
        }

        private void playSingleGame()
        {
            // Initialize a new game - new board, players history, game status and starting player
            r_UI.ClearScreen();
            m_Board = new Board(m_BoardSize);
            eGameStatus gameStatus = eGameStatus.Playing;
            ePlayerPosition winner = ePlayerPosition.BottomPlayer;
            r_Player1.ClearMoveHistory();
            r_Player2.ClearMoveHistory();
            m_CurrentPlayer = r_Player1;
            Move previousMove = null;

            while (gameStatus == eGameStatus.Playing)
            {
                r_UI.ClearScreen();
                r_UI.PrintBoard(m_Board.BoardMatrix);
                r_UI.PrintLastMove(otherPlayer());

                // Get a players move and preform it
                Move currentMove = getMove(previousMove, out eMoveStatus currentMoveStatus);
                m_CurrentPlayer.AddMove(currentMove);

                // If the player can not preform another jump, change player
                if (currentMoveStatus == eMoveStatus.AnotherJumpPossible)
                {
                    previousMove = m_CurrentPlayer.GetLastMove();
                }
                else
                {
                    changeActivePlayer();
                    previousMove = null;
                }

                gameStatus = m_Board.GetGameStatus(m_CurrentPlayer, out winner);
            }

            concludeSingleGame(gameStatus, winner);
        }

        private void concludeSingleGame(eGameStatus i_GameStatus, ePlayerPosition i_Winner)
        {
            r_UI.ClearScreen();
            r_UI.PrintBoard(m_Board.BoardMatrix);

            int player1Points = m_Board.GetPlayerScore(r_Player1);
            int player2Points = m_Board.GetPlayerScore(r_Player2);

            switch (i_GameStatus)
            {
                case eGameStatus.Draw:
                    r_UI.Draw();
                    break;
                case eGameStatus.Win:
                    string winner = (r_Player1.PlayerPosition == i_Winner) ? r_Player1.Name : r_Player2.Name;
                    r_UI.Winning(winner);
                    break;
                case eGameStatus.Forfit:
                    string forfiter = (r_Player1.PlayerPosition == i_Winner) ? r_Player2.Name : r_Player1.Name;
                    r_UI.PlayerForfited(forfiter);
                    break;
            }

            r_Player1.Points += player1Points;
            r_Player2.Points += player2Points;

            r_UI.PlayerRecivedPoints(r_Player1.Name, player1Points);
            r_UI.PlayerRecivedPoints(r_Player2.Name, player2Points);

            r_UI.PointStatus(r_Player1.Name, r_Player1.Points, r_Player2.Name, r_Player2.Points);
        }

        private Move getMove(Move i_PreviousMove, out eMoveStatus o_MoveStatus)
        {
            Move currentMove = null;
            eMoveStatus currentMoveStatus = eMoveStatus.Illegal;
            if (m_CurrentPlayer.PlayerType == ePlayerType.Human)
            {
                while (currentMoveStatus == eMoveStatus.Illegal)
                {
                    currentMove = r_UI.GetUserMoveInput(m_CurrentPlayer, out bool forfitFlag);
                    if (forfitFlag)
                    {
                        m_Board.PlayerForfit(m_CurrentPlayer, out currentMoveStatus);
                        if (currentMoveStatus == eMoveStatus.Illegal)
                        {
                            r_UI.NotAllowedForfit();
                        }
                    }
                    else
                    {
                        m_Board.MovePiece(ref currentMove, i_PreviousMove, out currentMoveStatus);
                        if (currentMoveStatus == eMoveStatus.Illegal)
                        {
                            r_UI.InValidMove();
                        }
                    }
                }
            }
            else
            {
                currentMove = m_Board.GetRandomMove(m_CurrentPlayer.PlayerPosition);
                m_Board.MovePiece(ref currentMove, i_PreviousMove, out currentMoveStatus);
            }

            o_MoveStatus = currentMoveStatus;

            return currentMove;
        }

        private void changeActivePlayer()
        {
            m_CurrentPlayer = (m_CurrentPlayer == r_Player1) ? r_Player2 : r_Player1;
        }

        private Player otherPlayer()
        {
            return (m_CurrentPlayer == r_Player1) ? r_Player2 : r_Player1;
        }
    }
}