using ChessGame.Entities.Chess;
using ChessGame.Entities.Enums;
using ChessGame.Entities.Rules;

namespace ChessGame;

public class Screen
{
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

    public static ChessPosition ReadPosition()
    {
        string position = Console.ReadLine();
        char column = position[0];
        int line = int.Parse($"{position[1]}");
        return new ChessPosition(column, line);
    }

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
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
            Console.Write(" ");
        }
    }
}