﻿using System;
using ChessGame.Entities.Chess;
using ChessGame.Entities.Enums;
using ChessGame.Entities.Rules;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.PutPiece(new Rook(Color.White, board), new Position(0,0));
            board.PutPiece(new Rook(Color.White, board), new Position(1,3));
            board.PutPiece(new King(Color.Black, board), new Position(2,4));
            
            Screen.PrintBoard(board);
        }
    }
}