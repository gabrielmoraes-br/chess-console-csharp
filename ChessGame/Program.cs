using System;
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
                Board board = new Board(8, 8);

                board.PutPiece(new Rook(Color.White, board), new Position(5, 1));
                board.PutPiece(new Knight(Color.Black, board), new Position(1, 3));
                board.PutPiece(new King(Color.Black, board), new Position(0, 2));
                board.PutPiece(new Bishop(Color.Black, board), new Position(3, 7));
                board.PutPiece(new Pawn(Color.White, board), new Position(4, 4));
                board.PutPiece(new Queen(Color.White, board), new Position(7, 2));
                board.PutPiece(new King(Color.White, board), new Position(7, 3));

                Screen.PrintBoard(board);
            }
            catch (BoardException e)
            {
                Console.WriteLine($"An error has occurred: {e.Message}");
            }
        }
    }
}