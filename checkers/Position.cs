namespace checkers
{
    public struct Position
    {
        private int m_Row;
        private int m_Col;

        public Position (int i_Row, int i_Col)
        {
            // New position constructor
            m_Row = i_Row;
            m_Col = i_Col;
        }

        public int Row
        {
            get
            {
                return m_Row;
            }
        }

        public int Col
        {
            get
            {
                return m_Col;
            }
        }

        public override string ToString()
        {
            return "(" + m_Row + "," + m_Col + ")";
        }
    }
}