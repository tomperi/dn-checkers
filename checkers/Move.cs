using System;

namespace checkers
{
    public struct Move
    {
        // TODO: Change move from struct to class
        private Position m_Begin;
        private Position m_End;
        private ePlayer? m_Player;
        private eMoveType? m_Type;

        public Move(Position i_Begin, Position i_End, ePlayer i_Player)
        {
            // New move constructor
            m_Begin = i_Begin;
            m_End = i_End;
            m_Player = i_Player;
            m_Type = null;
        }

        public Move(Position i_Begin, Position i_End, ePlayer i_Player, eMoveType i_MoveType)
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
            m_Type = null;
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

        public ePlayer player
        {
            get
            {
                if (m_Player != null)
                {
                    return m_Player.Value;
                }
                else
                {
                    throw new Exception("No value for you");
                }
            }
        }

        public eMoveType Type
        {
            get
            {
                if (m_Type != null)
                {
                    return m_Type.Value;
                }
                else
                {
                    throw new Exception("No value for you");
                }
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