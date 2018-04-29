namespace checkers
{
    struct Piece
    {
        private pieceColor m_Color;
        private pieceType m_Type;

        Piece(pieceColor i_Color, pieceType i_Type)
        {            
            // new piece constructor
            m_Color = i_Color;
            m_Type = i_Type;
        }

        public void SetKing()
        {
            // change piece type from regular to king
        }
    }
}