using System.Text.Unicode;
using ChessGame.Entities.Chess;
using ChessGame.Entities.Enums;
using ChessGame.Entities.Exceptions;

namespace ChessGame.Entities.Rules;

public class ChessMatch
{
    public Board Board { get; private set; }
    public int Turn { get; private set; }
    public Color PlayerTurn { get; private set; }
    public bool Finished { get; private set; }

    public ChessMatch()
    {
        Board = new Board(8, 8);
        Turn = 1;
        PlayerTurn = Color.White;
        Finished = false;
        PutPieces();
    }

    public void Move(Position origin, Position destination)
    {
        Piece piece = Board.PullPiece(origin);
        piece.IncreaseMoves();
        Piece capturedPiece = Board.PullPiece(destination);
        Board.PutPiece(piece, destination);
    }

    public void ExecuteMove(Position origin, Position destination)
    {
        Move(origin, destination);
        Turn++;
        PassTurn();
    }

    public void ValidateOriginPosition(Position position)
    {
        if (Board.Piece(position) == null)
        {
            throw new BoardException("Piece does not exist!");
        }
        if (PlayerTurn != Board.Piece(position).Color)
        {
            throw new BoardException("This piece belongs to the opponent!");
        }
        if (!Board.Piece(position).HasPossibleMoves())
        {
            throw new BoardException("There are no possible moves for this piece.");
        }
    }
    
    public void ValidateDestinationPosition(Position origin, Position destination)
    {
        if (!Board.Piece(origin).CanMoveTo(destination))
        {
            throw new BoardException("Invalid destination!");
        }
    }

    private void PassTurn()
    {
        if (PlayerTurn == Color.White)
        {
            PlayerTurn = Color.Black;
        }
        else
        {
            PlayerTurn = Color.White;
        }
    }

    private void PutPieces()
    {
        Board.PutPiece(new Rook(Color.White, Board), new ChessPosition('a',1).ToPosition());
        Board.PutPiece(new Rook(Color.White, Board), new ChessPosition('h',1).ToPosition());
        Board.PutPiece(new Knight(Color.White, Board), new ChessPosition('b', 1).ToPosition());
        Board.PutPiece(new Knight(Color.White, Board), new ChessPosition('g', 1).ToPosition());
        Board.PutPiece(new Bishop(Color.White, Board), new ChessPosition('c', 1).ToPosition());
        Board.PutPiece(new Bishop(Color.White, Board), new ChessPosition('f', 1).ToPosition());
        Board.PutPiece(new Queen(Color.White, Board), new ChessPosition('d', 1).ToPosition());
        Board.PutPiece(new King(Color.White, Board), new ChessPosition('e', 1).ToPosition());
        Board.PutPiece(new Pawn(Color.White, Board), new ChessPosition('d', 2).ToPosition());
        Board.PutPiece(new Pawn(Color.White, Board), new ChessPosition('e', 2).ToPosition());
        Board.PutPiece(new Pawn(Color.White, Board), new ChessPosition('f', 2).ToPosition());
        Board.PutPiece(new King(Color.White, Board), new ChessPosition('h', 3).ToPosition());
        
        Board.PutPiece(new Rook(Color.Black, Board), new ChessPosition('a',8).ToPosition());
        Board.PutPiece(new Rook(Color.Black, Board), new ChessPosition('h',8).ToPosition());
        Board.PutPiece(new Knight(Color.Black, Board), new ChessPosition('b', 8).ToPosition());
        Board.PutPiece(new Knight(Color.Black, Board), new ChessPosition('g', 8).ToPosition());
        Board.PutPiece(new Bishop(Color.Black, Board), new ChessPosition('c', 8).ToPosition());
        Board.PutPiece(new Bishop(Color.Black, Board), new ChessPosition('f', 8).ToPosition());
        Board.PutPiece(new Queen(Color.Black, Board), new ChessPosition('d', 8).ToPosition());
        Board.PutPiece(new King(Color.Black, Board), new ChessPosition('e', 8).ToPosition());
    }
}