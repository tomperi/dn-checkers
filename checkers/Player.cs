using System.Collections.Generic;

namespace checkers
{
    public class Player
    {
        private readonly ePlayerPosition r_PlayerPosition;
        private List<Move> m_MoveHistory;
        private string m_Name;
        private ePlayerType m_PlayerType;
        private int m_Points;

        public Player(ePlayerPosition i_PlayerPosition)
        {
            r_PlayerPosition = i_PlayerPosition;
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
                return r_PlayerPosition;
            }
        }

        public void ClearMoveHistory()
        {
            m_MoveHistory = new List<Move>();
        }

        public void AddMove(Move i_Move)
        {
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