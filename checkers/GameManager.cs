using System;
using System.Collections.Generic;
using System.Text;

namespace checkers
{
    // Todo: Clear all irrelevant "using"
    // Todo: Change ROWcol to COLrow in the input and output 
    // Todo: Create GetRandomMove(ePlayerType) method in the Board class 
    // Todo: Order all relevant enums
    // Todo: Allow playing multiple games (count points)
    // Todo: Display relevant message on game end (who won / draw)
    // Todo: Style all the code
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
        private int m_BoardSize;

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
            m_BoardSize = CheckersConsolUI.GetUserBoardSize(Board.ALLOWED_BOARD_SIZES);

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
            
            // Call playSingleGame
            playSingleGame();
        }

        private void playSingleGame()
        {
            // This method allows the user to play a single game

            // Initialize a new board and print it
            CheckersConsolUI.ClearScreen();
            m_Board = new Board(m_BoardSize);
            eGameStatus gameStatus = eGameStatus.playing;
            ePlayerPosition winner = ePlayerPosition.BottomPlayer;
            m_CurrentPlayer = m_Player1;
            Move previousMove = null;

            System.Console.Out.WriteLine("{0}'s points: {1} -- {2}'s points: {3}",
                m_Player1.Name, m_Board.GetPlayerScore(m_Player1),
                m_Player2.Name, m_Board.GetPlayerScore(m_Player2));
            CheckersConsolUI.PrintBoard(m_Board.GetBoard());

            while (gameStatus == eGameStatus.playing)
            {
                // Get a players move and preform it
                Move currentMove = GetMove(previousMove, out eMoveStatus currentMoveStatus);
                m_CurrentPlayer.AddMove(currentMove);
                
                CheckersConsolUI.ClearScreen();
                System.Console.Out.WriteLine("{0}'s points: {1} -- {2}'s points: {3}",
                    m_Player1.Name, m_Board.GetPlayerScore(m_Player1),
                    m_Player2.Name, m_Board.GetPlayerScore(m_Player2));
                CheckersConsolUI.PrintBoard(m_Board.GetBoard());

                // Todo: put this in the CheckersConsolUI method
                // CheckersConsolUI.PrintLastMove(m_CurrentPlayer, m_CurrentPlayer.GetLastMove());
                if (m_CurrentPlayer.GetLastMove() != null)
                    System.Console.Out.WriteLine("{0}'s move was (SYMBOL): {1}", m_CurrentPlayer.Name, m_CurrentPlayer.GetLastMove().ToString());

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

            int player1points = m_Board.GetPlayerScore(m_Player1);
            int player2points = m_Board.GetPlayerScore(m_Player2);

            if (gameStatus == eGameStatus.draw)
            {
                System.Console.Out.WriteLine("Game ended in a draw");
                System.Console.Out.WriteLine("{0} recieved {1} points", m_Player1.Name, player1points);
                System.Console.Out.WriteLine("{0} recieved {1} points", m_Player2.Name, player2points);
            } else if (gameStatus == eGameStatus.win)
            {
                System.Console.Out.WriteLine("{0} has won!", (m_Player1.PlayerPosition == winner) ? m_Player1.Name : m_Player2.Name);
                System.Console.Out.WriteLine("{0} recieved {1} points", m_Player1.Name, player1points);
                System.Console.Out.WriteLine("{0} recieved {1} points", m_Player2.Name, player2points);
            }

            m_Player1.Points += player1points;
            m_Player2.Points += player2points;
            System.Console.Out.WriteLine("Total number of points:");
            System.Console.Out.WriteLine("{0} recieved {1} points", m_Player1.Name, m_Player1.Points);
            System.Console.Out.WriteLine("{0} recieved {1} points", m_Player2.Name, m_Player2.Points);

        }

        private Move GetMove(Move i_PreviousMove, out eMoveStatus o_MoveStatus)
        {
            Move currentMove;
            eMoveStatus currentMoveStatus;
            if (m_CurrentPlayer.PlayerType == ePlayerType.Human)
            {
                currentMove = CheckersConsolUI.GetUserMoveInput(m_CurrentPlayer);
                m_Board.MovePiece(ref currentMove, i_PreviousMove, out currentMoveStatus);

                while (currentMoveStatus == eMoveStatus.Illegal)
                {
                    // Todo: put this method in the CheckersConsolUI. Idea: add a flag to GetUserMoveInput that says the move was invalid
                    System.Console.Out.WriteLine("Invalid move, enter a new one:");
                    currentMove = CheckersConsolUI.GetUserMoveInput(m_CurrentPlayer);
                    m_Board.MovePiece(ref currentMove, i_PreviousMove, out currentMoveStatus);
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
