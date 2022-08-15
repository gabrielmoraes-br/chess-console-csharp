using ChessGame.Entities.Chess;
using ChessGame.Entities.Enums;
using ChessGame.Entities.Rules;

namespace ChessGame;

public class Screen
{
    //Prints the chess game on the console.
    public static void PrintChessMatch(ChessMatch match)
    {
        PrintBoard(match.Board);
        Console.WriteLine();
        PrintScore(match);
        Console.WriteLine();
        Console.WriteLine($"Turn: {match.Turn}");
        Console.WriteLine($"Waiting move: {match.PlayerTurn}");
    }

    //Prints the score on the console.
    public static void PrintScore(ChessMatch match)
    {
        Console.WriteLine("Captured Pieces: ");
        Console.Write("White: ");
        PrintCapturedPieces(match.Captured(Color.White));
        Console.WriteLine();
        ConsoleColor white = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Black: ");
        PrintCapturedPieces(match.Captured(Color.Black));
        Console.ForegroundColor = white;
        Console.WriteLine();
    }

    //Prints the letter of the pieces on the score.
    public static void PrintCapturedPieces(HashSet<Piece> captured)
    {
        Console.Write("[");
        foreach (Piece piece in captured)
        {
            Console.Write($"{piece} ");
        }
        Console.Write("]");
    }
    
    //Prints the chess board on the console black screen.
    public static void PrintBoard(Board board)
    {
        for (int i = 0; i < board.Lines; i++)
        {
            Console.Write($"{8 - i} ");
            for (int j = 0; j < board.Columns; j++)
            {
                PrintPiece(board.Piece(i, j));
            }
            Console.WriteLine();
        }
        Console.WriteLine("  a b c d e f g h");
    }
    
    //Prints the chess board on the console black screen and pints the available move squares from a piece.
    public static void PrintBoard(Board board, bool[,] possiblePositions)
    {
        ConsoleColor standard = Console.BackgroundColor;
        ConsoleColor alternative = ConsoleColor.DarkGray;
        
        for (int i = 0; i < board.Lines; i++)
        {
            Console.BackgroundColor = standard;
            Console.Write($"{8 - i} ");
            for (int j = 0; j < board.Columns; j++)
            {
                if (possiblePositions[i, j])
                {
                    Console.BackgroundColor = alternative;
                }
                else
                {
                    Console.BackgroundColor = standard;
                }
                PrintPiece(board.Piece(i, j));
            }
            Console.WriteLine();
        }
        Console.BackgroundColor = standard;
        Console.WriteLine("  a b c d e f g h");
    }

    //Gets the string format position ("a1" for example) and convert it to a ChessPosition object format.
    public static ChessPosition ReadPosition()
    {
        string position = Console.ReadLine();
        char column = position[0];
        int line = int.Parse($"{position[1]}");
        return new ChessPosition(line, column);
    }

    //Prints "- " into a null board position or prints the piece letter according your kind and color.
    public static void PrintPiece(Piece piece)
    {
        if (piece == null)
        {
            Console.Write("- ");
        }
        else
        {
            if (piece.Color == Color.White)
            {
                Console.Write(piece);
            }
            else
            {
                //Changes piece text color to Yellow for the Black player, than it changes back to white.
                ConsoleColor whiteColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = whiteColor;
            }
            Console.Write(" ");
        }
    }
}