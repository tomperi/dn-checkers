using System;
using System.Collections.Generic;

namespace checkers
{
    public class Player
    {
        private string m_Name;
        private ePlayerType m_PlayerType;
        private List<Move> m_MoveHistory;
        private int m_Points = 0;
        private ePlayerNumber m_PlayerNumber;
        
        public Player(ePlayerNumber i_PlayerNumber)
        {
            m_PlayerNumber = i_PlayerNumber;
            m_MoveHistory = new List<Move>();
        }

        public string Name 
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        public int Points 
        {
            get
            {
                return m_Points; 
            }

            set
            {
                m_Points = value;
            }
        }

        public ePlayerType PlayerType
        {
            get 
            {
                return m_PlayerType;
            }
            set
            {
                m_PlayerType = value;
            }
        }

        public ePlayerNumber PlayerNumber
        {
            get
            {
                return m_PlayerNumber;
            }
        }

        public void ClearMoveHistory()
        {
            // clears the move history, in order to start a new game
            m_MoveHistory = new List<Move>();
        }

        protected void AddMove(Move i_Move)
        {
            // Add a move to the players move history
            m_MoveHistory.Add(i_Move);
        }

        public Move GetLastMove()
        {
            return m_MoveHistory[m_MoveHistory.Count - 1];
        }
    }
}