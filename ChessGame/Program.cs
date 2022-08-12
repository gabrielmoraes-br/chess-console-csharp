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
                    Console.Clear();
                    Screen.PrintBoard(match.Board);

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = Screen.ReadPosition().ToPosition();
                    Console.Write("Destination: ");
                    Position destination = Screen.ReadPosition().ToPosition();
                    
                    match.ExecuteMove(origin, destination);
                }
                
            }
            catch (BoardException e)
            {
                Console.WriteLine($"An error has occurred: {e.Message}");
            }
        }
    }
}