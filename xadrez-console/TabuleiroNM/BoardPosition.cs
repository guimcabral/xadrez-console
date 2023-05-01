namespace TabuleiroNM
{
    class BoardPosition
    {
        public int Line { get; set; }
        public int Column { get; set; }
        
        public BoardPosition(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public void SetValues(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public override string ToString() => $"{Line}, {Column}";
    }
}
