namespace checkers
{
    public enum ePlayerPosition
    {
        TopPlayer,
        BottomPlayer
    }

    public enum ePieceType
    {
        regular,
        king
    }

    public enum ePieceSymbol
    {
        player1regular,
        player1king,
        player2regular,
        player2king
    }

    public class Piece
    {
        private ePlayerPosition m_PlayerPosition;
        private ePieceType m_Type;

        public Piece(ePlayerPosition i_PlayerPosition)
        {
            // new piece constructor
            m_PlayerPosition = i_PlayerPosition;
            m_Type = ePieceType.regular;
        }

        public ePieceType Type
        {
            get
            {
                return m_Type;
            }
        }

        public ePlayerPosition PlayerPosition
        {
            get
            {
                return m_PlayerPosition;
            }
        }

        public ePieceSymbol PieceSymbol
        {
            get
            {
                if (m_Type == ePieceType.regular && m_PlayerPosition == ePlayerPosition.TopPlayer)
                {
                    return ePieceSymbol.player1regular;
                }

                if (m_Type == ePieceType.king && m_PlayerPosition == ePlayerPosition.TopPlayer)
                {
                    return ePieceSymbol.player1king;
                }

                if (m_Type == ePieceType.regular && m_PlayerPosition == ePlayerPosition.BottomPlayer)
                {
                    return ePieceSymbol.player2regular;
                }

                if (m_Type == ePieceType.king && m_PlayerPosition == ePlayerPosition.BottomPlayer)
                {
                    return ePieceSymbol.player2king;
                }

                return ePieceSymbol.player1regular;
            }
        }

        public void SetKing()
        {
            m_Type = ePieceType.king;
        }
    }
}