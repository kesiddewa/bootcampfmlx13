using Chess;

public class GameController
{
    public Status status { get; set; }
    public List<IPlayer> players { get; set; }
    public List<IPiece> pieces { get; set; }
    public IBoard board { get; set; }
    Action<List<Cell>, List<IPiece>>? ScanCheckmate;
    public GameController(List<IPlayer> player, List<IPiece> pieces, IBoard board)
    {
        players = player;
        this.pieces = pieces;
        this.board = board;
        status = Status.Normal;
    }
    // public bool ValidatePiece()
    // {

    // }
    // public bool ValidateDestination()
    // {

    // }
    // public bool ValidateMove()
    // {

    // }
    // public void PieceMove()
    // {

    // }
    // public bool ValidateChecked()
    // {

    // }
    // public bool ValidateCheckmate()
    // {

    // }
    // public void PlayerMove()
    // {

    // }

    // public IPiece PromotePawn(Pawn pawn)
    // {
    // }

    public void EnPassantMove()
    {

    }

    public void CastlingKing()
    {

    }
    public void StartGame()
    {
        status = Status.Normal;
        board.SetBoard(pieces);
        foreach (var player in players)
        {
            player.SetStatus(Status.Normal);
        }
    }

    public void EndGame()
    {

    }
}