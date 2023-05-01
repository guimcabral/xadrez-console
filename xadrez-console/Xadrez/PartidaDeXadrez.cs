using System;
using TabuleiroNM;

namespace Xadrez
{
    class ChessMatch
    {
        public MatchBoard Board { get; } = new MatchBoard(8, 8);
        public int Turn { get; private set; } = 1;
        public PieceColor Player { get; private set; } = PieceColor.White;
        public bool Finished { get; private set; }
        public bool Check { get; private set; }
        public Piece? EnPassantVulnerablePiece { get; private set; }

        private readonly HashSet<Piece> _pieces = new();
        private readonly HashSet<Piece> _captured = new();

        public ChessMatch() => PutPieces();

        private void PutPieces()
        {
            PutNewPiece('a', 1, new Rook(PieceColor.White, Board));
            PutNewPiece('b', 1, new Knight(PieceColor.White, Board));
            PutNewPiece('c', 1, new Bishop(PieceColor.White, Board));
            PutNewPiece('d', 1, new Queen(PieceColor.White, Board));
            PutNewPiece('e', 1, new King(PieceColor.White, Board, this));
            PutNewPiece('f', 1, new Bishop(PieceColor.White, Board));
            PutNewPiece('g', 1, new Knight(PieceColor.White, Board));
            PutNewPiece('h', 1, new Rook(PieceColor.White, Board));
            PutNewPiece('a', 2, new Pawn(PieceColor.White, Board, this));
            PutNewPiece('b', 2, new Pawn(PieceColor.White, Board, this));
            PutNewPiece('c', 2, new Pawn(PieceColor.White, Board, this));
            PutNewPiece('d', 2, new Pawn(PieceColor.White, Board, this));
            PutNewPiece('e', 2, new Pawn(PieceColor.White, Board, this));
            PutNewPiece('f', 2, new Pawn(PieceColor.White, Board, this));
            PutNewPiece('g', 2, new Pawn(PieceColor.White, Board, this));
            PutNewPiece('h', 2, new Pawn(PieceColor.White, Board, this));

            PutNewPiece('a', 8, new Rook(PieceColor.Black, Board));
            PutNewPiece('b', 8, new Knight(PieceColor.Black, Board));
            PutNewPiece('c', 8, new Bishop(PieceColor.Black, Board));
            PutNewPiece('d', 8, new Queen(PieceColor.Black, Board));
            PutNewPiece('e', 8, new King(PieceColor.Black, Board, this));
            PutNewPiece('f', 8, new Bishop(PieceColor.Black, Board));
            PutNewPiece('g', 8, new Knight(PieceColor.Black, Board));
            PutNewPiece('h', 8, new Rook(PieceColor.Black, Board));
            PutNewPiece('a', 7, new Pawn(PieceColor.Black, Board, this));
            PutNewPiece('b', 7, new Pawn(PieceColor.Black, Board, this));
            PutNewPiece('c', 7, new Pawn(PieceColor.Black, Board, this));
            PutNewPiece('d', 7, new Pawn(PieceColor.Black, Board, this));
            PutNewPiece('e', 7, new Pawn(PieceColor.Black, Board, this));
            PutNewPiece('f', 7, new Pawn(PieceColor.Black, Board, this));
            PutNewPiece('g', 7, new Pawn(PieceColor.Black, Board, this));
            PutNewPiece('h', 7, new Pawn(PieceColor.Black, Board, this));
        }

        public HashSet<Piece> CapturedPieces(PieceColor color)
        {
            var aux = new HashSet<Piece>();
            foreach (Piece piece in _captured)
            {
                if (piece.Color == color)
                    aux.Add(piece);
            }
            return aux;
        }

        public HashSet<Piece> PiecesOnBoard(PieceColor color)
        {
            var aux = new HashSet<Piece>();
            foreach (Piece piece in _pieces)
            {
                if (piece.Color == color)
                    aux.Add(piece);
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        private static PieceColor Opponent(PieceColor color)
        {
            if (color == PieceColor.White)
                return PieceColor.Black;
            else
                return PieceColor.White;
        }

        private Piece? King(PieceColor color)
        {
            foreach (Piece piece in PiecesOnBoard(color))
            {
                if (piece is King)
                    return piece;
            }
            return null;
        }

        public bool IsInCheck(PieceColor color)
        {
            var piece = King(color) ?? throw new BoardException("There is no king of " + color + "color on the board!");

            foreach (Piece x in PiecesOnBoard(Opponent(color)))
            {
                var moves = x.GetMoves();
                if (moves[piece.Position.Line, piece.Position.Column])
                    return true;
            }
            return false;
        }

        public bool CheckmateTest(PieceColor color)
        {
            if (!IsInCheck(color))
                return false;

            foreach (Piece piece in PiecesOnBoard(color))
            {
                var moves = piece.GetMoves();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (moves[i, j])
                        {
                            var initialPosition = piece.Position;
                            var finalPosition = new BoardPosition(i, j);
                            var capturedPiece = ExecuteMove(initialPosition, finalPosition);
                            var isInCheck = IsInCheck(color);

                            UndoMove(initialPosition, finalPosition, capturedPiece);

                            if (!isInCheck)
                                return false;
                        }
                    }
                }
            }
            return true;
        }

        public void PutNewPiece(char column, int linha, Piece piece)
        {
            Board.PutPiece(piece, new PosicaoXadrez(column, linha).ToPosition());
            _pieces.Add(piece);
        }

        public Piece ExecuteMove(BoardPosition initialPosition, BoardPosition finalPosition)
        {
            var piece = Board.TakePiece(initialPosition);
            piece?.IncrementNumberOfMoves();

            var capturedPiece = Board.TakePiece(finalPosition);
            Board.PutPiece(piece, finalPosition);

            if (capturedPiece != null)
                _captured.Add(capturedPiece);

            // #jogaespecial roque pequeno
            if (piece is King && finalPosition.Column == initialPosition.Column + 2)
            {
                var origemT = new BoardPosition(initialPosition.Line, initialPosition.Column + 3);
                var destinoT = new BoardPosition(initialPosition.Line, initialPosition.Column + 1);
                var T = Board.TakePiece(origemT);
                T.IncrementNumberOfMoves();
                Board.PutPiece(T, destinoT);
            }

            // #jogaespecial roque grande
            if (piece is King && finalPosition.Column == initialPosition.Column - 2)
            {
                var origemT = new BoardPosition(initialPosition.Line, initialPosition.Column - 4);
                var destinoT = new BoardPosition(initialPosition.Line, initialPosition.Column - 1);
                var T = Board.TakePiece(origemT);
                T.IncrementNumberOfMoves();
                Board.PutPiece(T, destinoT);
            }

            // #jogadaespecial en passant
            if (piece is Pawn){
                if (initialPosition.Column != finalPosition.Column && capturedPiece is null)
                {
                    BoardPosition posP;
                    if (piece.Color == PieceColor.White)
                        posP = new BoardPosition(finalPosition.Line + 1, finalPosition.Column);
                    else
                        posP = new BoardPosition(finalPosition.Line - 1, finalPosition.Column);

                    capturedPiece = Board.TakePiece(posP);
                    _captured.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void UndoMove(BoardPosition initialPosition, BoardPosition finalPosition, Piece capturedPiece)
        {
            Piece piece = Board.TakePiece(finalPosition);
            piece.DecrementNumberOfMoves();

            if (capturedPiece != null)
            {
                Board.PutPiece(capturedPiece, finalPosition);
                _captured.Remove(capturedPiece);
            }
            Board.PutPiece(piece, initialPosition);

            // #jogaespecial roque pequeno
            if (piece is King && finalPosition.Column == initialPosition.Column + 2)
            {
                var origemT = new BoardPosition(initialPosition.Line, initialPosition.Column + 3);
                var destinoT = new BoardPosition(initialPosition.Line, initialPosition.Column + 1);
                var T = Board.TakePiece(destinoT);
                T.DecrementNumberOfMoves();
                Board.PutPiece(T, origemT);
            }

            // #jogaespecial roque grande
            if (piece is King && finalPosition.Column == initialPosition.Column - 2)
            {
                var origemT = new BoardPosition(initialPosition.Line, initialPosition.Column - 4);
                var destinoT = new BoardPosition(initialPosition.Line, initialPosition.Column - 1);
                var T = Board.TakePiece(destinoT);
                T.IncrementNumberOfMoves();
                Board.PutPiece(T, origemT);
            }

            // #jogadaespecial en passant
            if (piece is Pawn)
            {
                if (initialPosition.Column != finalPosition.Column && capturedPiece == EnPassantVulnerablePiece)
                {
                    var pawn = Board.TakePiece(finalPosition);
                    BoardPosition posP;
                    if (piece.Color == PieceColor.White)
                        posP = new BoardPosition(3, finalPosition.Column);
                    else
                        posP = new BoardPosition(4, finalPosition.Column);

                    Board.PutPiece(pawn, posP);
                }
            }
        }

        public void ExecutePlay(BoardPosition initialPosition, BoardPosition finalPosition)
        {
            var capturedPiece = ExecuteMove(initialPosition, finalPosition);

            if (IsInCheck(Player))
            {
                UndoMove(initialPosition, finalPosition, capturedPiece);
                throw new BoardException("You can't put yourself in check!");
            }

            var piece = Board.GetPiece(finalPosition);

            // #jogadaespecial promoção
            if (piece is Pawn)
            {
                if ((piece.Color == PieceColor.White && finalPosition.Line == 0) || (piece.Color == PieceColor.Black && finalPosition.Line == 7))
                {
                    piece = Board.TakePiece(finalPosition);
                    _pieces.Remove(piece);

                    var queen = new Queen(piece.Color, piece.Board);
                    Board.PutPiece(queen, finalPosition);
                    _pieces.Add(queen);
                }
            }

            if (IsInCheck(Opponent(Player)))
                Check = true;
            else
                Check = false;

            if (CheckmateTest(Opponent(Player)))
                Finished = true;
            else
            {
                Turn++;
                ChangePlayer();
            }

            // #jogadaespecial en passant
            if (piece is Pawn && (finalPosition.Line == initialPosition.Line - 2 || finalPosition.Line == initialPosition.Line + 2))
                EnPassantVulnerablePiece = piece;
            else
                EnPassantVulnerablePiece = null;
        }

        public void ValidateInitialPosition(BoardPosition pos)
        {
            if (Board.GetPiece(pos) is null)
                throw new BoardException("There is no piece in the chosen position!");
            if (Player != Board.GetPiece(pos).Color)
                throw new BoardException("The chosen piece is not yours!");
            if (!Board.GetPiece(pos).CheckForMoves())
                throw new BoardException("There are no possible moves for the chosen piece!");
        }

        public void ValidateFinalPosition(BoardPosition origem, BoardPosition destino)
        {
            if (!Board.GetPiece(origem).IsPossibleMove(destino))
                throw new BoardException("Invalid final position!");
        }

        private void ChangePlayer()
        {
            if (Player == PieceColor.White)
                Player = PieceColor.Black;
            else
                Player = PieceColor.White;
        }
    }
}
