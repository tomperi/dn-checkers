namespace checkers
{
    public struct Move
    {
        private Position m_Begin;
        private Position m_End;
        private ePlayer m_Player;
        private eMoveType m_Type;

        public Move(Position i_Begin, Position i_End, ePlayer i_Player)
        {
            // New move constructor
            m_Begin = i_Begin;
            m_End = i_End;
            m_Player = i_Player;
            m_Type = eMoveType.regular;
        }

        public Move(Position i_Begin, Position i_End, ePlayer i_Player, eMoveType i_MoveType)
        {
            // New move constructor
            m_Begin = i_Begin;
            m_End = i_End;
            m_Player = i_Player;
            m_Type = i_MoveType;
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
                return m_Player;
            }
        }

        public eMoveType Type
        {
            get
            {
                return m_Type;
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