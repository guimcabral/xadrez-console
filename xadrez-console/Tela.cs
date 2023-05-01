using TabuleiroNM;
using Xadrez;

namespace Xadrez_console
{
    class Screen
    {
        public static void PrintMatch(ChessMatch match)
        {
            PrintBoard(match.Board);
            Console.WriteLine();

            PrintCapturedPieces(match);
            Console.WriteLine();

            Console.WriteLine("Turn: " + match.Turn);

            if (!match.Finished)
            {
                Console.WriteLine("Waiting move: " + match.Player);
                if (match.Check)
                    Console.WriteLine("Check!");
            }
            else
            {
                Console.WriteLine("Checkmate!");
                Console.WriteLine("Winner: " + match.Player);
            }
        }

        public static void PrintCapturedPieces(ChessMatch partida)
        {
            Console.WriteLine("Captured pieces:");

            Console.Write("White: ");
            PrintPieces(partida.CapturedPieces(PieceColor.White));
            Console.WriteLine();

            Console.Write("Black: ");
            var aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintPieces(partida.CapturedPieces(PieceColor.Black));
            Console.WriteLine();
            Console.ForegroundColor = aux;
        }

        public static void PrintPieces(HashSet<Piece> pieces)
        {
            Console.Write("[ ");
            foreach (Piece piece in pieces)
                Console.Write(piece + " ");
            Console.Write("]");
        }

        public static void PrintBoard(MatchBoard board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.GetPiece(i, j)!);
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintBoard(MatchBoard board, bool[,] moves)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (moves[i, j])
                        Console.BackgroundColor = fundoAlterado;
                    else
                        Console.BackgroundColor = fundoOriginal;
                    PrintPiece(board.GetPiece(i, j));
                }
                Console.WriteLine();
                Console.BackgroundColor = fundoOriginal;
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadrez ReadPosition()
        {
            var input = Console.ReadLine();
            var column = input[0];
            var line = int.Parse(input[1] + "");

            return new PosicaoXadrez(column, line);
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
                Console.Write("- ");
            else
            {
                if (piece.Color == PieceColor.White)
                    Console.Write(piece);
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
