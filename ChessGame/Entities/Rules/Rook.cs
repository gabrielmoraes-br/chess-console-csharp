using ChessGame.Entities.Chess;
using ChessGame.Entities.Enums;

namespace ChessGame.Entities.Rules;

public class Rook : Piece
{
    public Rook(Color color, Board board) : base(color, board)
    {
    }

    public override bool[,] IsPossibleMove()
    {
        bool[,] placeble = new bool[Board.Lines, Board.Columns];

        Position position = new Position(0, 0);
        
        //SetValues method set the values into the class Position.
            
        //Above
        position.SetValues(Position.Line - 1, Position.Column);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.Line -= 1;
        }
        //Right
        position.SetValues(Position.Line, Position.Column + 1);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.Column += 1;
        }
        //Below
        position.SetValues(Position.Line + 1, Position.Column);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.Line += 1;
        }
        //Left
        position.SetValues(Position.Line, Position.Column - 1);
        while (Board.IsValidPosition(position) && CanMove(position))
        {
            placeble[position.Line, position.Column] = true;
            if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
            {
                break;
            }
            position.Column -= 1;
        }
        return placeble;
    }
    
    public override string ToString()
    {
        return "R";
    }
}