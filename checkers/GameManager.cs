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
    public enum eMoveStatus { legal, illegal } // syntax error should be checked in the UI part
    public enum eListOfMessages { } // all possible ui messages 
    public enum eSquareStatus { empty, outOfBounds, occupied}

    public class GameManager
    {
        Board m_Board;
        Player m_Player1;
        Player m_Player2;
        Move?[] m_MoveHistory; // is it relevant?
        Player m_CurrentPlayer;

        public GameManager()
        {
            // Default constructor
            m_Player1 = new Player(ePlayer.BottomPlayer);
            m_Player2 = new Player(ePlayer.TopPlayer);
        }

        public void Start()
        {
            // Runs several games with the same configuration

            // User name
            m_Player1.Name = CheckersConsolUI.GetUserNameInput();

            // Board size
            int boardSize = CheckersConsolUI.GetUserBoardSize();
            m_Board = new Board(boardSize);

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
            CheckersConsolUI.ClearScreen();
            System.Console.Out.WriteLine("======================================");
            System.Console.Out.WriteLine("\t  {0} vs. {1}!!", m_Player1.Name, m_Player2.Name);
            System.Console.Out.WriteLine("\t\tReady?");
            System.Console.Out.WriteLine("======================================");
            System.Console.In.ReadLine();
            PlayGame();
        }

        public void PlayGame()
        {
            // Play a single game
            // Draw the initialized board
            CheckersConsolUI.PrintBoard(m_Board.GetBoard());

            // For human - write who's turn it is and get an input
            // For computer - get a random move
        }

        private eMoveStatus checkMoveLegalality(Move i_Move)
        {
            // check if the move is legal
            return eMoveStatus.legal;
        }

        private Move getRandomMove(Player i_Player)
        {
            // returns a move for a specific player, from all possible moves 
            return new Move();
        }

        private bool isValidBoardSize(int i_Size)
        {
            return false;
        }

        private void changeActivePlayer()
        {
            // changes the active player to the other one
        }

    }
}
