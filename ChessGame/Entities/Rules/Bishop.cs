using ChessGame.Entities.Chess;
using ChessGame.Entities.Enums;

namespace ChessGame.Entities.Rules;

public class Bishop : Piece
{
    public Bishop(Color color, Board board) : base(color, board)
    {
    }

    public override string ToString()
    {
        return "B";
    }
}