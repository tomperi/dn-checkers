namespace checkers
{
    struct Move
    {
        private Position m_Begin;
        private Position m_End;
        private Player m_Player;
        private eMoveType m_Type;

        public Move(Position i_Begin, Position i_End, Player i_Player)
        {
            m_Begin = i_Begin;
            m_End = i_End;
            m_Player = i_Player;
            m_Type = eMoveType.regular;
            // new move constructor 
        }

        // get begin 
        // get end
        // get player
        // get/set move type
    }
}