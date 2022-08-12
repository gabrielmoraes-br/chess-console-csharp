using ChessGame.Entities.Chess;
using ChessGame.Entities.Enums;

namespace ChessGame.Entities.Rules;

public class Queen : Piece
{
    public Queen(Color color, Board board) : base(color, board)
    {
    }

    public override bool[,] IsPossibleMove()
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return "Q";
    }
}