using TabuleiroNM;

namespace Xadrez
{
    class Pawn : Piece
    {
        private readonly ChessMatch _match;

        public Pawn(PieceColor color, MatchBoard board, ChessMatch match) : base(color, board) => _match = match;

        public override string ToString() => "P";

        private bool CheckForEnemy(BoardPosition position)
        {
            var piece = Board.GetPiece(position);
            return piece is not null && piece.Color != Color;
        }

        private bool IsFree(BoardPosition position) => Board.GetPiece(position) is null;

        public override bool[,] GetMoves()
        {
            var moves = new bool[Board.Lines, Board.Columns];
            var position = new BoardPosition(0, 0);

            if (Position is null)
                throw new NullReferenceException();

            if (Color == PieceColor.White)
            {
                position.SetValues(Position.Line - 1, Position.Column);
                if (Board.IsOnBoard(position) && IsFree(position))
                    moves[position.Line, position.Column] = true;

                position.SetValues(Position.Line - 2, Position.Column);
                if (Board.IsOnBoard(position) && IsFree(position) && NumbersOfMoves == 0)
                    moves[position.Line, position.Column] = true;

                position.SetValues(Position.Line - 1, Position.Column - 1);
                if (Board.IsOnBoard(position) && CheckForEnemy(position))
                    moves[position.Line, position.Column] = true;

                position.SetValues(Position.Line - 1, Position.Column + 1);
                if (Board.IsOnBoard(position) && CheckForEnemy(position))
                    moves[position.Line, position.Column] = true;

                // en passant
                if (Position.Line == 3)
                {
                    var left = new BoardPosition(Position.Line, Position.Column - 1);
                    if (Board.IsOnBoard(left) && CheckForEnemy(left) && Board.GetPiece(left) == _match.EnPassantVulnerablePiece)
                        moves[left.Line - 1, left.Column] = true;

                    var right = new BoardPosition(Position.Line, Position.Column + 1);
                    if (Board.IsOnBoard(right) && CheckForEnemy(right) && Board.GetPiece(right) == _match.EnPassantVulnerablePiece)
                        moves[right.Line - 1, right.Column] = true;
                }
            }
            else
            {
                position.SetValues(Position.Line + 1, Position.Column);
                if (Board.IsOnBoard(position) && IsFree(position))
                    moves[position.Line, position.Column] = true;

                position.SetValues(Position.Line + 2, Position.Column);
                if (Board.IsOnBoard(position) && IsFree(position) && NumbersOfMoves == 0)
                    moves[position.Line, position.Column] = true;

                position.SetValues(Position.Line + 1, Position.Column - 1);
                if (Board.IsOnBoard(position) && CheckForEnemy(position))
                    moves[position.Line, position.Column] = true;

                position.SetValues(Position.Line + 1, Position.Column + 1);
                if (Board.IsOnBoard(position) && CheckForEnemy(position))
                    moves[position.Line, position.Column] = true;

                // en passant
                if (Position.Line == 4)
                {
                    var left = new BoardPosition(Position.Line, Position.Column - 1);
                    if (Board.IsOnBoard(left) && CheckForEnemy(left) && Board.GetPiece(left) == _match.EnPassantVulnerablePiece)
                        moves[left.Line + 1, left.Column] = true;

                    var right = new BoardPosition(Position.Line, Position.Column + 1);
                    if (Board.IsOnBoard(right) && CheckForEnemy(right) && Board.GetPiece(right) == _match.EnPassantVulnerablePiece)
                        moves[right.Line + 1, right.Column] = true;
                }
            }

            return moves;
        }
    }
}
