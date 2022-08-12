using ChessGame.Entities.Exceptions;

namespace ChessGame.Entities.Chess;

public class Board
{
    public int Lines { get; set; }
    public int Columns { get; set; }
    private Piece[,] Pieces { get; set; }

    public Board(int lines, int columns)
    {
        Lines = lines;
        Columns = columns;
        Pieces = new Piece[lines, columns];
    }

    public Piece Piece(int line, int column)
    {
        return Pieces[line, column];
    }

    public Piece Piece(Position position)
    {
        return Pieces[position.Line, position.Column];
    }

    public bool IsNullPosition(Position position)
    {
        ValidatePosition(position);
        return Piece(position) == null;
    }
    
    public void PutPiece(Piece piece, Position position)
    {
        if (!IsNullPosition(position))
        {
            throw new BoardException("Position already occupied by a piece!");
        }
        Pieces[position.Line, position.Column] = piece;
        piece.Position = position;
    }

    public Piece PullPiece(Position position)
    {
        if (Piece(position) == null)
        {
            return null;
        }
        Piece aux = Piece(position);
        aux.Position = null;
        Pieces[position.Line, position.Column] = null;
        return aux;
    }

    //Return false when the position is out of the range from array index of the board.
    public bool IsValidPosition(Position position)
    {
        if (position.Line < 0 || position.Line >= Lines || 
            position.Column < 0 || position.Column >= Columns)
        {
            return false;
        }
        return true;
    }

    public void ValidatePosition(Position position)
    {
        if (!IsValidPosition(position))
        {
            throw new BoardException("Invalid position!");
        }
    }
}