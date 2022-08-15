using ChessGame.Entities.Chess;

namespace ChessGame.Entities.Rules;

public class ChessPosition
{
    public int Line { get; set; }
    public char Column { get; set; }

    public ChessPosition(int line, char column)
    {
        Line = line;
        Column = column;
    }

    //Converts the ChessPosition format ("a1" for example) to Position format (array index, "[0,0]").
    public Position ToPosition()
    {
        //The column char will subtract the 'a' character (ASCII Table based),
        //This means that column value will take an Integer number as result.
        return new Position(8 - Line, Column - 'a');
    }

    public override string ToString()
    {
        return $"{Column}{Line}";
    }
}