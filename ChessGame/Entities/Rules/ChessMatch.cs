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
    private HashSet<Piece> Pieces { get; set; }
    private HashSet<Piece> CapturedPieces { get; set; }

    public ChessMatch()
    {
        Board = new Board(8, 8);
        Turn = 1;
        PlayerTurn = Color.White;
        Finished = false;
        Pieces = new HashSet<Piece>();
        CapturedPieces = new HashSet<Piece>();
        PlacePieces();
    }

    //Grabs the pulled piece from the board (returned pulledPiece from PullPiece) and place it in another position.
    public void Move(Position origin, Position destination)
    {
        Piece piece = Board.PullPiece(origin);
        piece.IncreaseMoves();
        //If destination have a opponent piece, than it is captured.
        Piece capturedPiece = Board.PullPiece(destination);
        Board.PutPiece(piece, destination);
        if (capturedPiece != null)
        {
            CapturedPieces.Add(capturedPiece);
        }
    }

    //Executes the move and change the turn to next player.
    public void ExecuteMove(Position origin, Position destination)
    {
        Move(origin, destination);
        Turn++;
        PassTurn();
    }

    //Throw an exception according to the possibles from origin piece.
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
    
    //Throw an exception if the origin piece cannot move to the destination, when it's not an empty position (null).
    //Also, it validates if the piece is able to move to it, according to your movement rules.
    public void ValidateDestinationPosition(Position origin, Position destination)
    {
        if (!Board.Piece(origin).CanMoveTo(destination))
        {
            throw new BoardException("Invalid destination!");
        }
    }

    //Player turn change method.
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

    //Returns all captured pieces from a specific color.
    public HashSet<Piece> Captured(Color color)
    {
        HashSet<Piece> captured = new HashSet<Piece>();
        foreach (Piece piece in CapturedPieces)
        {
            if (piece.Color == color)
            {
                captured.Add(piece);
            }
        }
        return captured;
    }
    
    //Returns all remaining pieces from a specific color.
    public HashSet<Piece> RemainingPieces(Color color)
    {
        HashSet<Piece> pieces = new HashSet<Piece>();
        foreach (Piece piece in Pieces)
        {
            if (piece.Color == color)
            {
                pieces.Add(piece);
            }
        }
        pieces.ExceptWith(Captured(color));
        return pieces;
    }

    //Place a new piece into the chess board and into the HashSet Pieces.
    public void PutNewPiece(int line, char column, Piece piece)
    {
        Board.PutPiece(piece, new ChessPosition(line, column).ToPosition());
        Pieces.Add(piece);
    }

    //Instantiates a piece object into a board position.
    private void PlacePieces()
    {
        PutNewPiece(1, 'a',new Rook(Color.White, Board));
        PutNewPiece(1, 'h',new Rook(Color.White, Board));
        PutNewPiece(1, 'b',new Knight(Color.White, Board));
        PutNewPiece(1, 'g',new Knight(Color.White, Board));
        PutNewPiece(1, 'c',new Bishop(Color.White, Board));
        PutNewPiece(1, 'f',new Bishop(Color.White, Board));
        PutNewPiece(1, 'd',new Queen(Color.White, Board));
        PutNewPiece(1, 'e',new King(Color.White, Board));
        PutNewPiece(2, 'd',new Pawn(Color.White, Board));
        PutNewPiece(2, 'e',new Pawn(Color.White, Board));
        PutNewPiece(2, 'f',new Pawn(Color.White, Board));
        PutNewPiece(3, 'h',new King(Color.White, Board));
        
        PutNewPiece(8, 'a',new Rook(Color.Black, Board));
        PutNewPiece(8, 'h',new Rook(Color.Black, Board));
        PutNewPiece(8, 'b',new Knight(Color.Black, Board));
        PutNewPiece(8, 'g',new Knight(Color.Black, Board));
        PutNewPiece(8, 'c',new Bishop(Color.Black, Board));
        PutNewPiece(8, 'f',new Bishop(Color.Black, Board));
        PutNewPiece(8, 'd',new Queen(Color.Black, Board));
        PutNewPiece(8, 'e',new King(Color.Black, Board));
    }
}