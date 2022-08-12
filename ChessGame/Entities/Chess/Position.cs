namespace ChessGame.Entities.Chess;

public class Position
{
    public int Line { get; set; }
    public int Column { get; set; }

    public Position(int line, int column)
    {
        Line = line;
        Column = column;
    }

    //Set the values of a Position object.
    public void SetValues(int line, int column)
    {
        Line = line;
        Column = column;
    }

    public override string ToString()
    {
        return $"{Line}, {Column}";
    }
}