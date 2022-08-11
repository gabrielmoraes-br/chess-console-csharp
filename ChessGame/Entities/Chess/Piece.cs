using ChessGame.Entities.Enums;

namespace ChessGame.Entities.Chess;

public class Piece
{
    public Color Color { get; protected set; }
    public Position Position { get; set; }
    public int Movements { get; protected set; }
    public Board Board { get; protected set; }

    public Piece(Color color, Board board)
    {
        Color = color;
        Position = null;
        Board = board;
        Movements = 0;
    }
}