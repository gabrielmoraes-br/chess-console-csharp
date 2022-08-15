using ChessGame.Entities.Chess;
using ChessGame.Entities.Enums;

namespace ChessGame.Entities.Rules;

public class Knight : Piece
{
    public Knight(Color color, Board board) : base(color, board)
    {
    }

    //Movement rules of the Knight (Horse).
    public override bool[,] IsPossibleMove()
    {
        bool[,] placeble = new bool[Board.Lines, Board.Columns];

        Position position = new Position(0, 0);
        
        //SetValues method set the values into the class Position, not in local variable position.
        
        //'L' Possible Moves for Knight:
        position.SetValues(Position.Line - 1, Position.Column - 2);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
        }
        position.SetValues(Position.Line - 2, Position.Column - 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
        }
        position.SetValues(Position.Line - 2, Position.Column + 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
        }
        position.SetValues(Position.Line - 1, Position.Column + 2);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
        }
        position.SetValues(Position.Line + 1, Position.Column + 2);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
        }
        position.SetValues(Position.Line + 2, Position.Column + 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
        }
        position.SetValues(Position.Line + 2, Position.Column - 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
        }
        position.SetValues(Position.Line + 1, Position.Column - 2);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
        }
        return placeble;
    }
    public override string ToString()
    {
        //Letter 'H' to represent the Horse, distinguishing from the 'K' of King.
        return "H";
    }
}