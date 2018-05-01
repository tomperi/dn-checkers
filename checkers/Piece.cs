namespace checkers
{
    public enum ePlayer { TopPlayer, BottomPlayer }
    public enum ePieceType { regular, king }
    public enum ePieceSymbol { player1regular, player1king, player2regular, player2king }

    public class Piece
    {
        private ePlayer m_PlayerNumber;
        private ePieceType m_Type;

        public Piece(ePlayer i_PlayerNumber)
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

        public ePlayer PlayerNumber
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
                if (m_Type == ePieceType.regular && m_PlayerNumber == ePlayer.TopPlayer) return ePieceSymbol.player1regular;
                if (m_Type == ePieceType.king && m_PlayerNumber == ePlayer.TopPlayer) return ePieceSymbol.player1king;
                if (m_Type == ePieceType.regular && m_PlayerNumber == ePlayer.BottomPlayer) return ePieceSymbol.player2regular;
                if (m_Type == ePieceType.king && m_PlayerNumber == ePlayer.BottomPlayer) return ePieceSymbol.player2king;
                return ePieceSymbol.player1regular;
            }
        }
    }
}