using System;

namespace checkers
{
    public class Move
    {
        private readonly Position r_Begin;
        private readonly Position r_End;
        private ePlayerPosition? m_Player;
        private eMoveType? m_Type;

        public Move(Position i_Begin, Position i_End, ePlayerPosition i_Player)
        {
            // New move constructor
            r_Begin = i_Begin;
            r_End = i_End;
            m_Player = i_Player;
            m_Type = null;
        }

        public Move(Position i_Begin, Position i_End, ePlayerPosition i_Player, eMoveType i_MoveType)
        {
            // New move constructor
            r_Begin = i_Begin;
            r_End = i_End;
            m_Player = i_Player;
            m_Type = i_MoveType;
        }

        public Move(Position i_Begin, Position i_End)
        {
            r_Begin = i_Begin;
            r_End = i_End;
            m_Player = null;
            m_Type = eMoveType.Regular;
        }

        public Position Begin
        {
            get
            {
                return r_Begin;
            }
        }

        public Position End
        {
            get
            {
                return r_End;
            }
        }

        public ePlayerPosition Player
        {
            get
            {
                return m_Player != null ? m_Player.Value : throw new Exception();
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
                return m_Type != null ? m_Type.Value : throw new Exception();
            }

            set
            {
                m_Type = value;
            }
        }

        public override string ToString()
        {
            return string.Format(Strings.MoveFormat, r_Begin, r_End);
        }
    }
}