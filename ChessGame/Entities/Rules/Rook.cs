using ChessGame.Entities.Chess;
using ChessGame.Entities.Enums;

namespace ChessGame.Entities.Rules;

public class Rook : Piece
{
    public Rook(Color color, Board board) : base(color, board)
    {
    }

    public override string ToString()
    {
        return "R";
    }
}