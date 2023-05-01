using TabuleiroNM;

namespace Xadrez
{
    class Rook : Piece
    {
        public Rook(PieceColor color, MatchBoard board) : base(color, board) { }

        public override string ToString() => "R";

        private bool CanMove(BoardPosition position)
        {
            var piece = Board.GetPiece(position);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] GetMoves()
        {
            var moves = new bool[Board.Lines, Board.Columns];
            var positions = new BoardPosition(0, 0);

            if (Position is null)
                throw new NullReferenceException();

            // up
            positions.SetValues(Position.Line - 1, Position.Column);
            while (Board.IsOnBoard(positions) && CanMove(positions))
            {
                moves[positions.Line, positions.Column] = true;
                if (Board.GetPiece(positions) != null && Board.GetPiece(positions)?.Color != Color)
                    break;
                positions.Line--;
            }

            // down
            positions.SetValues(Position.Line + 1, Position.Column);
            while (Board.IsOnBoard(positions) && CanMove(positions))
            {
                moves[positions.Line, positions.Column] = true;
                if (Board.GetPiece(positions) != null && Board.GetPiece(positions)?.Color != Color)
                    break;
                positions.Line++;
            }

            // right
            positions.SetValues(Position.Line, Position.Column + 1);
            while (Board.IsOnBoard(positions) && CanMove(positions))
            {
                moves[positions.Line, positions.Column] = true;
                if (Board.GetPiece(positions) != null && Board.GetPiece(positions)?.Color != Color)
                    break;
                positions.Column++;
            }

            // left
            positions.SetValues(Position.Line, Position.Column - 1);
            while (Board.IsOnBoard(positions) && CanMove(positions))
            {
                moves[positions.Line, positions.Column] = true;
                if (Board.GetPiece(positions) != null && Board.GetPiece(positions)?.Color != Color)
                    break;
                positions.Column--;
            }

            return moves;
        }
    }
}
