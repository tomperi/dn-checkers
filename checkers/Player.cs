using System.Collections.Generic;

namespace checkers
{
    public class Player
    {
        private List<Move> m_MoveHistory;
        private string m_Name;
        private ePlayerPosition m_PlayerPosition;
        private ePlayerType m_PlayerType;
        private int m_Points = 0;

        public Player(ePlayerPosition i_PlayerPosition)
        {
            m_PlayerPosition = i_PlayerPosition;
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

        public ePlayerPosition PlayerPosition
        {
            get
            {
                return m_PlayerPosition;
            }
        }

        public void ClearMoveHistory()
        {
            // clears the move history, in order to start a new game
            m_MoveHistory = new List<Move>();
        }

        public void AddMove(Move i_Move)
        {
            // Add a move to the players move history
            m_MoveHistory.Add(i_Move);
        }

        public Move GetLastMove()
        {
            Move lastMove = null;
            if (m_MoveHistory.Count > 0)
            {
                lastMove = m_MoveHistory[m_MoveHistory.Count - 1];
            }

            return lastMove;
        }
    }
}