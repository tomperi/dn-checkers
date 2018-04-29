namespace checkers
{
    public class Player
    {
        private string m_Name;
        private playerType m_Type;
        private pieceColor m_Color;
        private Move?[] m_MoveHistory;
        private int m_Points = 0;

        protected string Name 
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value; 
            }
        }

        protected int Points 
        {
            get
            {
                return m_Points; 
            }

            set
            {
                m_Points = value;
            }
        }

        protected playerType Type
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

        protected pieceColor Color
        {
            get
            {
                return m_Color;
            }
            set
            {
                m_Color = value;
            }

        }

        protected void ClearMoveHistory()
        {
            // clears the move history, in order to start a new game
        }

        protected void AddMove()
        {
            // add a move to the players move history
        }
    }
}