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
                //Every match is instantiated as new, clean and ready to play.
                ChessMatch match = new ChessMatch();

                //The move actions will be repeating until match.Finished gets true value.
                while (!match.Finished)
                {
                    try
                    {
                        Console.Clear();
                        //Console prints the chess board and player actions.
                        Screen.PrintChessMatch(match);
                        
                        Console.WriteLine();
                        Console.Write("Origin piece (ex. 'd2'): ");
                        
                        //Console awaits an action. Player types a chess position to choose a piece to move.
                        Position origin = Screen.ReadPosition().ToPosition();
                        //The chosen position is converted to array index and next it will be validated.
                        match.ValidateOriginPosition(origin);

                        //Now the game checks if the chosen piece has possibilities to move. 
                        bool[,] possiblePositions = match.Board.Piece(origin).IsPossibleMove();

                        //The console is cleared and the chess board is reprint with the move possibilities.
                        Console.Clear();
                        Screen.PrintBoard(match.Board, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destination position (ex. 'd4'): ");
                        
                        //Console awaits an action. Player types a chess position to be your piece destination.
                        Position destination = Screen.ReadPosition().ToPosition();
                        //The chosen position is converted to array index and next it will be validated.
                        match.ValidateDestinationPosition(origin, destination);

                        //Finally, the move is executed according to the chess rules.
                        match.ExecuteMove(origin, destination);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine($"Invalid action: {e.Message}");
                        Console.ReadLine();
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine($"Invalid position: {e.Message}");
                        Console.ReadLine();
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Console.WriteLine($"Invalid position: {e.Message}");
                        Console.ReadLine();
                    }
                }
                //In the end of the game, when match.Finished has true value, the winner is displayed.
                Console.Clear();
                Screen.PrintChessMatch(match);
            }
            catch (BoardException e)
            {
                Console.WriteLine($"An error has occurred: {e.Message}");
            }
        }
    }
}