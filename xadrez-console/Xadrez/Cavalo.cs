using TabuleiroNM;

namespace Xadrez
{
    class Knight : Piece
    {
        public Knight(PieceColor color, MatchBoard board) : base(color, board) { }

        public override string ToString() => "N";

        private bool CanMove(BoardPosition pos)
        {
            var piece = Board.GetPiece(pos);
            return piece is null || piece.Color != Color;
        }

        public override bool[,] GetMoves()
        {
            var moves = new bool[Board.Lines, Board.Columns];
            var position = new BoardPosition(0, 0);

            if (Position is null)
                throw new NullReferenceException();

            position.SetValues(Position.Line - 1, Position.Column - 2);
            if (Board.IsOnBoard(position) && CanMove(position))
                moves[position.Line, position.Column] = true;

            position.SetValues(Position.Line - 2, Position.Column - 1);
            if (Board.IsOnBoard(position) && CanMove(position))
                moves[position.Line, position.Column] = true;

            position.SetValues(Position.Line - 2, Position.Column + 1);
            if (Board.IsOnBoard(position) && CanMove(position))
                moves[position.Line, position.Column] = true;

            position.SetValues(Position.Line - 1, Position.Column + 2);
            if (Board.IsOnBoard(position) && CanMove(position))
                moves[position.Line, position.Column] = true;

            position.SetValues(Position.Line + 1, Position.Column + 2);
            if (Board.IsOnBoard(position) && CanMove(position))
                moves[position.Line, position.Column] = true;

            position.SetValues(Position.Line + 2, Position.Column + 1);
            if (Board.IsOnBoard(position) && CanMove(position))
                moves[position.Line, position.Column] = true;

            position.SetValues(Position.Line + 2, Position.Column - 1);
            if (Board.IsOnBoard(position) && CanMove(position))
                moves[position.Line, position.Column] = true;

            position.SetValues(Position.Line + 1, Position.Column - 2);
            if (Board.IsOnBoard(position) && CanMove(position))
                moves[position.Line, position.Column] = true;

            return moves;
        }
    }
}
