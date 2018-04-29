using System;
using System.Collections.Generic;
using System.Text;

namespace checkers
{
    // add e_ to all enums
    public enum boardSize { small, medium, large }
    public enum gameStatus { playing, win, draw }
    public enum playerType { human, computer }
    public enum pieceColor { black, white }
    public enum pieceType { regular, king }
    public enum moveType { regular, jump }
    public enum moveStatus { legal, logicError } // syntax error should be checked in the UI part
    public enum listOfMessages { } // all possible ui messages 

    class GameManager
    {
        Board m_Board;
        Player m_Player1;
        Player m_Player2;
        Move?[] m_MoveHistory;
        Player m_CurrentPlayer;

        public GameManager()
        {
            // default constructor
        }

        public void Start()
        {
            // the function that runs the entire game
        }

        private moveStatus checkMoveLegalality(Move i_Move)
        {
            // check if the move is legal
            return moveStatus.legal;
        }

        private Move getRandomMove(Player i_Player)
        {
            // returns a move for a specific player, from all possible moves 
            return new Move();
        }

        private void changeActivePlayer()
        {
            // changes the active player to the other one
        }

    }
}
