using System;
using System.Collections.Generic;
using System.Text;

namespace checkers
{
    // add e_ to all enums
    public enum eBoardSize { small = 6, medium = 8, large = 10}
    public enum eGameStatus { playing, win, draw }
    public enum ePlayerType { Human, Computer }
    public enum eMoveType { regular, jump }
    public enum eMoveStatus { Legal, Illegal, AnotherJumpPossible} // syntax error should be checked in the UI part
    public enum eListOfMessages { } // all possible ui messages 
    public enum eSquareStatus { empty, outOfBounds, occupied}

    public class GameManager
    {
        Board m_Board;
        Player m_Player1;
        Player m_Player2;
        Player m_CurrentPlayer;

        public GameManager()
        {
            // Default constructor
            m_Player1 = new Player(ePlayerPosition.BottomPlayer);
            m_Player2 = new Player(ePlayerPosition.TopPlayer);
            m_CurrentPlayer = m_Player1;
        }

        public void Start()
        {
            // Runs several games with the same configuration

            // User name
            m_Player1.Name = CheckersConsolUI.GetUserNameInput();

            // Board size
            int boardSize = CheckersConsolUI.GetUserBoardSize(Board.ALLOWED_BOARD_SIZES);
            m_Board = new Board(boardSize); // Todo: move the creation and initialization of the board to the PlayGame() method

            // Choose human/computer opponent, if human, enter name
            m_Player2.PlayerType = CheckersConsolUI.GetPlayerType();
            if (m_Player2.PlayerType == ePlayerType.Human)
            {
                m_Player2.Name = CheckersConsolUI.GetUserNameInput();
            }
            else
            {
                m_Player2.Name = "Computer";
            }
            
            // Call PlayGame
            PlayGame();
        }

        public void PlayGame()
        {
            // Play a single game
            // Draw the initialized board
            eGameStatus gameStatus = eGameStatus.playing;
            CheckersConsolUI.ClearScreen();
            CheckersConsolUI.PrintBoard(m_Board.GetBoard());

            while (gameStatus == eGameStatus.playing)
            {
//                List<Move> listOfPossibleMoves =
//                    m_Board.GetPossibleMoves(m_CurrentPlayer.PlayerPosition, m_CurrentPlayer.GetLastMove());
//                CheckersConsolUI.PrintListOfMoves(listOfPossibleMoves);

                Move currentMove = CheckersConsolUI.GetUserMoveInput(m_CurrentPlayer);
                m_Board.MovePiece(ref currentMove, m_CurrentPlayer.GetLastMove(), out eMoveStatus currentMoveStatus);

                CheckersConsolUI.PrintBoard(m_Board.GetBoard());

                while (currentMoveStatus == eMoveStatus.Illegal)
                {
                    // Todo: put this method in the CheckersConsolUI. Idea: add a flag to GetUserMoveInput that says the move was invalid
                    System.Console.Out.WriteLine("Invalid move, enter a new one:");
                    currentMove = CheckersConsolUI.GetUserMoveInput(m_CurrentPlayer);
                    m_Board.MovePiece(ref currentMove, m_CurrentPlayer.GetLastMove(), out currentMoveStatus);
                }


                m_CurrentPlayer.AddMove(currentMove);

                gameStatus = m_Board.GetGameStatus(out ePlayerPosition winner, out int winnerPoints, out int loserPoints);

                // Print the current players last move
                CheckersConsolUI.PrintMove(m_CurrentPlayer.GetLastMove());

                // If the player can not preform another jump, change player
                if (currentMoveStatus != eMoveStatus.AnotherJumpPossible)
                {
                    changeActivePlayer();
                }

                CheckersConsolUI.ClearScreen();
                CheckersConsolUI.PrintBoard(m_Board.GetBoard());
                System.Console.Out.WriteLine("{0}'s points: {1} -- {2}'s points: {3}", 
                    m_Player1.Name, m_Board.GetPlayerScore(m_Player1), 
                    m_Player2.Name, m_Board.GetPlayerScore(m_Player2));
            }




            // For human - write who's turn it is and get an input
            // For computer - get a random move
        }

        private Move getRandomMove(Player i_Player)
        {
            // returns a move for a specific player, from all possible moves 
            return null;
        }

        private void changeActivePlayer()
        {
            // Changes the active player to the other one
            if (m_CurrentPlayer == m_Player1)
            {
                m_CurrentPlayer = m_Player2;
            }
            else
            {
                m_CurrentPlayer = m_Player1;
            }
        }

    }
}
