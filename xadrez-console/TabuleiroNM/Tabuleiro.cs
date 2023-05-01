namespace TabuleiroNM
{
    class MatchBoard
    {
        public int Lines { get; }
        public int Columns { get; }

        private readonly Piece?[,] _pieces;

        public MatchBoard(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;

            _pieces = new Piece[lines, columns];
        }

        public Piece? GetPiece(int line, int column) => _pieces[line, column];

        public Piece? GetPiece(BoardPosition positions) => _pieces[positions.Line, positions.Column];

        public bool IsPiece(BoardPosition position) => IsValidPiecePosition(position) && GetPiece(position) is not null;

        public void PutPiece(Piece piece, BoardPosition position)
        {
            if (IsPiece(position))
                throw new BoardException("There is already a piece in this position!");

            _pieces[position.Line, position.Column] = piece;
            piece.Position = position;
        }

        public Piece? TakePiece(BoardPosition position)
        {
            if (GetPiece(position) is null)
                return null;

            var piece = GetPiece(position) ?? throw new NullReferenceException();
            piece.Position = null;
            _pieces[position.Line, position.Column] = null;

            return piece;
        }

        public bool IsOnBoard(BoardPosition position) => position.Line >= 0 && position.Line < Lines && position.Column >= 0 && position.Column < Columns;

        public bool IsValidPiecePosition(BoardPosition position) => IsOnBoard(position) ? true : throw new BoardException("Invalid position!");
    }
}