namespace checkers
{
    struct Move
    {
        private Position m_Begin;
        private Position m_End;
        private Player m_Player;
        private moveType m_Type;

        Move(Position i_Begin, Position i_End, Player i_Player)
        {
            m_Begin = i_Begin;
            m_End = i_End;
            m_Player = i_Player;
            m_Type = moveType.regular;
            // new move constructor 
        }

        // get begin 
        // get end
        // get player
        // get/set move type
    }
}