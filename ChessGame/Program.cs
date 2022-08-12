using System;
using System.Text.Encodings.Web;
using ChessGame.Entities.Chess;
using ChessGame.Entities.Enums;
using ChessGame.Entities.Exceptions;
using ChessGame.Entities.Rules;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();

                while (!match.Finished)
                {
                    try
                    {
                        Console.Clear();

                        Screen.PrintBoard(match.Board);
                        Console.WriteLine();

                        Console.WriteLine($"Turn: {match.Turn}");
                        Console.WriteLine($"Waiting move from: {match.PlayerTurn}");

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadPosition().ToPosition();
                        match.ValidateOriginPosition(origin);

                        bool[,] possiblePositions = match.Board.Piece(origin).IsPossibleMove();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destination: ");
                        Position destination = Screen.ReadPosition().ToPosition();
                        match.ValidateDestinationPosition(origin, destination);

                        match.ExecuteMove(origin, destination);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine($"Invalid action: {e.Message}");
                        Console.ReadLine();
                    }
                }

            }
            catch (BoardException e)
            {
                Console.WriteLine($"An error has occurred: {e.Message}");
            }
        }
    }
}