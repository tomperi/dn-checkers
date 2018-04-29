namespace checkers
{
    struct Piece
    {
        private ePiecePlayer m_Player;
        private ePieceType m_Type;

        public Piece(ePiecePlayer i_Player)
        {            
            // new piece constructor
            m_Player = i_Player;
            m_Type = ePieceType.regular;
        }

        public void SetKing()
        {
            // change piece type from regular to king
        }
    }
}