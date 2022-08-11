using ChessGame.Entities.Chess;
using ChessGame.Entities.Enums;

namespace ChessGame.Entities.Rules;

public class Pawn : Piece
{
    public Pawn(Color color, Board board) : base(color, board)
    {
    }

    public override string ToString()
    {
        return "P";
    }
}