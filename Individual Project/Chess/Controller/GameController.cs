using Chess;

public class GameController
{
    public Status status { get; set; }
    public List<IPlayer> players { get; set; }
    public List<IPiece> pieces { get; set; }
    public IBoard board { get; set; }
    // Action
    public GameController(List<IPlayer> player, List<IPiece> pieces, IBoard board)
    {
        this.players = player;
        this.pieces = pieces;
        this.board = board;
        status = Status.Normal;


    }

    public void StartGame()
    {
        status = Status.Normal;
        Console.WriteLine("StartGame() called. Game status: " + this.status);
        if (this.board is Board concreteBoard && this.pieces != null) // MODIFIED LINE
        {
            concreteBoard.SetBoard(this.pieces); // MODIFIED LINE
        }
    }
}