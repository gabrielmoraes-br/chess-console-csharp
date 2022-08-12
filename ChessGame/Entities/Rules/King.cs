using System.Security.Cryptography.X509Certificates;
using ChessGame.Entities.Chess;
using ChessGame.Entities.Enums;

namespace ChessGame.Entities.Rules;

public class King : Piece
{
    public King(Color color, Board board) : base(color, board)
    {
    }

    public override bool[,] IsPossibleMove()
    {
        bool[,] placeble = new bool[Board.Lines, Board.Columns];

        Position position = new Position(0, 0);
        
        //SetValues method set the values into the class Position, not in local variable position.
        
        //Above
        position.SetValues(Position.Line - 1, Position.Column);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
        }
        //Northeast
        position.SetValues(Position.Line - 1, Position.Column + 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
        }
        //Right
        position.SetValues(Position.Line, Position.Column + 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
        }
        //Southeast
        position.SetValues(Position.Line + 1, Position.Column + 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
        }
        //Below
        position.SetValues(Position.Line + 1, Position.Column);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
        }
        //Southwest
        position.SetValues(Position.Line + 1, Position.Column - 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
        }
        //Left
        position.SetValues(Position.Line, Position.Column - 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
        }
        //Northwest
        position.SetValues(Position.Line - 1, Position.Column - 1);
        if (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
        }
        return placeble;
    }
    
    public override string ToString()
    {
        return "K";
    }
}