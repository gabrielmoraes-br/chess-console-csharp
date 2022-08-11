using ChessGame.Entities.Chess;
using ChessGame.Entities.Enums;

namespace ChessGame.Entities.Rules;

public class Knight : Piece
{
    public Knight(Color color, Board board) : base(color, board)
    {
    }

    public override string ToString()
    {
        return "H";
    }
}