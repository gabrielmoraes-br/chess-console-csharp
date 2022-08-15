using ChessGame.Entities.Exceptions;

namespace ChessGame.Entities.Chess;

public class Board
{
    public int Lines { get; set; }
    public int Columns { get; set; }
    private Piece[,] PlacedPieces { get; set; }

    public Board(int lines, int columns)
    {
        Lines = lines;
        Columns = columns;
        PlacedPieces = new Piece[lines, columns];
    }

    //Sets the line and column of a piece object, returning this piece into the board array position.
    public Piece Piece(int line, int column)
    {
        return PlacedPieces[line, column];
    }

    //Sets the position argument to a piece object, returning this piece into the board array position.
    public Piece Piece(Position position)
    {
        return PlacedPieces[position.Line, position.Column];
    }

    //Returns true if the respective position is empty (null).
    public bool IsNullPosition(Position position)
    {
        ValidatePosition(position);
        return Piece(position) == null;
    }
    
    //Places a piece object in the respective board position and set the position to the piece.
    public void PutPiece(Piece piece, Position position)
    {
        if (!IsNullPosition(position))
        {
            throw new BoardException("Position already occupied by a piece!");
        }
        PlacedPieces[position.Line, position.Column] = piece;
        piece.Position = position;
    }

    //Removes a piece from the respective board position, return the removed piece.
    public Piece PullPiece(Position position)
    {
        if (Piece(position) == null)
        {
            return null;
        }
        Piece pulledPiece = Piece(position);
        pulledPiece.Position = null;
        PlacedPieces[position.Line, position.Column] = null;
        return pulledPiece;
    }

    //Returns false when the position is out of the range from array index of the board.
    public bool IsValidPosition(Position position)
    {
        if (position.Line < 0 || position.Line >= Lines || 
            position.Column < 0 || position.Column >= Columns)
        {
            return false;
        }
        return true;
    }

    //Throw an exception if the position is out of array index of the board.
    public void ValidatePosition(Position position)
    {
        if (!IsValidPosition(position))
        {
            throw new BoardException("Invalid position!");
        }
    }
}