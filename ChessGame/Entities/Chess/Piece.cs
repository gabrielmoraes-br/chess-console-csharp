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
    
    //Increase moves from a piece.
    public void IncreaseMoves()
    {
        Moves++;
    }
    
    //Decrease moves from a piece. Useful in UndoMove method from ChessMatch class.
    public void DecreaseMoves()
    {
        Moves--;
    }
    
    //Checks if the position is empty (null) or if has an opponent piece there (for capturing).
    public bool CanMove(Position position)
    {
        Piece piece = Board.Piece(position);
        return piece == null || piece.Color != Color;
    }

    //Tests the board array in relation to the piece position and return true if it is capable to move somewhere.
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

    //Returns true if the position argument is a possible move (to destination) of the respective piece. 
    public bool CanMoveTo(Position position)
    {
        return IsPossibleMove()[position.Line, position.Column];
    }
    
    //Returns true if the piece can be placed in a specific position, according to your movement rules.
    public abstract bool[,] IsPossibleMove();
}