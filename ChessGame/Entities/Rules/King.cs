using System.Security.Cryptography.X509Certificates;
using ChessGame.Entities.Chess;
using ChessGame.Entities.Enums;

namespace ChessGame.Entities.Rules;

public class King : Piece
{
    private ChessMatch Match;
    public King(Color color, Board board, ChessMatch match) : base(color, board)
    {
        Match = match;
    }
    
    //Returns true if Rook is placed according Castling rules.
    private bool CanCastling(Position position)
    {
        Piece piece = Board.Piece(position);
        return piece != null && piece is Rook && piece.Color == Color && piece.Moves == 0;
    }

    //Movement rules of the King.
    public override bool[,] IsPossibleMove()
    {
        bool[,] placeble = new bool[Board.Lines, Board.Columns];

        Position position = new Position(0, 0);
        
        //SetValues method set the values into the class Position, not in local variable position.
        
        //North
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
        //East
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
        //South
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
        //West
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
        
        //Castling Kingside (short)
        if (Moves == 0 && !Match.Check)
        {
            Position closerRook = new Position(Position.Line, Position.Column + 3);
            if (CanCastling(closerRook))
            {
                Position pos1 = new Position(Position.Line, Position.Column + 1);
                Position pos2 = new Position(Position.Line, Position.Column + 2);
                if (Board.Piece(pos1) == null && Board.Piece(pos2) == null)
                {
                    placeble[Position.Line, Position.Column + 2] = true;
                }
            } 
        }
        //Castling Queenside (long)
        if (Moves == 0 && !Match.Check)
        {
            Position farRook = new Position(Position.Line, Position.Column - 4);
            if (CanCastling(farRook))
            {
                Position pos1 = new Position(Position.Line, Position.Column - 1);
                Position pos2 = new Position(Position.Line, Position.Column - 2);
                Position pos3 = new Position(Position.Line, Position.Column - 3);
                if (Board.Piece(pos1) == null && Board.Piece(pos2) == null && Board.Piece(pos3) == null)
                {
                    placeble[Position.Line, Position.Column - 2] = true;
                }
            } 
        }
        return placeble;
    }
    
    public override string ToString()
    {
        return "K";
    }
}