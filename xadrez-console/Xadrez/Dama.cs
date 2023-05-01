﻿using TabuleiroNM;

namespace Xadrez
{
    class Queen : Piece
    {
        public Queen(PieceColor color, MatchBoard board) : base(color, board) { }

        public override string ToString() => "Q";

        private bool CanMove(BoardPosition position)
        {
            var piece = Board.GetPiece(position);
            return piece is null || piece.Color != Color;
        }

        public override bool[,] GetMoves()
        {
            var moves = new bool[Board.Lines, Board.Columns];
            var position = new BoardPosition(0, 0);

            if (Position is null)
                throw new NullReferenceException();

            // up
            position.SetValues(Position.Line - 1, Position.Column);
            while (Board.IsOnBoard(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
                if (Board.GetPiece(position) != null && Board.GetPiece(position)?.Color != Color)
                    break;
                position.Line--;
            }

            // down
            position.SetValues(Position.Line + 1, Position.Column);
            while (Board.IsOnBoard(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
                if (Board.GetPiece(position) != null && Board.GetPiece(position)?.Color != Color)
                    break;
                position.Line++;
            }

            // right
            position.SetValues(Position.Line, Position.Column + 1);
            while (Board.IsOnBoard(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
                if (Board.GetPiece(position) != null && Board.GetPiece(position)?.Color != Color)
                    break;
                position.Column++;
            }

            // left
            position.SetValues(Position.Line, Position.Column - 1);
            while (Board.IsOnBoard(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
                if (Board.GetPiece(position) != null && Board.GetPiece(position)?.Color != Color)
                    break;
                position.Column--;
            }

            // northwest
            position.SetValues(Position.Line - 1, Position.Column - 1);
            while (Board.IsOnBoard(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
                if (Board.GetPiece(position) != null && Board.GetPiece(position)?.Color != Color)
                    break;
                position.SetValues(position.Line - 1, position.Column - 1);
            }

            // northeast
            position.SetValues(Position.Line - 1, Position.Column + 1);
            while (Board.IsOnBoard(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
                if (Board.GetPiece(position) != null && Board.GetPiece(position)?.Color != Color)
                    break;
                position.SetValues(position.Line - 1, position.Column + 1);
            }

            // southeast
            position.SetValues(Position.Line + 1, Position.Column + 1);
            while (Board.IsOnBoard(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
                if (Board.GetPiece(position) != null && Board.GetPiece(position)?.Color != Color)
                    break;
                position.SetValues(position.Line + 1, position.Column + 1);
            }

            // southwest
            position.SetValues(Position.Line + 1, Position.Column - 1);
            while (Board.IsOnBoard(position) && CanMove(position))
            {
                moves[position.Line, position.Column] = true;
                if (Board.GetPiece(position) != null && Board.GetPiece(position)?.Color != Color)
                    break;
                position.SetValues(position.Line + 1, position.Column - 1);
            }

            return moves;
        }
    }
}
