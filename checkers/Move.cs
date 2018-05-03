using System;

namespace checkers
{
    public class Move
    {
        private Position m_Begin;
        private Position m_End;
        private ePlayerPosition? m_Player;
        private eMoveType? m_Type;

        public Move(Position i_Begin, Position i_End, ePlayerPosition i_Player)
        {
            // New move constructor
            m_Begin = i_Begin;
            m_End = i_End;
            m_Player = i_Player;
            m_Type = null;
        }

        public Move(Position i_Begin, Position i_End, ePlayerPosition i_Player, eMoveType i_MoveType)
        {
            // New move constructor
            m_Begin = i_Begin;
            m_End = i_End;
            m_Player = i_Player;
            m_Type = i_MoveType;
        }

        public Move(Position i_Begin, Position i_End)
        {
            m_Begin = i_Begin;
            m_End = i_End;
            m_Player = null;
            m_Type = eMoveType.regular;
        }

        public Position Begin
        {
            get
            {
                return m_Begin; 
            }
        }

        public Position End
        {
            get
            {
                return m_End;
            }
        }

        public ePlayerPosition Player
        {
            get
            {
                return m_Player.Value;
            }
            set
            {
                m_Player = value;

            }
        }

        public eMoveType Type
        {
            get
            {
                return m_Type.Value;
            }
            set
            {
                m_Type = value;
            }
        }

        public override string ToString()
        {
            return m_Begin.ToString() + ">" + m_End.ToString();
        }
    }
}