using ChessGame.Entities.Chess;
using ChessGame.Entities.Enums;

namespace ChessGame.Entities.Rules;

public class King : Piece
{
    public King(Color color, Board board) : base(color, board)
    {
    }

    public override string ToString()
    {
        return "K";
    }
}