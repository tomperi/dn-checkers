using System;
using System.Collections.Generic;
using System.Text;

namespace checkers
{
    // add e_ to all enums
    public enum eBoardSize { small = 6, medium = 8, large = 10}
    public enum eGameStatus { playing, win, draw }
    public enum ePlayerType { human, computer }
    public enum ePiecePlayer { player1, player2 }
    public enum ePieceType { regular, king }
    public enum eMoveType { regular, jump }
    public enum eMoveStatus { legal, logicError } // syntax error should be checked in the UI part
    public enum eListOfMessages { } // all possible ui messages 

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

        private void changeActivePlayer()
        {
            // changes the active player to the other one
        }

    }
}
