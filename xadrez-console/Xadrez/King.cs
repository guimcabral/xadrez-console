using TabuleiroNM;

namespace Xadrez
{
    class King : Piece
    {
        private readonly ChessMatch _match;

        public King(PieceColor color, MatchBoard board, ChessMatch match) : base(color, board) => _match = match;

        public override string ToString() => "K";

        private bool CanMove(BoardPosition position)
        {
            var piece = Board.GetPiece(position);
            return piece is null || piece.Color != Color;
        }

        private bool RookCastlingTest(BoardPosition position)
        {
            var piece = Board.GetPiece(position);
            return piece is not null && piece is Rook && piece.Color == Color && piece.NumbersOfMoves == 0;
        }

        public override bool[,] GetMoves()
        {
            var moves = new bool[Board.Lines, Board.Columns];
            var positions = new BoardPosition(0, 0);

            if (Position is null)
                throw new NullReferenceException();

            // north
            positions.SetValues(Position.Line - 1, Position.Column);
            if (Board.IsOnBoard(positions) && CanMove(positions))
                moves[positions.Line, positions.Column] = true;

            // northeast
            positions.SetValues(Position.Line - 1, Position.Column + 1);
            if (Board.IsOnBoard(positions) && CanMove(positions))
                moves[positions.Line, positions.Column] = true;

            // east
            positions.SetValues(Position.Line, Position.Column + 1);
            if (Board.IsOnBoard(positions) && CanMove(positions))
                moves[positions.Line, positions.Column] = true;

            // southeast
            positions.SetValues(Position.Line + 1, Position.Column + 1);
            if (Board.IsOnBoard(positions) && CanMove(positions))
                moves[positions.Line, positions.Column] = true;

            // south
            positions.SetValues(Position.Line + 1, Position.Column);
            if (Board.IsOnBoard(positions) && CanMove(positions))
                moves[positions.Line, positions.Column] = true;

            // southwest
            positions.SetValues(Position.Line + 1, Position.Column - 1);
            if (Board.IsOnBoard(positions) && CanMove(positions))
                moves[positions.Line, positions.Column] = true;

            // west
            positions.SetValues(Position.Line, Position.Column - 1);
            if (Board.IsOnBoard(positions) && CanMove(positions))
                moves[positions.Line, positions.Column] = true;

            // northwest
            positions.SetValues(Position.Line - 1, Position.Column - 1);
            if (Board.IsOnBoard(positions) && CanMove(positions))
                moves[positions.Line, positions.Column] = true;

            // castling
            if (NumbersOfMoves == 0 && !_match.Check)
            {
                // kingside castling
                var posT1 = new BoardPosition(Position.Line, Position.Column + 3);
                if (RookCastlingTest(posT1))
                {
                    var p1 = new BoardPosition(Position.Line, Position.Column + 1);
                    var p2 = new BoardPosition(Position.Line, Position.Column + 2);
                    if (Board.GetPiece(p1) == null && Board.GetPiece(p2) == null)
                        moves[Position.Line, Position.Column + 2] = true;
                }

                // queenside castling
                var posT2 = new BoardPosition(Position.Line, Position.Column - 4);
                if (RookCastlingTest(posT2))
                {
                    var p1 = new BoardPosition(Position.Line, Position.Column - 1);
                    var p2 = new BoardPosition(Position.Line, Position.Column - 2);
                    var p3 = new BoardPosition(Position.Line, Position.Column - 3);
                    if (Board.GetPiece(p1) == null && Board.GetPiece(p2) == null && Board.GetPiece(p3) == null)
                        moves[Position.Line, Position.Column - 2] = true;
                }
            }

            return moves;
        }
    }
}
