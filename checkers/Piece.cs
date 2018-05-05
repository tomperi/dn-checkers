namespace checkers
{
    public enum ePlayerPosition
    {
        TopPlayer,
        BottomPlayer
    }

    public enum ePieceType
    {
        Regular,
        King
    }

    public enum ePieceSymbol
    {
        Player1Regular,
        Player1King,
        Player2Regular,
        Player2King
    }

    public class Piece
    {
        private readonly ePlayerPosition r_PlayerPosition;
        private ePieceType m_Type;

        public Piece(ePlayerPosition i_PlayerPosition)
        {
            r_PlayerPosition = i_PlayerPosition;
            m_Type = ePieceType.Regular;
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
                return r_PlayerPosition;
            }
        }

        public ePieceSymbol PieceSymbol
        {       
            get
            {
                ePieceSymbol pieceSymbol;

                if (m_Type == ePieceType.Regular && r_PlayerPosition == ePlayerPosition.TopPlayer)
                {
                    pieceSymbol = ePieceSymbol.Player1Regular;
                }
                else if (m_Type == ePieceType.King && r_PlayerPosition == ePlayerPosition.TopPlayer)
                {
                    pieceSymbol = ePieceSymbol.Player1King;
                }
                else if (m_Type == ePieceType.Regular && r_PlayerPosition == ePlayerPosition.BottomPlayer)
                {
                    pieceSymbol = ePieceSymbol.Player2Regular;
                }
                else if (m_Type == ePieceType.King && r_PlayerPosition == ePlayerPosition.BottomPlayer)
                {
                    pieceSymbol = ePieceSymbol.Player2King;
                }
                else
                {
                    pieceSymbol = ePieceSymbol.Player1Regular;
                }

                return pieceSymbol;
            }
        }

        public void SetKing()
        {
            m_Type = ePieceType.King;
        }
    }
}