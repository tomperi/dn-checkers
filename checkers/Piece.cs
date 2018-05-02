namespace checkers
{
    public enum ePlayerNumber { TopPlayer, BottomPlayer }
    public enum ePieceType { regular, king }
    public enum ePieceSymbol { player1regular, player1king, player2regular, player2king }

    public class Piece
    {
        private ePlayerNumber m_PlayerNumber;
        private ePieceType m_Type;

        public Piece(ePlayerNumber i_PlayerNumber)
        {            
            // new piece constructor
            m_PlayerNumber = i_PlayerNumber;
            m_Type = ePieceType.regular;
        }

        public ePieceType Type
        {
            get
            {
                return m_Type;
            }
        }

        public ePlayerNumber PlayerNumber
        {
            get
            {
                return m_PlayerNumber;
            }
        }

        public void SetKing()
        {
            m_Type = ePieceType.king;
        }

        public ePieceSymbol PieceSymbol
        {
            get
            {
                if (m_Type == ePieceType.regular && m_PlayerNumber == ePlayerNumber.TopPlayer) return ePieceSymbol.player1regular;
                if (m_Type == ePieceType.king && m_PlayerNumber == ePlayerNumber.TopPlayer) return ePieceSymbol.player1king;
                if (m_Type == ePieceType.regular && m_PlayerNumber == ePlayerNumber.BottomPlayer) return ePieceSymbol.player2regular;
                if (m_Type == ePieceType.king && m_PlayerNumber == ePlayerNumber.BottomPlayer) return ePieceSymbol.player2king;
                return ePieceSymbol.player1regular;
            }
        }
    }
}