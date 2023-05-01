namespace TabuleiroNM
{
    abstract class Piece
    {
        public BoardPosition? Position { get; set; }
        public PieceColor Color { get; protected set; }
        public int NumbersOfMoves { get; protected set; }
        public MatchBoard Board { get; protected set; }

        public Piece(PieceColor color, MatchBoard board)
        {
            Color = color;
            NumbersOfMoves = 0;
            Board = board;
        }

        public void IncrementNumberOfMoves() => NumbersOfMoves++;

        public void DecrementNumberOfMoves() => NumbersOfMoves--;

        public bool CheckForMoves()
        {
            var moves = GetMoves();
            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (moves[i,j])
                        return true;
                }
            }
            return false;
        }

        public bool IsPossibleMove(BoardPosition positions) => GetMoves()[positions.Line, positions.Column];

        public abstract bool[,] GetMoves();
    }
}
