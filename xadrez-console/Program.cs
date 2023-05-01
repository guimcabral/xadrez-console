using System;
using TabuleiroNM;
using Xadrez;

namespace Xadrez_console
{
    class Program
    {
        static void Main()
        {
            try
            {
                var match = new ChessMatch();

                while (!match.Finished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(match);

                        Console.WriteLine();
                        Console.Write("Inital position: ");
                        var initialPosition = Screen.ReadPosition().ToPosition();
                        match.ValidateInitialPosition(initialPosition);

                        var possiblePositions = match.Board.GetPiece(initialPosition)?.GetMoves() ?? throw new NullReferenceException();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Final position: ");
                        var finalPosition = Screen.ReadPosition().ToPosition();
                        match.ValidateFinalPosition(initialPosition, finalPosition);

                        match.ExecutePlay(initialPosition, finalPosition);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.PrintMatch(match);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}