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
        private ePlayer m_NumberPlayer;

        public const int MAX_NAME_SIZE = 20;

        public string Name 
        {
            get
            {
                return m_Name;
            }
            set
            {
                if (value.Length <= MAX_NAME_SIZE)
                {
                    m_Name = value;
                }
                else
                {
                    throw new Exception("Name too long");
                }
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

        public ePlayer PlayerNumber
        {
            get
            {
                return m_NumberPlayer;
            }
        }

        protected void ClearMoveHistory()
        {
            // clears the move history, in order to start a new game
        }

        protected void AddMove()
        {
            // add a move to the players move history
        }

        public Move GetLastMove()
        {
            return m_MoveHistory[m_MoveHistory.Count - 1];
        }
    }
}