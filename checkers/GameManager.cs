﻿using System;
using System.Text;

namespace checkers
{
    // Todo: Order all relevant enums
    // Todo: Style all the code
    // Todo: check if there are multiple return statements in the same method and remove it
    // Todo: Update all methods/variables privacy level
    public enum eBoardSize
    {
        small = 6,
        medium = 8,
        large = 10
    }

    public enum eGameStatus
    {
        playing,
        win,
        draw,
        forfit
    }

    public enum ePlayerType
    {
        Human,
        Computer
    }

    public enum eMoveType
    {
        regular,
        jump
    }

    public enum eMoveStatus
    {
        Legal,
        Illegal,
        AnotherJumpPossible
    } // syntax error should be checked in the UI part

    public enum eListOfMessages
    {
    } // all possible ui messages 

    public enum eSquareStatus
    {
        empty,
        outOfBounds,
        occupied
    }

    public class GameManager
    {
        private const int MAX_NAME_SIZE = 20;
        Board m_Board;
        private int m_BoardSize;
        Player m_CurrentPlayer;
        Player m_Player1;
        Player m_Player2;

        private CheckersConsolUI m_Ui = new CheckersConsolUI();

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
            m_Player1.Name = m_Ui.GetUserNameInput(MAX_NAME_SIZE);

            // Board size
            m_BoardSize = m_Ui.GetUserBoardSize(Board.ALLOWED_BOARD_SIZES);

            // Choose human/computer opponent, if human, enter name
            m_Player2.PlayerType = m_Ui.GetPlayerType();
            m_Player2.Name = m_Player2.PlayerType == ePlayerType.Human
                                 ? m_Ui.GetUserNameInput(MAX_NAME_SIZE)
                                 : "Computer";

            // Call playSingleGame
            bool continuePlaying = true;

            while (continuePlaying)
            {
                playSingleGame();
                continuePlaying = m_Ui.GetUserAnotherGameInput();
            }

            Console.Out.WriteLine("Thank you for playing, hope you enjoyed!");
            Console.In.ReadLine();
        }

        private void playSingleGame()
        {
            // This method allows the user to play a single game

            // Initialize a new board and print it
            m_Ui.ClearScreen();
            m_Board = new Board(m_BoardSize);
            eGameStatus gameStatus = eGameStatus.playing;
            ePlayerPosition winner = ePlayerPosition.BottomPlayer;
            m_CurrentPlayer = m_Player1;
            Move previousMove = null;

            m_Ui.PrintScoreBoard(
                m_Player1.Name,
                m_Board.GetPlayerScore(m_Player1),
                m_Player2.Name,
                m_Board.GetPlayerScore(m_Player2));
                m_Ui.PrintBoard(m_Board.GetBoard());

            while (gameStatus == eGameStatus.playing)
            {
                // Get a players move and preform it
                Move currentMove = GetMove(previousMove, out eMoveStatus currentMoveStatus);
                m_CurrentPlayer.AddMove(currentMove);

                m_Ui.ClearScreen();
                m_Ui.PrintScoreBoard(
                    m_Player1.Name,
                    m_Board.GetPlayerScore(m_Player1),
                    m_Player2.Name,
                    m_Board.GetPlayerScore(m_Player2));
                    m_Ui.PrintBoard(m_Board.GetBoard());
                    m_Ui.PrintLastMove(m_CurrentPlayer);

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
            int player1points = m_Board.GetPlayerScore(m_Player1);
            int player2points = m_Board.GetPlayerScore(m_Player2);

            if (i_GameStatus == eGameStatus.draw)
            {
                Console.Out.WriteLine("Game ended in a draw");
            }
            else if (i_GameStatus == eGameStatus.win)
            {
                Console.Out.WriteLine(
                    "{0} has won!",
                    (m_Player1.PlayerPosition == i_Winner) ? m_Player1.Name : m_Player2.Name);
            }
            else if (i_GameStatus == eGameStatus.forfit)
            {
                Console.Out.WriteLine(
                    "{0} has forfited the game :(",
                    (m_Player1.PlayerPosition == i_Winner) ? m_Player2.Name : m_Player1.Name);
            }

            Console.Out.WriteLine("{0} recieved {1} points", m_Player1.Name, player1points);
            Console.Out.WriteLine("{0} recieved {1} points", m_Player2.Name, player2points);

            m_Player1.Points += player1points;
            m_Player2.Points += player2points;

            Console.Out.WriteLine("Total number of points:");
            Console.Out.WriteLine("{0} has {1} points", m_Player1.Name, m_Player1.Points);
            Console.Out.WriteLine("{0} has {1} points", m_Player2.Name, m_Player2.Points);
        }

        private Move GetMove(Move i_PreviousMove, out eMoveStatus o_MoveStatus)
        {
            Move currentMove = null;
            eMoveStatus currentMoveStatus = eMoveStatus.Illegal;
            if (m_CurrentPlayer.PlayerType == ePlayerType.Human)
            {
                while (currentMoveStatus == eMoveStatus.Illegal)
                {
                    currentMove = m_Ui.GetUserMoveInput(m_CurrentPlayer, out bool forfitFlag);
                    if (forfitFlag)
                    {
                        m_Board.PlayerForfit(m_CurrentPlayer, out currentMoveStatus);
                        if (currentMoveStatus == eMoveStatus.Illegal)
                        {
                            m_Ui.PrintMessage(
                                new StringBuilder("You're not allowed to forfit when you're on the lead"));
                        }
                    }
                    else
                    {
                        m_Board.MovePiece(ref currentMove, i_PreviousMove, out currentMoveStatus);
                        if (currentMoveStatus == eMoveStatus.Illegal)
                        {
                            m_Ui.PrintMessage(new StringBuilder("Invalid move, enter a new one:"));
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