using ChessGame.Entities.Enums;

namespace ChessGame.Entities.Chess;

public abstract class Piece
{
    public Color Color { get; protected set; }
    public Position Position { get; set; }
    public int Moves { get; protected set; }
    public Board Board { get; protected set; }

    public Piece(Color color, Board board)
    {
        Color = color;
        Position = null;
        Board = board;
        Moves = 0;
    }

    public void IncreaseMoves()
    {
        Moves++;
    }
    
    //Checks if the position is empty (null) or if has an opponent piece there (for capturing).
    public bool CanMove(Position position)
    {
        Piece piece = Board.Piece(position);
        return piece == null || piece.Color != Color;
    }

    public bool HasPossibleMoves()
    {
        bool[,] placeble = IsPossibleMove();
        for (int i = 0; i < Board.Lines; i++)
        {
            for (int j = 0; j < Board.Columns; j++)
            {
                if (placeble[i, j])
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CanMoveTo(Position position)
    {
        return IsPossibleMove()[position.Line, position.Column];
    }
    
    //Return true if the piece can be placed in a specific position.
    public abstract bool[,] IsPossibleMove();
}