using ChessGame.Entities.Chess;
using ChessGame.Entities.Enums;

namespace ChessGame.Entities.Rules;

public class Pawn : Piece
{
    public Pawn(Color color, Board board) : base(color, board)
    {
    }

    public bool CanCapture(Position position)
    {
        Piece piece = Board.Piece(position);
        return piece != null && piece.Color != Color;
    }

    public bool FreeMove(Position position)
    {
        return Board.Piece(position) == null;
    }

    public override bool[,] IsPossibleMove()
    {
        bool[,] placeble = new bool[Board.Lines, Board.Columns];

        Position position = new Position(0, 0);

        //SetValues method set the values into the class Position, not in local variable position.

        if (Color == Color.White)
        {
            //Pawn first move
            position.SetValues(Position.Line - 2, Position.Column);
            if (Board.IsValidPosition(position) && FreeMove(position) && Moves == 0)
            {
                placeble[position.Line, position.Column] = true;
            }
            //North
            position.SetValues(Position.Line - 1, Position.Column);
            if (Board.IsValidPosition(position) && FreeMove(position))
            {
                placeble[position.Line, position.Column] = true;
            }
            //Capture moves
            position.SetValues(Position.Line - 1, Position.Column - 1);
            if (Board.IsValidPosition(position) && CanCapture(position))
            {
                placeble[position.Line, position.Column] = true;
            }
            position.SetValues(Position.Line - 1, Position.Column + 1);
            if (Board.IsValidPosition(position) && CanCapture(position))
            {
                placeble[position.Line, position.Column] = true;
            }
        }
        else
        {
            //Pawn first move
            position.SetValues(Position.Line + 2, Position.Column);
            if (Board.IsValidPosition(position) && FreeMove(position) && Moves == 0)
            {
                placeble[position.Line, position.Column] = true;
            }
            //South
            position.SetValues(Position.Line + 1, Position.Column);
            if (Board.IsValidPosition(position) && FreeMove(position))
            {
                placeble[position.Line, position.Column] = true;
            }
            //Capture moves
            position.SetValues(Position.Line + 1, Position.Column - 1);
            if (Board.IsValidPosition(position) && CanCapture(position))
            {
                placeble[position.Line, position.Column] = true;
            }
            position.SetValues(Position.Line + 1, Position.Column + 1);
            if (Board.IsValidPosition(position) && CanCapture(position))
            {
                placeble[position.Line, position.Column] = true;
            }
        }
        return placeble;
    }

    public override string ToString()
    {
        return "P";
    }
}