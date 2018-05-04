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
                ePieceSymbol pieceSymbol;

                if (m_Type == ePieceType.regular && m_PlayerPosition == ePlayerPosition.TopPlayer)
                {
                    pieceSymbol = ePieceSymbol.player1regular;
                }
                else if (m_Type == ePieceType.king && m_PlayerPosition == ePlayerPosition.TopPlayer)
                {
                    pieceSymbol = ePieceSymbol.player1king;
                }
                else if (m_Type == ePieceType.regular && m_PlayerPosition == ePlayerPosition.BottomPlayer)
                {
                    pieceSymbol = ePieceSymbol.player2regular;
                }
                else if (m_Type == ePieceType.king && m_PlayerPosition == ePlayerPosition.BottomPlayer)
                {
                    pieceSymbol = ePieceSymbol.player2king;
                }
                else
                {
                    pieceSymbol = ePieceSymbol.player1regular;
                }

                return pieceSymbol;
            }
        }

        public void SetKing()
        {
            m_Type = ePieceType.king;
        }
    }
}