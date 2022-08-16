using System.Text.Unicode;
using ChessGame.Entities.Chess;
using ChessGame.Entities.Enums;
using ChessGame.Entities.Exceptions;

namespace ChessGame.Entities.Rules;

public class ChessMatch
{
    public Board Board { get; private set; }
    public int Turn { get; private set; }
    public Color PlayerTurn { get; private set; }
    public bool Finished { get; private set; }
    private HashSet<Piece> Pieces { get; set; }
    private HashSet<Piece> CapturedPieces { get; set; }
    public bool Check { get; private set; }
    public Piece EnPassantThreat { get; private set; }

    public ChessMatch()
    {
        Board = new Board(8, 8);
        Turn = 1;
        PlayerTurn = Color.White;
        Finished = false;
        Check = false;
        EnPassantThreat = null;
        Pieces = new HashSet<Piece>();
        CapturedPieces = new HashSet<Piece>();
        PlacePieces();
    }

    //Grabs the pulled piece from the board (returned pulledPiece from PullPiece) and place it in another position.
    public Piece Move(Position origin, Position destination)
    {
        Piece piece = Board.PullPiece(origin);
        piece.IncreaseMoves();
        //If destination have a opponent piece, than it is captured.
        Piece capturedPiece = Board.PullPiece(destination);
        Board.PutPiece(piece, destination);
        if (capturedPiece != null)
        {
            CapturedPieces.Add(capturedPiece);
        }
        //Castling Kingside (short)
        if (piece is King && destination.Column == origin.Column + 2)
        {
            Position rookOrigin = new Position(origin.Line, origin.Column + 3);
            Position rookDestination = new Position(origin.Line, origin.Column + 1);
            Piece closerRook = Board.PullPiece(rookOrigin);
            closerRook.IncreaseMoves();
            Board.PutPiece(closerRook, rookDestination);
        }
        //Castling Queenside (long)
        if (piece is King && destination.Column == origin.Column - 2)
        {
            Position rookOrigin = new Position(origin.Line, origin.Column - 4);
            Position rookDestination = new Position(origin.Line, origin.Column - 1);
            Piece farRook = Board.PullPiece(rookOrigin);
            farRook.IncreaseMoves();
            Board.PutPiece(farRook, rookDestination);
        }
        //En Passant, Pawn special move.
        if (piece is Pawn)
        {
            if (origin.Column != destination.Column && capturedPiece == null)
            {
                Position pawnPosition;
                if (piece.Color == Color.White)
                {
                    pawnPosition = new Position(destination.Line + 1, destination.Column);
                }
                else
                {
                    pawnPosition = new Position(destination.Line - 1, destination.Column);
                }
                capturedPiece = Board.PullPiece(pawnPosition);
                CapturedPieces.Add(capturedPiece);
            }
        }
        return capturedPiece;
    }

    //Executes the move and change the turn to next player.
    public void ExecuteMove(Position origin, Position destination)
    {
        Piece capturedPiece = Move(origin, destination);

        //Tests if player is trying to check itself after executing this move.
        if (IsCheck(PlayerTurn))
        {
            UndoMove(origin, destination, capturedPiece);
            throw new BoardException("You cannot check yourself!");
        }
        
        //Promotion to Queen, Pawn special move.
        Piece piece = Board.Piece(destination);
        if (piece is Pawn)
        {
            if ((piece.Color == Color.White && destination.Line == 0) ||
                (piece.Color == Color.Black && destination.Line == 7))
            {
                piece = Board.PullPiece(destination);
                Pieces.Remove(piece);
                Piece queen = new Queen(piece.Color, Board);
                Board.PutPiece(queen, destination);
                Pieces.Add(queen);
            }
        }

        //Tests if opponent king is under threat after executing this move.
        if (IsCheck(Opponent(PlayerTurn)))
        {
            Check = true;
        }
        else
        {
            Check = false;
        }
        
        //Tests if a checkmate was done.
        if (IsCheckmate(Opponent(PlayerTurn)))
        {
            Finished = true;
        }
        else
        {
            Turn++;
            PassTurn();
        }
        
        //En Passant, Pawn special move.
        if (piece is Pawn && (destination.Line == origin.Line - 2 || destination.Line == origin.Line + 2))
        {
            EnPassantThreat = piece;
        }
        else
        {
            EnPassantThreat = null;
        }
    }

    //Undos the move if a player try to check itself.
    public void UndoMove(Position origin, Position destination, Piece capturedPiece)
    {
        Piece piece = Board.PullPiece(destination);
        piece.DecreaseMoves();
        if (capturedPiece != null)
        {
            Board.PutPiece(capturedPiece, destination);
            CapturedPieces.Remove(capturedPiece);
        }
        Board.PutPiece(piece, origin);
        
        //Undos Castling Kingside (short)
        if (piece is King && destination.Column == origin.Column + 2)
        {
            Position rookOrigin = new Position(origin.Line, origin.Column + 3);
            Position rookDestination = new Position(origin.Line, origin.Column + 1);
            Piece closerRook = Board.PullPiece(rookDestination);
            closerRook.DecreaseMoves();
            Board.PutPiece(closerRook, rookOrigin);
        }
        //Undos Castling Queenside (long)
        if (piece is King && destination.Column == origin.Column - 2)
        {
            Position rookOrigin = new Position(origin.Line, origin.Column - 4);
            Position rookDestination = new Position(origin.Line, origin.Column - 1);
            Piece farRook = Board.PullPiece(rookDestination);
            farRook.DecreaseMoves();
            Board.PutPiece(farRook, rookOrigin);
        }
        //Undos En Passant move (Pawn)
        if (piece is Pawn)
        {
            if (origin.Column != destination.Column && capturedPiece == EnPassantThreat)
            {
                Piece pawn = Board.PullPiece(destination);
                Position pawnPosition;
                if (piece.Color == Color.White)
                {
                    pawnPosition = new Position(3, destination.Column);
                }
                else
                {
                    pawnPosition = new Position(4, destination.Column);
                }
                Board.PutPiece(pawn, pawnPosition);
            }
        }
    }
    
    //Throw an exception according to the possibles from origin piece.
    public void ValidateOriginPosition(Position position)
    {
        if (Board.Piece(position) == null)
        {
            throw new BoardException("Piece does not exist!");
        }
        if (PlayerTurn != Board.Piece(position).Color)
        {
            throw new BoardException("This piece belongs to the opponent!");
        }
        if (!Board.Piece(position).HasPossibleMoves())
        {
            throw new BoardException("There are no possible moves for this piece.");
        }
    }

    //Throw an exception if the origin piece cannot move to the destination, when it's not an empty position (null).
    //Also, it validates if the piece is able to move to it, according to your movement rules.
    public void ValidateDestinationPosition(Position origin, Position destination)
    {
        if (!Board.Piece(origin).CanMoveTo(destination))
        {
            throw new BoardException("Invalid destination!");
        }
    }

    //Player turn change method.
    private void PassTurn()
    {
        if (PlayerTurn == Color.White)
        {
            PlayerTurn = Color.Black;
        }
        else
        {
            PlayerTurn = Color.White;
        }
    }

    //Returns all captured pieces from a specific color.
    public HashSet<Piece> Captured(Color color)
    {
        HashSet<Piece> captured = new HashSet<Piece>();
        foreach (Piece piece in CapturedPieces)
        {
            if (piece.Color == color)
            {
                captured.Add(piece);
            }
        }
        return captured;
    }

    //Returns all remaining pieces from a specific color.
    public HashSet<Piece> RemainingPieces(Color color)
    {
        HashSet<Piece> pieces = new HashSet<Piece>();
        foreach (Piece piece in Pieces)
        {
            if (piece.Color == color)
            {
                pieces.Add(piece);
            }
        }
        pieces.ExceptWith(Captured(color));
        return pieces;
    }

    //Inverts a color. This method is useful to determine a check, from IsCheck method below.
    private Color Opponent(Color color)
    {
        if (color == Color.White)
        {
            return Color.Black;
        }
        return Color.White;
    }

    //Returns a King(color) to be tested in IsCheck method below.
    private Piece King(Color color)
    {
        foreach (Piece piece in RemainingPieces(color))
        {
            if (piece is King)
            {
                return piece;
            }
        }
        return null;
    }

    //Returns true if the King(color) can be captured by any of opponent pieces.
    public bool IsCheck(Color color)
    {
        Piece king = King(color);
        if (king == null)
        {
            throw new BoardException("There is no King on the board!");
        }
        //Tests all possible moves from all opponent pieces and returns true if any piece can capture the king.
        foreach (Piece piece in RemainingPieces(Opponent(color)))
        {
            bool[,] canCapture = piece.IsPossibleMove();
            if (canCapture[king.Position.Line, king.Position.Column])
            {
                return true;
            }
        }
        return false;
    }

    //Returns true if the King(color) is in a checkmate!
    public bool IsCheckmate(Color color)
    {
        if (!IsCheck(color))
        {
            return false;
        }
        //Tests if any move of any piece can get the king out of the checkmate.
        foreach (Piece piece in RemainingPieces(color))
        {
            bool[,] threat = piece.IsPossibleMove();
            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (threat[i, j])
                    {
                        Position origin = piece.Position;
                        Position destination = new Position(i, j);
                        Piece capturedPiece = Move(origin, destination);
                        bool check = IsCheck(color);
                        UndoMove(origin, destination, capturedPiece);
                        if (!check)
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    //Place a new piece into the chess board and into the HashSet Pieces.
    public void PutNewPiece(int line, char column, Piece piece)
    {
        Board.PutPiece(piece, new ChessPosition(line, column).ToPosition());
        Pieces.Add(piece);
    }

    //Instantiates a piece object into a board position.
    private void PlacePieces()
    {
        PutNewPiece(1, 'a', new Rook(Color.White, Board));
        PutNewPiece(1, 'h', new Rook(Color.White, Board));
        PutNewPiece(1, 'b', new Knight(Color.White, Board));
        PutNewPiece(1, 'g', new Knight(Color.White, Board));
        PutNewPiece(1, 'c', new Bishop(Color.White, Board));
        PutNewPiece(1, 'f', new Bishop(Color.White, Board));
        PutNewPiece(1, 'd', new Queen(Color.White, Board));
        PutNewPiece(1, 'e', new King(Color.White, Board, this));
        PutNewPiece(2, 'a', new Pawn(Color.White, Board, this));
        PutNewPiece(2, 'b', new Pawn(Color.White, Board, this));
        PutNewPiece(2, 'c', new Pawn(Color.White, Board, this));
        PutNewPiece(2, 'd', new Pawn(Color.White, Board, this));
        PutNewPiece(2, 'e', new Pawn(Color.White, Board, this));
        PutNewPiece(2, 'f', new Pawn(Color.White, Board, this));
        PutNewPiece(2, 'g', new Pawn(Color.White, Board, this));
        PutNewPiece(2, 'h', new Pawn(Color.White, Board, this));

        PutNewPiece(8, 'a', new Rook(Color.Black, Board));
        PutNewPiece(8, 'h', new Rook(Color.Black, Board));
        PutNewPiece(8, 'b', new Knight(Color.Black, Board));
        PutNewPiece(8, 'g', new Knight(Color.Black, Board));
        PutNewPiece(8, 'c', new Bishop(Color.Black, Board));
        PutNewPiece(8, 'f', new Bishop(Color.Black, Board));
        PutNewPiece(8, 'd', new Queen(Color.Black, Board));
        PutNewPiece(8, 'e', new King(Color.Black, Board, this));
        PutNewPiece(7, 'a', new Pawn(Color.Black, Board, this));
        PutNewPiece(7, 'b', new Pawn(Color.Black, Board, this));
        PutNewPiece(7, 'c', new Pawn(Color.Black, Board, this));
        PutNewPiece(7, 'd', new Pawn(Color.Black, Board, this));
        PutNewPiece(7, 'e', new Pawn(Color.Black, Board, this));
        PutNewPiece(7, 'f', new Pawn(Color.Black, Board, this));
        PutNewPiece(7, 'g', new Pawn(Color.Black, Board, this));
        PutNewPiece(7, 'h', new Pawn(Color.Black, Board, this));
    }
}