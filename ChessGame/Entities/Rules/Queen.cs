using ChessGame.Entities.Chess;
using ChessGame.Entities.Enums;

namespace ChessGame.Entities.Rules;

public class Queen : Piece
{
    public Queen(Color color, Board board) : base(color, board)
    {
    }

    //Movement rules of the Queen.
    public override bool[,] IsPossibleMove()
    {
        bool[,] placeble = new bool[Board.Lines, Board.Columns];

        Position position = new Position(0, 0);
        
        //SetValues method set the values into the class Position.
            
        //North
        position.SetValues(Position.Line - 1, Position.Column);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.SetValues(position.Line - 1, position.Column);
        }
        //Northeast
        position.SetValues(Position.Line - 1, Position.Column + 1);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.SetValues(position.Line - 1, position.Column + 1);
        }
        //East
        position.SetValues(Position.Line, Position.Column + 1);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.SetValues(position.Line, position.Column + 1);
        }
        //Southeast
        position.SetValues(Position.Line + 1, Position.Column + 1);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.SetValues(position.Line + 1, position.Column + 1);
        }
        //South
        position.SetValues(Position.Line + 1, Position.Column);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.SetValues(position.Line + 1, position.Column);
        }
        //Southwest
        position.SetValues(Position.Line + 1, Position.Column - 1);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.SetValues(position.Line + 1, position.Column - 1);
        }
        //West
        position.SetValues(Position.Line, Position.Column - 1);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.SetValues(position.Line, position.Column - 1);
        }
        //Northwest
        position.SetValues(Position.Line - 1, Position.Column - 1);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.SetValues(position.Line - 1, position.Column - 1);
        }
        return placeble;
    }

    public override string ToString()
    {
        return "Q";
    }
}